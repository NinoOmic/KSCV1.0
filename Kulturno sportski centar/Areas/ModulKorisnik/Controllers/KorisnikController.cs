using Kulturno_sportski_centar.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using System.Data.Entity;
using Kulturno_sportski_centar.Models;
using Kulturno_sportski_centar.Areas.ModulKorisnik.Models;
using static Kulturno_sportski_centar.Areas.ModulKorisnik.Models.KorisnikPrikaziViewModel;
using Kulturno_sportski_centar.Helper;

namespace Kulturno_sportski_centar.Areas.ModulKorisnik
{
    public class KorisnikController : Controller
    {
        MojContext ctx = new MojContext();

        public ActionResult Prikazi(bool? error, int? UlogaNaSistemuId)
        {
            if (Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });

            KorisnikPrikaziViewModel Model = new KorisnikPrikaziViewModel();
           
            Model.Korisnici = ctx.Korisnik
                  .Where(x => !UlogaNaSistemuId.HasValue || UlogaNaSistemuId == x.UlogaNaSistemuId)
                  .Select(x => new KorisnikPrikaziViewModel.KorisnikInfo()
                  {
                      Ime = x.Osoba.Ime,
                      Prezime = x.Osoba.Prezime,
                      KorisnickoIme = x.Osoba.KorisnickoIme,
                      UlogaNaSistemu = x.UlogaNaSistemu.Uloga,
                      Id = x.Id
                  }
                   ).ToList();
               Model.UlogeNaSistemu = ctx.UlogaNaSistemu.ToList();

            if (error == true)
            {
                ViewData["error"] = "Ne možeš obrisati ovog korinika jer ima oraganizovan događaj/e!";
            }

             return View("Prikazi",Model);
        }
    

        public ActionResult Dodaj()
        {
            if (Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });

            KorisnikEditViewModel Model = new KorisnikEditViewModel();
            Korisnik k = new Korisnik();
            k.Osoba = new Osoba();
            k.UlogaNaSistemu = new UlogaNaSistemu();
            Model.DatumRodjenja = DateTime.Now;
            Model.Uloge = UcitajUloge();
            Model.Gradovi = UcitajGradove();
            return View("Dodaj", Model);
        }  

        public ActionResult Index()
        {
            if (Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });
            return RedirectToAction("Prikazi");
        }

        public ActionResult Detalji(int Id)
        {
            if (Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });
            KorisnikPrikaziDetaljnoViewModel Model = new KorisnikPrikaziDetaljnoViewModel();
            
            List<Korisnik> Korisnici = ctx.Korisnik
           .Include(x => x.Osoba)
           .Include(x => x.Osoba.Grad)
           .Include(x => x.Osoba.Grad.Regija)
           .Include(x => x.Osoba.Grad.Regija.Drzava)
           .Include(x => x.UlogaNaSistemu)
           .ToList();

            Model = Korisnici.Where(x => x.Id == Id).Select(x => new KorisnikPrikaziDetaljnoViewModel()
            {
                Id = x.Id,
                Ime = x.Osoba.Ime,
                Prezime = x.Osoba.Prezime,
                KorisnickoIme = x.Osoba.KorisnickoIme,
                UlogaNaSistemu = x.UlogaNaSistemu.Uloga,
                DatumRegistracije = x.DatumRegistracije,
                DatumRodjenja = (DateTime)x.Osoba.DatumRodjenja,
                Grad = x.Osoba.Grad.Naziv,
                Adresa = x.Osoba.Adresa,
                JMBG = x.Osoba.JMBG,
                Email = x.Osoba.Email,
                Telefon = x.Osoba.Telefon
            }).FirstOrDefault();


            return View("Detalji",Model);
        }

        public ActionResult Uredi(int Id)
        {
            if (Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });
            Korisnik a = ctx.Korisnik.Where(x => x.Id == Id).Include(x=>x.Osoba).FirstOrDefault();
            KorisnikEditViewModel Model = new KorisnikEditViewModel();
            Model.Gradovi = UcitajGradove();
            Model.Uloge = UcitajUloge();
            Model.Ime=  a.Osoba.Ime;
            Model.DatumRegistracije= a.DatumRegistracije  ;
            Model.Prezime=  a.Osoba.Prezime ;
            Model.DatumRodjenja= a.Osoba.DatumRodjenja;
            Model.KorisnckoIme= a.Osoba.KorisnickoIme ;
            Model.JMBG=a.Osoba.JMBG ;
            Model.Telefon=  a.Osoba.Telefon ;
            Model.Adresa=a.Osoba.Adresa ;
            Model.Email= a.Osoba.Email ;
            Model.Lozinka= a.Osoba.Lozinka;
            Model.GradId= a.Osoba.GradId ;
            Model.UlogaId= a.UlogaNaSistemuId ;
           

            return View("Uredi",Model);
        }
        public ActionResult Spremi(KorisnikEditViewModel Model)
        {
            if (Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });

            Osoba Osoba1 = ctx.Osoba.Where(x => x.KorisnickoIme == Model.KorisnckoIme).FirstOrDefault();
            Osoba Osoba2 = ctx.Osoba.Where(x => x.Email == Model.Email).FirstOrDefault();

            if (!ModelState.IsValid)
            {
                Model.Gradovi = UcitajGradove();
                Model.Uloge = UcitajUloge();
                return View("Dodaj", Model);
            }
            Korisnik K;
            Osoba O;
            Uposlenik U = ctx.Uposlenik.Where(x => x.OsobaId == Model.Id).FirstOrDefault();

            if ( Model.Id==0)
            {

                if (Osoba1 != null)
                {
                    Model.Uloge = UcitajUloge();
                    Model.Gradovi = UcitajGradove();
                    ViewData["error"] = "Korisničko ime već postoji!";

                    return View("Dodaj", Model);
                }

                if (Osoba2 != null)
                {
                    Model.Uloge = UcitajUloge();
                    Model.Gradovi = UcitajGradove();
                    ViewData["error1"] = "Taj E-mail se već koristi!";

                    return View("Dodaj", Model);
                }

                if (Model.UlogaId == 3)
                {
                    Model.Uloge = UcitajUloge();
                    Model.Gradovi = UcitajGradove();
                    ViewData["error2"] = "Ne možeš tako napraviti Uposlenika!";
                }

                K = new Korisnik();
                O = new Osoba();

                ctx.Osoba.Add(O);

                O.Grad = ctx.Grad.Where(x => x.Id == Model.GradId).FirstOrDefault();
                O.GradId = Model.GradId;
                O.Ime = Model.Ime;
                O.Prezime = Model.Prezime;
                O.KorisnickoIme = Model.KorisnckoIme;
                O.Lozinka = Model.Lozinka;
                O.JMBG = Model.JMBG;
                O.Telefon = Model.Telefon;
                O.DatumRodjenja = Model.DatumRodjenja;
                O.Adresa = Model.Adresa;
                O.Email = Model.Email;
                ctx.SaveChanges();

                ctx.Korisnik.Add(K);
                K.OsobaId = ctx.Osoba.Where(x => x.KorisnickoIme == O.KorisnickoIme).FirstOrDefault().Id;
                K.Osoba = ctx.Osoba.Where(x => x.KorisnickoIme == O.KorisnickoIme).FirstOrDefault();
                K.DatumRegistracije = DateTime.Now;
                K.UlogaNaSistemu = ctx.UlogaNaSistemu.Where(x => x.Id == Model.UlogaId).FirstOrDefault();
                K.UlogaNaSistemuId = Model.UlogaId;
                ctx.SaveChanges();
            }
            else
            {
                if(Model.GradId == 0)
                {
                    Model.Uloge = UcitajUloge();
                    Model.Gradovi = UcitajGradove();
                    ViewData["error"] = "Izaberi grad!";

                    return View("Uredi", Model);
                }

                if (Model.UlogaId == 3 && U == null)
                {
                    Model.Uloge = UcitajUloge();
                    Model.Gradovi = UcitajGradove();
                    ViewData["error1"] = "Ne možeš Korisnika promjeniti u Uposlenika!";
                    Model.UlogaId = 2;
                    return View("Uredi", Model);
                }

                O = ctx.Osoba.Where(x => x.Id == Model.Id).FirstOrDefault();
                K = ctx.Korisnik.Where(x => x.OsobaId == O.Id).FirstOrDefault();   

                K.OsobaId = ctx.Osoba.Where(x => x.KorisnickoIme == O.KorisnickoIme).FirstOrDefault().Id;
                K.Osoba = ctx.Osoba.Where(x => x.KorisnickoIme == O.KorisnickoIme).FirstOrDefault();
                K.DatumRegistracije = DateTime.Now;
                K.UlogaNaSistemu = ctx.UlogaNaSistemu.Where(x => x.Id == Model.UlogaId).FirstOrDefault();
                K.UlogaNaSistemuId = Model.UlogaId;

                ctx.SaveChanges();

                O.Grad = ctx.Grad.Where(x => x.Id == Model.GradId).FirstOrDefault();
                O.GradId = Model.GradId;
                O.Ime = Model.Ime;
                O.Prezime = Model.Prezime;
                O.KorisnickoIme = Model.KorisnckoIme;
                O.Lozinka = Model.Lozinka;
                O.JMBG = Model.JMBG;
                O.Telefon = Model.Telefon;
                O.DatumRodjenja = Model.DatumRodjenja;
                O.Adresa = Model.Adresa;
                O.Email = Model.Email;
                ctx.SaveChanges();

            }

            return RedirectToAction("Detalji", new { Id = K.Id });
        }
        public ActionResult Obrisi(int Id)
        {
            if (Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });


            Uposlenik u = new Uposlenik();

            Korisnik k = new Korisnik();

            k = ctx.Korisnik.Where(x => x.Id == Id).FirstOrDefault();

            Dogadjaj d = ctx.Dogadjaj.Where(x => x.OrganizatorId == k.OsobaId).FirstOrDefault();
            if (d != null)
            {
                return RedirectToAction("Prikazi",new { error = true });
            }



            List<Rezervacija> rezervacijas = ctx.Rezervacija.Where(x => x.Korisnik.Id == k.Id).ToList();
            foreach (var x in rezervacijas)
            {
                ctx.Termin.Where(y => y.Id == x.TerminId).FirstOrDefault().Rezervisan = false;
                ctx.Rezervacija.Remove(x);
                ctx.SaveChanges();
            }

            List<RezervacijaZaDogadjaj> rez = ctx.RezervacijaZaDogadjaj.Where(x => x.OsobaId == k.OsobaId).ToList();
            foreach(var r in rez)
            {
                ctx.RezervacijaZaDogadjaj.Remove(r);
                ctx.SaveChanges();
            }

            int pom = k.OsobaId;
            Osoba o = ctx.Osoba.Where(x => x.Id == pom).FirstOrDefault();

            ctx.Korisnik.Remove(k);
            ctx.Osoba.Remove(o);
            ctx.SaveChanges();

            return RedirectToAction("Prikazi");
        }

        private List<SelectListItem> UcitajUloge()
        {
            List<SelectListItem> uloge = new List<SelectListItem>();
            uloge.AddRange(ctx.UlogaNaSistemu.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Uloga + " " + x.Id.ToString() }));

            return uloge;
        }
        private List<SelectListItem> UcitajGradove()
        {
            List<SelectListItem> gradovi = new List<SelectListItem>();
            gradovi.AddRange(ctx.Grad.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }));

            return gradovi;
        }
    }
}