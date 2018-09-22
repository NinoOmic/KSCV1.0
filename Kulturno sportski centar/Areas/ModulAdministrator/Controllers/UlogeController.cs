using Kulturno_sportski_centar.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApplication2.Models;
using Kulturno_sportski_centar.ViewModel;
using Kulturno_sportski_centar.Areas.ModulAdministrator.Models;
using Kulturno_sportski_centar.Models;
using Kulturno_sportski_centar.Helper;

namespace Kulturno_sportski_centar.Areas.ModulAdministrator.Controllers
{
    public class UlogeController : Controller
    {
        MojContext ctx = new MojContext();

        public ActionResult Prikazi()
        {
            if (Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });

            PrikaziUlogeVM Model = new PrikaziUlogeVM();
            Model.Uloge = ctx.UlogaNaSistemu.ToList();

            return View("Prikazi", Model);
        }

        public ActionResult Dodaj()
        {
            if (Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });

            DodajUloguVM Model = new DodajUloguVM();

            return View("Dodaj", Model);
        }

        public ActionResult Uredi(int UlogaId)
        {
            if (Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });

            DodajUloguVM Model = new DodajUloguVM();
            UlogaNaSistemu U = ctx.UlogaNaSistemu.Where(x => x.Id == UlogaId).FirstOrDefault();
            Model.UlogaId = U.Id;
            Model.Uloga = U.Uloga;

            return View("Dodaj", Model);
        }

        public ActionResult Snimi(DodajUloguVM Model)
        {
            if (Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });

            if (!ModelState.IsValid)
            {
                return View("Dodaj", Model);
            }

            UlogaNaSistemu U;
            if(Model.UlogaId == 0)
            {
                U = new UlogaNaSistemu();
                ctx.UlogaNaSistemu.Add(U);
            }
            else
            {
                U = ctx.UlogaNaSistemu.Where(x => x.Id == Model.UlogaId).FirstOrDefault();
            }

            U.Uloga = Model.Uloga;
            ctx.SaveChanges();

            return RedirectToAction("Prikazi");
        }
    }
}