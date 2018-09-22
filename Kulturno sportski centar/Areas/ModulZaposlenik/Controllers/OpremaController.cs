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
    public class OpremaController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: ModulZaposlenik/Oprema

        public ActionResult Prikazi(int SalaId)
        {
            if(Autentifikacija.KorisnikSesija == null)
            {
                return RedirectToAction("Index", "Login", new { area = "" });
            }
            PrikaziOpremuVM Model = new PrikaziOpremuVM();
            Model.SalaId = SalaId;
            Model.Oprema = ctx.Oprema.Where(x => x.SalaId == SalaId).Select(x => new PrikaziOpremuVM.Podaci
            {
                Sala = x.Sala.Naziv,
                Id = x.Id,
                Naziv = x.Naziv,
                Kolicina = x.Kolicina,
                Opis = x.Opis
            }).ToList();

            return View("Prikazi",Model);
        }

        public ActionResult Dodaj(int SalaId)
        {
            if (Autentifikacija.KorisnikSesija == null)
            {
                return RedirectToAction("Index", "Login", new { area = "" });
            }
            DodajOpremuVM Model = new DodajOpremuVM();
            Model.SalaId = SalaId;
            Model.Sala = ctx.Sala.Where(x => x.Id == SalaId).FirstOrDefault();

            return View("Dodaj", Model);
        }

        public ActionResult Uredi(int OpremaId)
        {
            if (Autentifikacija.KorisnikSesija == null)
            {
                return RedirectToAction("Index", "Login", new { area = "" });
            }
            DodajOpremuVM Model = new DodajOpremuVM();
            Oprema O = ctx.Oprema.Where(x => x.Id == OpremaId).FirstOrDefault();
            Model.Id = O.Id;
            Model.isActive = O.isActive;
            Model.Kolicina = O.Kolicina;
            Model.Naziv = O.Naziv;
            Model.Opis = O.Opis;
            Model.Sala = O.Sala;
            Model.SalaId = O.SalaId;

            return View("Dodaj",Model);
        }

        public ActionResult Obrisi(int OpremaId)
        {
            if (Autentifikacija.KorisnikSesija == null)
            {
                return RedirectToAction("Index", "Login", new { area = "" });
            }
            Oprema O = ctx.Oprema.Where(x => x.Id == OpremaId).FirstOrDefault();
            int pom = O.SalaId;
            ctx.Oprema.Remove(O);
            ctx.SaveChanges();

            return RedirectToAction("Prikazi", new { SalaId = pom });
        }

        public ActionResult Snimi(DodajOpremuVM Model)
        {
            if (Autentifikacija.KorisnikSesija == null)
            {
                return RedirectToAction("Index", "Login", new { area = "" });
            }
            if (Model.SalaId == 0)
            {
                return View("Dodaj");
            }
            Oprema O;
            if(Model.Id == 0)
            {
                O = new Oprema();
                ctx.Oprema.Add(O);
                O.SalaId = Model.SalaId;
                O.Sala = ctx.Sala.Where(x => x.Id == Model.SalaId).FirstOrDefault();
                O.isActive = true;
                O.Kolicina = Model.Kolicina;
                O.Naziv = Model.Naziv;
                O.Opis = Model.Opis;
               
                ctx.SaveChanges();
            }
            else
            {
                O = ctx.Oprema.Where(x => x.Id == Model.Id).FirstOrDefault();
                O.isActive = true;
                O.Kolicina = Model.Kolicina;
                O.Naziv = Model.Naziv;
                O.Opis = Model.Opis;
                O.SalaId = Model.SalaId;
                O.Sala = ctx.Sala.Where(x => x.Id == Model.SalaId).FirstOrDefault();
                ctx.SaveChanges();
            }

            return RedirectToAction("Prikazi", new { SalaId = Model.SalaId});
        }
    }
}