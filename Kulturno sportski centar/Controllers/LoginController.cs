using Kulturno_sportski_centar.DAL;
using Kulturno_sportski_centar.Helper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace Kulturno_sportski_centar.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

       
        public ActionResult Provjera(string username, string password)
        {
            using (MojContext db = new MojContext())
            {
                Korisnik Korisnik = db.Korisnik.Include(x=>x.Osoba).Where(x => x.Osoba.KorisnickoIme == username).FirstOrDefault();
                if (Korisnik==null)
                {
                    ViewData["error"] = "Pogrešan username!";
                    return View("Index");
                }
                else if (Korisnik.Osoba.Lozinka == password && Korisnik.UlogaNaSistemuId==1)
                {
                    Autentifikacija.KorisnikSesija = Korisnik;
                    Autentifikacija.KorisnikSesija.UlogaNaSistemuId = Korisnik.UlogaNaSistemuId;
                    return RedirectToAction("Index", "Home");
                }else if(Korisnik.Osoba.Lozinka == password && (Korisnik.UlogaNaSistemuId == 2 || Korisnik.UlogaNaSistemuId == 3))
                {
                    Autentifikacija.KorisnikSesija = Korisnik;
                    Autentifikacija.KorisnikSesija.UlogaNaSistemuId = Korisnik.UlogaNaSistemuId;
                    return RedirectToAction("HomeKorisnik", "Home");
                }                
                else
                {
                    ViewData["error"] = "Pogrešan password!";
                    return View("Index");
                }
            }
        }

        public ActionResult odjaviSe()
        {
            Autentifikacija.odjava();
            return RedirectToAction("Index", "Login");
        }
    }
}