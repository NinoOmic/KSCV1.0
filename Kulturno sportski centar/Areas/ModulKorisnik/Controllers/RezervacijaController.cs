using Kulturno_sportski_centar.Areas.ModulKorisnik.Models;
using Kulturno_sportski_centar.DAL;
using Kulturno_sportski_centar.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace Kulturno_sportski_centar.Areas.ModulKorisnik.Controllers
{
    public class RezervacijaController : Controller
    {
        MojContext ctx = new MojContext();
        public ActionResult Prikazi()
        {
            if (Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });
            RezervacijaPrikaziViewModel Model = new RezervacijaPrikaziViewModel();

            Model.Termini = ctx.Termin.ToList();

            Model.Korisnici = ctx.Korisnik.ToList();
            ProvjeraRezervacija();
            if (Autentifikacija.KorisnikSesija.UlogaNaSistemuId == 2)
            {
                Model.Rezervacije = ctx.Rezervacija.Where(x => x.Korisnik.OsobaId == Autentifikacija.KorisnikSesija.OsobaId)
                    .Select(x => new RezervacijaPrikaziViewModel.RezervacijaInfo
                    {
                        Id = x.Id,
                        Datum = x.Termin.Datum,
                        Pocetak = x.Termin.Pocetak,
                        Kraj = x.Termin.Kraj,
                        Korisnik = x.Korisnik.Osoba.KorisnickoIme,
                        Sala = x.Termin.Sala.Naziv,
                        Zavrsena = x.Zavrsena
                    }).ToList();
            }
            else
            {
                Model.Rezervacije = ctx.Rezervacija
                    .Select(x => new RezervacijaPrikaziViewModel.RezervacijaInfo
                    {
                        Id = x.Id,
                        Datum = x.Termin.Datum,
                        Pocetak = x.Termin.Pocetak,
                        Kraj = x.Termin.Kraj,
                        Korisnik = x.Korisnik.Osoba.KorisnickoIme,
                        Sala = x.Termin.Sala.Naziv,
                        Zavrsena = x.Zavrsena
                    }).ToList();
            }



            return View("Prikazi",Model);
        }
        private void ProvjeraRezervacija()
        {
            if (Autentifikacija.KorisnikSesija == null)
                 RedirectToAction("Index", "Login", new { area = "" });
            List<Rezervacija> rezervacijas = ctx.Rezervacija.ToList();
            DateTime Datum = DateTime.Today;
            foreach (var x in rezervacijas)
            {
                if (x.Termin.Datum < Datum)
                    x.Zavrsena = true;
            }
            ctx.SaveChanges();
        }
        public ActionResult Index()
        {
            if (Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });
            return RedirectToAction("Prikazi");
        }
        public ActionResult Obrisi(int Id)
        {
            if (Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });
            Rezervacija R = new Rezervacija();
            R = ctx.Rezervacija.Where(x => x.Id == Id).FirstOrDefault();
            ctx.Termin.Where(x => x.Id == R.TerminId).FirstOrDefault().Rezervisan = false;
            ctx.Rezervacija.Remove(R);
            ctx.SaveChanges();

            return RedirectToAction("Prikazi");
        }


        public ActionResult Snimi(int TerminId)
        {
            if (Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });
            Rezervacija R = new Rezervacija();
            Termin T = ctx.Termin.Where(y => y.Id == TerminId).FirstOrDefault();
            Rezervacija R1 = new Rezervacija
            {
                KorisnikId = Autentifikacija.KorisnikSesija.Id,
                isActive = true,
                Termin = T,
                Zavrsena = false

            };
            T.Rezervisan = true;
      
           
            ctx.Rezervacija.Add(R1);
            ctx.SaveChanges();
            return RedirectToAction("Prikazi");
        }
    }
}