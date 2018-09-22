using Kulturno_sportski_centar.Areas.ModulZaposlenik.Models;
using Kulturno_sportski_centar.DAL;
using Kulturno_sportski_centar.Helper;
using Kulturno_sportski_centar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;


namespace Kulturno_sportski_centar.Areas.ModulZaposlenik.Controllers
{
    public class DogadjajController : Controller
    {

        MojContext ctx = new MojContext();

        public ActionResult Prikazi()
        {
            if (Autentifikacija.KorisnikSesija == null)
                RedirectToAction("Index", "Login", new { area = "" });

            PrikaziDogadjajeVM Model = new PrikaziDogadjajeVM();
            ProvjeraDogadjaja();

            Model.Dogadjaji = ctx.Dogadjaj.Select(x => new PrikaziDogadjajeVM.DogadjajInfo
            {
                DogadjajId = x.Id,
                BrojSjedista = x.BrojMjesta,
                CijenaUlaza = x.CijenaUlaza,
                Datum = x.Termin.Datum,
                Organizator = x.Organizator.Ime + " " + x.Organizator.Prezime,
                VrstaDogadjaja = x.VrstaDogadjaja.Naziv,
                NazivSale = x.Termin.Sala.Naziv,
                VrijemePocetka = x.Termin.Pocetak
            }).ToList();

            return View("Prikazi", Model);
        }

        private void ProvjeraDogadjaja()
        {
            if (Autentifikacija.KorisnikSesija == null)
                RedirectToAction("Index", "Login", new { area = "" });

            List<Dogadjaj> dogadjaji = ctx.Dogadjaj.ToList();
            DateTime Datum = DateTime.Now;
            Termin Termin = new Termin();
            foreach(var d in dogadjaji)
            {
                Termin = ctx.Termin.Where(x => x.Id == d.TerminId).FirstOrDefault();
                if (Termin.Datum < Datum)
                {
                    d.Termin.Zavrsena = true;
                    ctx.SaveChanges();
                }
            }
            
        }

        public ActionResult MojiDogadjaji()
        {
            if (Autentifikacija.KorisnikSesija == null)
                RedirectToAction("Index", "Login", new { area = "" });

            PrikaziDogadjajeVM Model = new PrikaziDogadjajeVM();

            Model.Dogadjaji = ctx.Dogadjaj.Where(x=>x.OrganizatorId == Autentifikacija.KorisnikSesija.OsobaId).Select(x => new PrikaziDogadjajeVM.DogadjajInfo
            {
                DogadjajId = x.Id,
                BrojSjedista = x.BrojMjesta,
                CijenaUlaza = x.CijenaUlaza,
                Datum = x.Termin.Datum,
                Organizator = x.Organizator.Ime + " " + x.Organizator.Prezime,
                VrstaDogadjaja = x.VrstaDogadjaja.Naziv,
                NazivSale = x.Termin.Sala.Naziv
            }).ToList();

            return View("MojiDogadjaji", Model);
        }

        public ActionResult Dodaj(int TerminId)
        {

            DodajDogadjajVM Model = new DodajDogadjajVM();
            Model.TerminId = TerminId;
            Model.Termin = ctx.Termin.Where(x => x.Id == TerminId).FirstOrDefault();
            Model.VrsteDogadjaja = UcitajVrste();

            return View("Dodaj",Model);
            
        }

        public ActionResult Uredi(int DogadjajId)
        {
            if (Autentifikacija.KorisnikSesija == null)
                RedirectToAction("Index", "Login", new { area = "" });

            DodajDogadjajVM Model = new DodajDogadjajVM();
            Dogadjaj D = ctx.Dogadjaj.Where(x => x.Id == DogadjajId).FirstOrDefault();
            Model.BrojMjesta = D.BrojMjesta;
            Model.CijenaUlaza = D.CijenaUlaza;
            Model.DogadjajId = D.Id;
            Model.isActive = D.isActive;
            Model.Organizator = D.Organizator;
            Model.OrganizatorId = D.OrganizatorId;
            Model.Termin = ctx.Termin.Where(x => x.Id == D.Id).FirstOrDefault();
            Model.TerminId = D.TerminId;
            Model.VrstaDogadjajaId = D.VrstaDogadjajaId;
            Model.VrsteDogadjaja = UcitajVrste();

            return View("Dodaj", Model);
        }

        public ActionResult Snimi(DodajDogadjajVM Model)
        {
            if (Autentifikacija.KorisnikSesija == null)
                RedirectToAction("Index", "Login", new { area = "" });

            if (!ModelState.IsValid)
            {
                Model.VrsteDogadjaja = UcitajVrste();
                return View("Dodaj", Model);
            }

            Dogadjaj D;

            if(Model.DogadjajId == 0)
            {
                D = new Dogadjaj();
                ctx.Dogadjaj.Add(D);
            }
            else
            {
                D = ctx.Dogadjaj.Where(x => x.Id == Model.DogadjajId).FirstOrDefault();
            }

            int pom = ctx.Termin.Where(x => x.Id == Model.TerminId).FirstOrDefault().SalaId;
            Sala S = ctx.Sala.Where(x => x.Id == pom).FirstOrDefault();

            if (Model.BrojMjesta > S.BrojSjedista)
            {
                D.BrojMjesta = S.BrojSjedista;
            }
            else
            {
                D.BrojMjesta = Model.BrojMjesta;
            }
            D.CijenaUlaza = Model.CijenaUlaza;
            D.isActive = Model.isActive;
            D.Organizator = ctx.Osoba.Where(x => x.Id == Autentifikacija.KorisnikSesija.OsobaId).FirstOrDefault();
            D.OrganizatorId = Autentifikacija.KorisnikSesija.OsobaId;
            D.Termin = ctx.Termin.Where(x => x.Id == Model.TerminId).FirstOrDefault();
            D.TerminId = Model.TerminId;
            D.Termin.Rezervisan = true;
            D.Termin.Zavrsena = false;
            D.VrstaDogadjajaId = Model.VrstaDogadjajaId;
            D.VrstaDogadjaja = ctx.VrstaDogadjaja.Where(x => x.Id == Model.VrstaDogadjajaId).FirstOrDefault();
            D.isActive = true;

            ctx.SaveChanges();

            return RedirectToAction("Prikazi");
        }

        public ActionResult Obrisi(int DogadjajId)
        {
            if (Autentifikacija.KorisnikSesija == null)
                RedirectToAction("Index", "Login", new { area = "" });

            Dogadjaj D = ctx.Dogadjaj.Where(x => x.Id == DogadjajId).FirstOrDefault();

            List<RezervacijaZaDogadjaj> karte = ctx.RezervacijaZaDogadjaj.Where(x => x.DogadjajId == DogadjajId).ToList();
            foreach(var k in karte)
            {
                ctx.RezervacijaZaDogadjaj.Remove(k);
                ctx.SaveChanges();
            }
            ctx.Termin.Where(x => x.Id == D.TerminId).FirstOrDefault().Rezervisan = false;
            ctx.Termin.Where(x => x.Id == D.TerminId).FirstOrDefault().Zavrsena = false;
            ctx.Dogadjaj.Remove(D);
            ctx.SaveChanges();

            return RedirectToAction("Prikazi");
        }

        private List<SelectListItem> UcitajVrste()
        {
            if (Autentifikacija.KorisnikSesija == null)
                RedirectToAction("Index", "Login", new { area = "" });

            List<SelectListItem> Vrste = new List<SelectListItem>();
            Vrste.Add(new SelectListItem
            {
                Value = 0.ToString(),
                Text = "Izaberi vrstu!"
            });
            Vrste.AddRange(ctx.VrstaDogadjaja.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Naziv + " - " + x.Id.ToString()
            }).ToList());

            return Vrste;
        }
    }
}
