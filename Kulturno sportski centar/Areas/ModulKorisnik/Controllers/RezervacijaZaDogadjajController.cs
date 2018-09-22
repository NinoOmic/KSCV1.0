using Kulturno_sportski_centar.Areas.ModulKorisnik.Models;
using Kulturno_sportski_centar.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kulturno_sportski_centar.Helper;
using Kulturno_sportski_centar.Models;

namespace Kulturno_sportski_centar.Areas.ModulKorisnik.Controllers
{
    public class RezervacijaZaDogadjajController : Controller
    {

        MojContext ctx = new MojContext();

        public ActionResult Prikazi()
        {
            PrikaziKarteVM Model = new PrikaziKarteVM();
            Model.Karte = ctx.RezervacijaZaDogadjaj.Where(x => x.OsobaId == Autentifikacija.KorisnikSesija.OsobaId).Select(x => new PrikaziKarteVM.KarteInfo
            {
                Id = x.Id,
                Cijena = x.Dogadjaj.CijenaUlaza,
                Datum = x.Dogadjaj.Termin.Datum,
                Dogadjaj = x.Dogadjaj.VrstaDogadjaja.Naziv,
                Korisnik = x.Id + " - " + x.Osoba.KorisnickoIme
            }).ToList();

            return View("Prikazi", Model);
        }

        public ActionResult PrikaziSveKarte()
        {
            PrikaziKarteVM Model = new PrikaziKarteVM();
            Model.Karte = ctx.RezervacijaZaDogadjaj.Select(x=>new PrikaziKarteVM.KarteInfo 
            {
                Id = x.Id,
                Cijena = x.Dogadjaj.CijenaUlaza,
                Datum = x.Dogadjaj.Termin.Datum,
                Dogadjaj = x.Dogadjaj.VrstaDogadjaja.Naziv,
                Korisnik = x.Id + " - " + x.Osoba.KorisnickoIme 
            }).ToList();

            return View("Prikazi", Model);
        }

        public ActionResult Dodaj(int DogadjajId)
        {
            List<RezervacijaZaDogadjaj> rez = ctx.RezervacijaZaDogadjaj.Where(x=>x.OsobaId == Autentifikacija.KorisnikSesija.OsobaId).ToList();
            foreach(var r in rez)
            {
                if(r.OsobaId == Autentifikacija.KorisnikSesija.OsobaId && r.DogadjajId == DogadjajId)
                {
                    return RedirectToAction("Prikazi");
                }
            }
            RezervacijaZaDogadjaj Karta = new RezervacijaZaDogadjaj();
            Karta.OsobaId = Autentifikacija.KorisnikSesija.OsobaId;
            Karta.Osoba = ctx.Osoba.Where(x => x.Id == Autentifikacija.KorisnikSesija.OsobaId).FirstOrDefault();
            Karta.Cijena = ctx.Dogadjaj.Where(x => x.Id == DogadjajId).FirstOrDefault().CijenaUlaza;
            Karta.Dogadjaj = ctx.Dogadjaj.Where(x => x.Id == DogadjajId).FirstOrDefault();
            ctx.Dogadjaj.Where(x => x.Id == DogadjajId).FirstOrDefault().BrojMjesta--;
            ctx.RezervacijaZaDogadjaj.Add(Karta);
            ctx.SaveChanges();

            return RedirectToAction("Prikazi");
        }

        public ActionResult Obrisi(int KartaId)
        {
            RezervacijaZaDogadjaj Karta = ctx.RezervacijaZaDogadjaj.Where(x => x.Id == KartaId).FirstOrDefault();
            ctx.Dogadjaj.Where(x => x.Id == Karta.DogadjajId).FirstOrDefault().BrojMjesta++;
            ctx.RezervacijaZaDogadjaj.Remove(Karta);
            ctx.SaveChanges();

            return RedirectToAction("Prikazi");
        }

    }
}