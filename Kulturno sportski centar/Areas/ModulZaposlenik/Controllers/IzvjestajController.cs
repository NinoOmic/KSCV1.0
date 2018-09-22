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
    public class IzvjestajController : Controller
    {
        MojContext ctx = new MojContext();

        public ActionResult Prikazi()
        {
            if (Autentifikacija.KorisnikSesija == null)
                RedirectToAction("Index", "Login", new { area = "" });

            PrikaziIzvjestajVM Model = new PrikaziIzvjestajVM();
            Osoba o = ctx.Osoba.Where(x => x.Id == Autentifikacija.KorisnikSesija.OsobaId).FirstOrDefault();
            Uposlenik u = ctx.Uposlenik.Where(x => x.OsobaId == o.Id).FirstOrDefault();

            if (Autentifikacija.KorisnikSesija.UlogaNaSistemuId==1)
            {
                Model.Izvjestaji = ctx.Izvjestaj.ToList();
            }
            else
            {
                Model.Izvjestaji = ctx.Izvjestaj.Where(x => x.UposlenikId == u.Id).ToList();
            }

            return View("Prikazi", Model);
        }

        public ActionResult Dodaj()
        {
            if (Autentifikacija.KorisnikSesija == null)
                RedirectToAction("Index", "Login", new { area = "" });

            DodajIzvjestajVM Model = new DodajIzvjestajVM();

            Osoba o = Autentifikacija.KorisnikSesija.Osoba;
            Uposlenik u = ctx.Uposlenik.Where(x => x.OsobaId == o.Id).FirstOrDefault();

            Model.Datum = DateTime.Now;
            Model.UposlenikId = u.Id;
            Model.Uposlenik = ctx.Uposlenik.Where(x => x.Id == u.Id).FirstOrDefault();

            return View("Dodaj", Model);

        }

        public ActionResult Uredi(int IzvjestajId)
        {
            if (Autentifikacija.KorisnikSesija == null)
                RedirectToAction("Index", "Login", new { area = "" });

            DodajIzvjestajVM Model = new DodajIzvjestajVM();
            Izvjestaj I = ctx.Izvjestaj.Where(x => x.Id == IzvjestajId).FirstOrDefault();

            Model.Datum = I.Datum;
            Model.Id = I.Id;
            Model.Opis = I.Opis;
            Model.UposlenikId = I.UposlenikId;
            Model.Uposlenik = ctx.Uposlenik.Where(x => x.Id == I.UposlenikId).FirstOrDefault();
            Model.Vrsta = I.Vrsta;

            return View("Dodaj", Model);
        }

        public ActionResult Snimi(DodajIzvjestajVM Model)
        {

            if (Autentifikacija.KorisnikSesija == null)
                RedirectToAction("Index", "Login", new { area = "" });

            if (!ModelState.IsValid)
            {
                return View("Dodaj", Model);
            }

            Izvjestaj I;
            if(Model.Id == 0)
            {
                I = new Izvjestaj();
                ctx.Izvjestaj.Add(I);
            }
            else
            {
                I = ctx.Izvjestaj.Where(x => x.Id == Model.Id).FirstOrDefault();
            }

            I.Datum = Model.Datum;
            I.Opis = Model.Opis;
            I.UposlenikId = Model.UposlenikId;
            I.Uposlenik = ctx.Uposlenik.Where(x => x.Id == Model.UposlenikId).FirstOrDefault();
            I.Vrsta = Model.Vrsta;

            ctx.SaveChanges();

            return RedirectToAction("Prikazi");
        }
    }
}
