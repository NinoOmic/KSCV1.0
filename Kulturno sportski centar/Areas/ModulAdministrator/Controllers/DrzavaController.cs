using Kulturno_sportski_centar.Areas.ModulAdministrator.Models;
using Kulturno_sportski_centar.DAL;
using Kulturno_sportski_centar.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace Kulturno_sportski_centar.Areas.ModulAdministrator.Controllers
{
    public class DrzavaController : Controller
    {
        MojContext ctx = new MojContext();
        public ActionResult Prikazi()
        {
            if (Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });

            DrzavaPrikaziViewModel Model = new DrzavaPrikaziViewModel();
           Model.Drzave = ctx.Drzava.ToList();            
            return View("Prikazi",Model);
        }
        public ActionResult index()
        {
            if (Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });

            return RedirectToAction("Prikazi");
        }
        public ActionResult Dodaj()
        {
            if (Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });

            DrzavaEditModelView Model = new DrzavaEditModelView();
            Drzava d = new Drzava();

            d.Naziv = Model.Naziv;
            d.Oznaka = Model.Oznaka;
            return View("Dodaj",Model);
        }

        public ActionResult Uredi(int Id)
        {
            if (Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });

            Drzava d = ctx.Drzava.Where(x => x.Id == Id).FirstOrDefault();
            DrzavaEditModelView Model = new DrzavaEditModelView();

             Model.Naziv = d.Naziv;
            Model.Oznaka= d.Oznaka;
            
            return View("Dodaj",Model);
        }

        public ActionResult Spremi(DrzavaEditModelView D)
        {
            if (Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });

            if (!ModelState.IsValid)
            {
                return View("Dodaj", D);
            }
            Drzava d;
            if (D.Id==0 )
            {
                d = new Drzava();
                
                ctx.Drzava.Add(d);
            }
            else
            {
                d = ctx.Drzava.Where(x => x.Id == D.Id).FirstOrDefault();
            }
      
            d.Naziv = D.Naziv;
            d.Oznaka = D.Oznaka;
            ctx.SaveChanges();

            return RedirectToAction("Prikazi");
        }
        public ActionResult Obrisi(int Id)
        {
            if (Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });

            Drzava d = new Drzava();
            d = ctx.Drzava.Where(x => x.Id == Id).FirstOrDefault();
           
            ctx.Drzava.Remove(d);
            ctx.SaveChanges();

            return RedirectToAction("Prikazi");
        }

    }
}