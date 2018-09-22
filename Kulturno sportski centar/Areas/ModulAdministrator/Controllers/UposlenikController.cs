using Kulturno_sportski_centar.Areas.ModulAdministrator.Models;
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

namespace Kulturno_sportski_centar.Areas.ModulAdministrator.Controllers
{
    public class UposlenikController : Controller
    {
        MojContext ctx = new MojContext();

        public ActionResult Prikazi()
        {
            if (Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });

            UposlenikPrikaziVM Model = new UposlenikPrikaziVM();
            Model.Uposlenici = ctx.Uposlenik.Select(x => new UposlenikPrikaziVM.UposlenikInfo
            {
                ImePrezime = x.Osoba.Ime + " " + x.Osoba.Prezime,
                UposlenikId = x.Id,
                Iskustvo = x.Iskustvo,
                DatumZaposljenja = x.DatumZaposljenja,
                RadnoMjesto = x.RadnoMjesto.Naziv,
                Zvanje = x.Zvanje,
                OsobaId = x.OsobaId
            }).ToList();

            return View("Prikazi", Model);
        }

        public ActionResult Dodaj()
        {
            if (Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });

            DodajUposlenikaVM Model = new DodajUposlenikaVM();
            Model.RadnaMjesta = UcitajRadnaMjesta();
            Model.Gradovi = UcitajGradove();
            Model.DatumRodjenja = DateTime.Now;

            return View("Dodaj", Model);
        }

        public ActionResult Snimi(DodajUposlenikaVM Model)
        {
            if (Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });

            Grad G = ctx.Grad.Where(x => x.Id == Model.GradId).FirstOrDefault();
            if(G == null)
            {
                Model.RadnaMjesta = UcitajRadnaMjesta();
                Model.Gradovi = UcitajGradove();
                ViewData["error"] = "Izaberi grad!";
                return View("Dodaj", Model);
            }

            if(Model.RadnoMjestoId == 0)
            {
                Model.RadnaMjesta = UcitajRadnaMjesta();
                Model.Gradovi = UcitajGradove();
                ViewData["error1"] = "Izaberi radno mjesto!";
                return View("Dodaj", Model);
            }

            Osoba Osoba = ctx.Osoba.Where(x => x.KorisnickoIme == Model.KorisnckoIme).FirstOrDefault();
            if (Osoba != null)
            {
                Model.RadnaMjesta = UcitajRadnaMjesta();
                Model.Gradovi = UcitajGradove();
                ViewData["error2"] = "Korisničko ime već postoji!";

                return View("Dodaj", Model);
            }

            Osoba Osoba2 = ctx.Osoba.Where(x => x.Email == Model.Email).FirstOrDefault();

            if (Osoba2 != null)
            {
                Model.RadnaMjesta = UcitajRadnaMjesta();
                Model.Gradovi = UcitajGradove();
                ViewData["error3"] = "Taj E-mail se već koristi!";

                return View("Dodaj", Model);
            }

            if (!ModelState.IsValid)
            {
                Model.RadnaMjesta = UcitajRadnaMjesta();
                Model.Gradovi = UcitajGradove();

                return View("Dodaj", Model);
            }

            Osoba O = new Osoba();
            Uposlenik U = new Uposlenik();
            Korisnik K = new Korisnik();
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

            ctx.Uposlenik.Add(U);
            U.OsobaId = ctx.Osoba.Where(x => x.KorisnickoIme == O.KorisnickoIme).FirstOrDefault().Id;
            U.Osoba = ctx.Osoba.Where(x => x.KorisnickoIme == O.KorisnickoIme).FirstOrDefault();
            U.RadnoMjesto = ctx.RadnoMjesto.Where(x => x.Id == Model.RadnoMjestoId).FirstOrDefault();
            U.RadnoMjestoId = Model.RadnoMjestoId;
            U.Zvanje = Model.Zvanje;
            U.Iskustvo = Model.Iskustvo;
            U.DatumZaposljenja = DateTime.Now;
            ctx.SaveChanges();
            ctx.Korisnik.Add(K);
            K.OsobaId = ctx.Osoba.Where(x => x.KorisnickoIme == O.KorisnickoIme).FirstOrDefault().Id;
            K.Osoba = ctx.Osoba.Where(x => x.KorisnickoIme == O.KorisnickoIme).FirstOrDefault();
            K.DatumRegistracije = DateTime.Now;
            K.UlogaNaSistemu = ctx.UlogaNaSistemu.Where(x => x.Id == 3).FirstOrDefault();
            K.UlogaNaSistemuId = 3;

            ctx.SaveChanges();

            return RedirectToAction("Prikazi");
        }

        public ActionResult UrediSnimi(UrediUposlenikVM Model)
        {
            if (Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });

            Uposlenik U = ctx.Uposlenik.Where(x => x.Id == Model.UposlenikId).FirstOrDefault();
            U.RadnoMjestoId = Model.RadnoMjestoId;
            U.RadnoMjesto = ctx.RadnoMjesto.Where(x => x.Id == Model.RadnoMjestoId).FirstOrDefault();
            U.Zvanje = Model.Zvanje;
            U.DatumZaposljenja = Model.DatumZaposljenja;
            Osoba o = ctx.Osoba.Where(x => x.Id == U.OsobaId).FirstOrDefault();
            if(ctx.RadnoMjesto.Where(x=>x.Id == Model.RadnoMjestoId).FirstOrDefault().Naziv == "otpusten")
            {
                o.Lozinka = "otpustenLozinka";
            }

            ctx.SaveChanges();

            return RedirectToAction("Prikazi");
        }

        public ActionResult Uredi(int UposlenikId)
        {
            if (Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });

            UrediUposlenikVM Model = new UrediUposlenikVM();
            Uposlenik U = ctx.Uposlenik.Where(x => x.Id == UposlenikId).FirstOrDefault();
            Model.UposlenikId = UposlenikId;
            Model.Zvanje = U.Zvanje;
            Model.RadnoMjestoId = U.RadnoMjestoId;
            Model.RadnaMjesta = UcitajRadnaMjesta();
            Model.DatumZaposljenja = U.DatumZaposljenja;

            return View("Uredi", Model);
        }

        private List<SelectListItem> UcitajRadnaMjesta()
        {
            List<SelectListItem> mjesta = new List<SelectListItem>();
            mjesta.AddRange(ctx.RadnoMjesto.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Naziv
            }).ToList());

            return mjesta;
        }

        private List<SelectListItem> UcitajGradove()
        {
            List<SelectListItem> Gradovi = new List<SelectListItem>();
            Gradovi.Add(new SelectListItem
            {
                Value = 0.ToString(),
                Text = "Odaberi grad!"
            });

            Gradovi.AddRange(ctx.Grad.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Naziv
            }).ToList());

            return Gradovi;
        }

    }
}