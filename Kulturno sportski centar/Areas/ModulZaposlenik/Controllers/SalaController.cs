using Kulturno_sportski_centar.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using System.Data.Entity;
using Kulturno_sportski_centar.Helper;
using Kulturno_sportski_centar.Areas.ModulZaposlenik.Models;

namespace Kulturno_sportski_centar.Areas.ModulZaposlenik.Controllers
{

    public class SalaController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: Sala
        public ActionResult Index()
        {
            if (Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });
            return View("index");
        }
        
        public ActionResult Obrisi(int SalaId)
        {
            Sala S = ctx.Sala.Where(x => x.Id == SalaId).FirstOrDefault();
            List<Oprema> oprema = ctx.Oprema.Where(x => x.SalaId == SalaId).ToList();
            List<Termin> termini = ctx.Termin.Where(x => x.SalaId == SalaId).ToList();
            Rezervacija R = ctx.Rezervacija.Where(x => x.Termin.SalaId == SalaId).FirstOrDefault();

            if (R != null)
            {
                List<Sala> Sale = new List<Sala>();
                Sale = ctx.Sala.Include(x => x.KulturnoSportskiCentar).ToList();
                ViewData["Sala"] = Sale;
                ViewData["error"] = "Ne možete izbrisati " + S.Naziv + " jer postoje rezervacije spremljene za tu salu!";

                return View("Prikazi");
            }

            foreach(var o in oprema)
            {
                ctx.Oprema.Remove(o);
                ctx.SaveChanges();
            }
            foreach(var t in termini)
            {
                ctx.Termin.Remove(t);
                ctx.SaveChanges();
            }
            ctx.Sala.Remove(S);
            ctx.SaveChanges();

            return RedirectToAction("Prikazi");
        }

        public ActionResult Prikazi()
        {
            if (Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });
            List<Sala> Sale = new List<Sala>();
      
          
            Sale = ctx.Sala.Include(x=>x.KulturnoSportskiCentar).ToList();
            ViewData["Sala"] = Sale;
         
            return View("Prikazi");
        }

        public ActionResult Dodaj()
        {
            if (Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });

            DodajSaluVM Model = new DodajSaluVM();
            Model.KulturnoSportskiCentri = UcitajCentre();

            return View("DodajSalu", Model);
        }

        public ActionResult Uredi(int SalaId)
        {
            if (Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });
            
            DodajSaluVM Model = new DodajSaluVM();
            Sala S = ctx.Sala.Where(x => x.Id == SalaId).FirstOrDefault();
            Model.KulturnoSportskiCentri = UcitajCentre();
            Model.Id = S.Id;
            Model.BrojSjedista = S.BrojSjedista;
            Model.isActive = S.isActive;
            Model.Kapacitet = S.Kapacitet;
            Model.KulturnoSportskiCentarId = S.KulturnoSportskiCentarId;
            Model.Naziv = S.Naziv;
            Model.Opis = S.Opis;
            Model.Vrsta = S.Vrsta;
            

            return View("DodajSalu", Model);
        }

        public ActionResult Snimi(DodajSaluVM Model)
        {
            if (Autentifikacija.KorisnikSesija == null)
                return RedirectToAction("Index", "Login", new { area = "" });

            if (!ModelState.IsValid || Model.KulturnoSportskiCentarId == 0)
            {
                Model.KulturnoSportskiCentri = UcitajCentre();
                return View("DodajSalu", Model);
            }

            Sala S;
            if(Model.Id == 0)
            {
                S = new Sala();
                ctx.Sala.Add(S);
                S.Kapacitet = Model.Kapacitet;
                S.KulturnoSportskiCentarId = Model.KulturnoSportskiCentarId;
                S.KulturnoSportskiCentar = ctx.KulturnoSportskiCentar.Where(x => x.Id == Model.KulturnoSportskiCentarId).FirstOrDefault();
                S.Naziv = Model.Naziv;
                S.Opis = Model.Opis;
                S.Vrsta = Model.Vrsta;
                S.BrojSjedista = Model.BrojSjedista;
                ctx.SaveChanges();
            }
            else
            {
                S = ctx.Sala.Where(x => x.Id == Model.Id).FirstOrDefault();
                S.Kapacitet = Model.Kapacitet;
                S.KulturnoSportskiCentarId = Model.KulturnoSportskiCentarId;
                S.KulturnoSportskiCentar = ctx.KulturnoSportskiCentar.Where(x => x.Id == Model.KulturnoSportskiCentarId).FirstOrDefault();
                S.Naziv = Model.Naziv;
                S.Opis = Model.Opis;
                S.Vrsta = Model.Vrsta;
                S.BrojSjedista = Model.BrojSjedista;

                ctx.SaveChanges();

                ctx.KulturnoSportskiCentar.Where(x => x.Id == S.KulturnoSportskiCentarId).FirstOrDefault().BrojSala++;
                ctx.SaveChanges();
            }

            return RedirectToAction("Prikazi");
        }

        private List<SelectListItem> UcitajCentre()
        {
            List<SelectListItem> centri = new List<SelectListItem>();
            centri.Add(new SelectListItem
            {
                Value = 0.ToString(),
                Text = "Izaberi centar!"
            });

            centri.AddRange(ctx.KulturnoSportskiCentar.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Naziv + " - " + x.Grad.Naziv
            }).ToList());

            return centri;
        }
    }


}