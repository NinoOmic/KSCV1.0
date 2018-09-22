using Kulturno_sportski_centar.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kulturno_sportski_centar.Areas.ModulAdministrator.Controllers
{
    public class LokacijaController : Controller
    {
        // GET: ModulAdministrator/Lokacija
        public ActionResult Index()
        {
            if (Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });
            return View();
        }
    }
}