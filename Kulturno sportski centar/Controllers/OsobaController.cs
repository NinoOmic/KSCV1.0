using Kulturno_sportski_centar.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using System.Data.Entity;

namespace Kulturno_sportski_centar.Controllers
{
    public class OsobaController : Controller
    {
        MojContext ctx = new MojContext();
     
        public ActionResult Prikazi()
        {
            List<Osoba> Osobe = ctx.Osoba.Include(x => x.Grad)
                .Include(x=>x.Grad.Regija)
                .Include(x => x.Grad.Regija.Drzava)
                .ToList();
            ViewData["nesto"] = Osobe;
            return View("Prikazi");

        }

        public ActionResult    Dodaj()
        {
            return View("Dodaj");

        }
        public ActionResult Snimi(string Ime,string Prezime, DateTime DatumRodjenja,string JMBG, string Telefon, string KorisnckoIme, string Grad, string Adresa, string Email, string Lozinka)
        {
            Osoba a = new Osoba();
            a.Ime = Ime;
            
            a.Prezime = Prezime;
            a.DatumRodjenja = DatumRodjenja;
            a.KorisnickoIme = KorisnckoIme;
            a.JMBG = JMBG;
            a.Telefon = Telefon;
            a.Adresa = Adresa;
            a.Email = Email;
            a.Lozinka = Lozinka;
            a.GradId = 5;
            ctx.Osoba.Add(a);
            ctx.SaveChanges();
            return RedirectToAction("Prikazi");
        }
    }
}