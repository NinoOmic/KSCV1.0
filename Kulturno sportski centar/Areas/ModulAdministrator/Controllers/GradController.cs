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
    public class GradController : Controller
    {
      
        
            MojContext ctx = new MojContext();
            public ActionResult Prikazi(int? RegijaId)
            {
            if (Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });

            GradPrikaziViewModel Model = new GradPrikaziViewModel();
               
            Model.Gradovi = ctx.Grad
                .Where(x=> !RegijaId.HasValue||x.RegijaId==RegijaId)
                .Include(x => x.Regija)
                .Include(x => x.Regija.Drzava)  
                .ToList();
            Model.Regije = ctx.Regija.Include(x => x.Drzava).ToList();
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
            GradEditViewModel Model = new GradEditViewModel();
            Model.Regije = UcitajRegije();
                
                return View("Dodaj",Model);
            }

            public ActionResult Uredi(int Id)
            {
            if (Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });

            GradEditViewModel Model = new GradEditViewModel();
            Model.Regije = UcitajRegije();
            Grad g = ctx.Grad.Where(x => x.Id == Id).FirstOrDefault();
            Model.RegijaId = g.RegijaId;
            Model.Naziv = g.Naziv;
            Model.Oznaka = g.Oznaka;
            Model.PostanskiBroj = g.PostanskiBroj;
                return View("Dodaj",Model);
            }

     
        public ActionResult Spremi(GradEditViewModel G)
            {

            if (Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });

            if (!ModelState.IsValid)
            {
                G.Regije = UcitajRegije();
                return View("Dodaj", G);
            }
           
                Grad grad;
                if (G.Id == 0 )
                {
                grad = new Grad();

                    ctx.Grad.Add(grad);
                }
                else
                {
                grad = ctx.Grad.Where(x => x.Id == G.Id).FirstOrDefault();
                }

            grad.Naziv = G.Naziv;
            grad.Oznaka = G.Oznaka;
            grad.RegijaId = G.RegijaId;
            grad.PostanskiBroj = G.PostanskiBroj;
                ctx.SaveChanges();

                return RedirectToAction("Prikazi");
            }
            public ActionResult Obrisi(int Id)
            {
            if (Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });

            Grad g = new Grad();
                g = ctx.Grad.Where(x => x.Id == Id).FirstOrDefault();

                ctx.Grad.Remove(g);
                ctx.SaveChanges();

                return RedirectToAction("Prikazi");
            }
        private List<SelectListItem> UcitajRegije()
        {
            List<SelectListItem> regije = new List<SelectListItem>();
            regije.AddRange(ctx.Regija.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Drzava.Naziv + " - " + x.Naziv }));


            return regije;
        }
        }
}