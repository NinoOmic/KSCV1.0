using Kulturno_sportski_centar.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using System.Data.Entity;
using Kulturno_sportski_centar.Areas.ModulAdministrator.Models;
using Kulturno_sportski_centar.Helper;

namespace Kulturno_sportski_centar.Areas.ModulAdministrator.Controllers
{
    public class RegijaController : Controller
    {
        MojContext ctx = new MojContext();
        public ActionResult Prikazi(int? DrzavaId )
        {
            if (Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });

            RegijaPrikaziViewModel Model = new RegijaPrikaziViewModel();

            Model.Regije = ctx.Regija
                .Where(x=> !DrzavaId.HasValue ||x.DrzavaId == DrzavaId)
                .Include(x=>x.Drzava).ToList();
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
            Regija r = new Regija();
            RegijaEditViewModel Model = new RegijaEditViewModel();
            
            List<Drzava> Drzave = new List<Drzava>();
              Model.Drzave = UcitajDrzave();
           
            return View("Dodaj",Model);
        }

        public ActionResult Uredi(int Id)
        {
            if (Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });
            RegijaEditViewModel Model = new RegijaEditViewModel();

            Model.Drzave = UcitajDrzave();
           
            Regija r = ctx.Regija.Where(x => x.Id == Id).FirstOrDefault();
            Model.DrzavaId = r.DrzavaId;
            Model.Naziv = r.Naziv;
            Model.Oznaka = r.Oznaka;
            return View("Dodaj",Model);
        }

        public ActionResult Spremi(RegijaEditViewModel R)
        {
            if (Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });
            if (!ModelState.IsValid)
            {
                R.Drzave=UcitajDrzave();
                return View("Dodaj", R);
            }
            Regija regija;
            if (R.Id == 0 )
            {
                regija = new Regija();

                ctx.Regija.Add(regija);
            }
            else
            {
                regija = ctx.Regija.Where(x => x.Id == R.Id).FirstOrDefault();
            }

            regija.Naziv = R.Naziv;
            regija.Oznaka = R.Oznaka;
            regija.DrzavaId = R.DrzavaId;
            ctx.SaveChanges();

            return RedirectToAction("Prikazi");
        }
        public ActionResult Obrisi(int Id)
        {
            if (Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });
            Regija r = new Regija();
            r = ctx.Regija.Where(x => x.Id == Id).FirstOrDefault();

            ctx.Regija.Remove(r);
            ctx.SaveChanges();

            return RedirectToAction("Prikazi");
        }
         private List<SelectListItem> UcitajDrzave()
         {
            List<SelectListItem> drzave = new List<SelectListItem>();


             drzave.AddRange(ctx.Drzava.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv  }).ToList());
              return drzave;
          }
}

}
