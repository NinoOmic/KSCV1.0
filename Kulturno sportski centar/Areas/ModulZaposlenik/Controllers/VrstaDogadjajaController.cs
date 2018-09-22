using Kulturno_sportski_centar.Areas.ModulKorisnik.Models;
using Kulturno_sportski_centar.Areas.ModulZaposlenik.Models;
using Kulturno_sportski_centar.DAL;
using Kulturno_sportski_centar.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace Kulturno_sportski_centar.Areas.ModulZaposlenik.Controllers
{
    public class VrstaDogadjajaController : Controller
    {
        MojContext ctx = new MojContext();

        public ActionResult Prikazi()
        {
            if(Autentifikacija.KorisnikSesija.UlogaNaSistemuId!=1 || Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });

            PrikaziVrstuDogadjajaVM Model = new PrikaziVrstuDogadjajaVM();
            Model.VrsteDogadjaja = ctx.VrstaDogadjaja.ToList();

            return View("Prikazi", Model);
        }

        public ActionResult Dodaj()
        {
            if (Autentifikacija.KorisnikSesija.UlogaNaSistemuId != 1 || Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });

            DodajVrstuDogadjajaVM Model = new DodajVrstuDogadjajaVM();

            return View("Dodaj", Model);
        }

        public ActionResult Uredi(int VrstaId)
        {
            if (Autentifikacija.KorisnikSesija.UlogaNaSistemuId != 1 || Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });

            DodajVrstuDogadjajaVM Model = new DodajVrstuDogadjajaVM();
            VrstaDogadjaja VD = ctx.VrstaDogadjaja.Where(x => x.Id == VrstaId).FirstOrDefault();
            Model.Naziv = VD.Naziv;
            Model.IsActive = VD.isActive;
            Model.VrstaDogadjajaId = VD.Id;
            Model.Opis = VD.Opis;

            return View("Dodaj", Model);
        }

        public ActionResult Snimi(DodajVrstuDogadjajaVM Model)
        {
            if (Autentifikacija.KorisnikSesija.UlogaNaSistemuId != 1 || Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });

            if (!ModelState.IsValid)
            {
                return View("Dodaj", Model);
            }

            VrstaDogadjaja VD;

            if(Model.VrstaDogadjajaId == 0)
            {
                VD = new VrstaDogadjaja();
                ctx.VrstaDogadjaja.Add(VD);
            }
            else
            {
                VD = ctx.VrstaDogadjaja.Where(x => x.Id == Model.VrstaDogadjajaId).FirstOrDefault();
            }

            VD.isActive = true;
            VD.Naziv = Model.Naziv;
            VD.Opis = Model.Opis;
            ctx.SaveChanges();

            return RedirectToAction("Prikazi");
        }

    }
}