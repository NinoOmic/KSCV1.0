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
    
    public class RadnoMjestoController : Controller
    {

        MojContext ctx = new MojContext();

        public ActionResult Prikazi()
        {
            if (Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });

            RadnoMjestoPrikaziVM Model = new RadnoMjestoPrikaziVM();
            Model.RadnaMjesta = ctx.RadnoMjesto.ToList();

            return View("Prikazi", Model);
        }

        public ActionResult Dodaj()
        {
            if (Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });

            DodajRadnoMjestoVM Model = new DodajRadnoMjestoVM();

            return View("Dodaj", Model);
        }

        public ActionResult Uredi(int MjestoId)
        {
            if (Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });

            DodajRadnoMjestoVM Model = new DodajRadnoMjestoVM();
            RadnoMjesto R = ctx.RadnoMjesto.Where(x => x.Id == MjestoId).FirstOrDefault();
            Model.DuzinaRadnogVremena = R.DuzinaRadnogVremena;
            Model.Naziv = R.Naziv;
            Model.Opis = R.Opis;
            Model.Priotritet = R.Prioritet;
            Model.RadnoMjestoId = R.Id;

            return View("Dodaj", Model);
        }

        public ActionResult Snimi(DodajRadnoMjestoVM Model)
        {
            if (Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });

            if (!ModelState.IsValid)
            {
                return View("Dodaj", Model);
            }

            RadnoMjesto R;
            if(Model.RadnoMjestoId == 0)
            {
                R = new RadnoMjesto();
                ctx.RadnoMjesto.Add(R);
                R.DuzinaRadnogVremena = Model.DuzinaRadnogVremena;
                R.isActive = true;
                R.Naziv = Model.Naziv;
                R.Opis = Model.Opis;
                R.Prioritet = Model.Priotritet;
            }
            else
            {
                R = ctx.RadnoMjesto.Where(x => x.Id == Model.RadnoMjestoId).FirstOrDefault();
                R.Id = Model.RadnoMjestoId;
                R.DuzinaRadnogVremena = Model.DuzinaRadnogVremena;
                R.isActive = true;
                R.Naziv = Model.Naziv;
                R.Opis = Model.Opis;
                R.Prioritet = Model.Priotritet;
            }

            ctx.SaveChanges();

            return RedirectToAction("Prikazi");
        }

    }
}