using Kulturno_sportski_centar.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApplication2.Models;
using Kulturno_sportski_centar.ViewModel;
using System.Net;
using System.Net.Mail;
using System.Windows.Input;
using System.Windows.Markup;

namespace Kulturno_sportski_centar.Controllers
{
    public class RegistracijaController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: Registracija
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Pocetna()
        {
            return View("Pocetna");
        }

        public ActionResult Registruj()
        {

            RegistracijaVM Model = new RegistracijaVM();
            Model.Gradovi = UcitajGradove();

            return View("Registracija", Model);
        }

        public ActionResult Snimi(RegistracijaVM Model) //izmjeniti početnu stranicu za korisnike :D
        {

            Osoba Osoba = ctx.Osoba.Where(x => x.KorisnickoIme == Model.KorisnckoIme).FirstOrDefault();

            if (Osoba != null)
            {
                Model.Gradovi = UcitajGradove();
                ViewData["error"] = "Korisničko ime već postoji!";
                
                return View("Registracija", Model);
            }

            if(Model.GradId == 0)
            {
                Model.Gradovi = UcitajGradove();
                ViewData["error2"] = "Izaberi grad!";

                return View("Registracija", Model);
            }

            Osoba Osoba2 = ctx.Osoba.Where(x => x.Email == Model.Email).FirstOrDefault();

            if (Osoba2 != null)
            {
                Model.Gradovi = UcitajGradove();
                ViewData["error1"] = "Taj E-mail se već koristi!";

                return View("Registracija", Model);
            }

            List<Osoba> Osobe = ctx.Osoba.ToList();

            if (!ModelState.IsValid)
            {
                Model.Gradovi = UcitajGradove();
                return View("Registracija", Model);
            }

            Korisnik K = new Korisnik();
            Osoba O = new Osoba();
            if(Model.DatumRodjenja == null)
            {
                Model.Gradovi = UcitajGradove();
                return View("Registracija", Model);
            }
            DateTime sada = Model.DatumRodjenja;

            
            ctx.Osoba.Add(O);

            O.Grad = ctx.Grad.Where(x => x.Id == Model.GradId).FirstOrDefault();
            O.GradId = Model.GradId;
            O.Ime = Model.Ime;
            O.Prezime = Model.Prezime;
            O.KorisnickoIme = Model.KorisnckoIme;
            O.Lozinka = Model.Lozinka;
            O.JMBG = Model.JMBG;
            O.Telefon = Model.Telefon;
            O.DatumRodjenja = sada;
            O.Adresa = Model.Adresa;
            O.Email = Model.Email;
            ctx.SaveChanges();

            ctx.Korisnik.Add(K);
            K.OsobaId = ctx.Osoba.Where(x => x.KorisnickoIme == O.KorisnickoIme).FirstOrDefault().Id;
            K.Osoba = ctx.Osoba.Where(x => x.KorisnickoIme == O.KorisnickoIme).FirstOrDefault();
            K.DatumRegistracije = DateTime.Now;
            K.UlogaNaSistemu = ctx.UlogaNaSistemu.Where(x => x.Id == 2).FirstOrDefault();
            K.UlogaNaSistemuId = 2;
            ctx.SaveChanges();

            return View("Pocetna");
        }

        public ActionResult Provjeri(string korisnickoIme)
        {
            if (!ctx.Osoba.Any(x => x.KorisnickoIme == korisnickoIme))
                return Json($"Korisničko ime " + korisnickoIme + " je zauzeto!");

            return Json(true);
        }
        private List<SelectListItem> UcitajGradove()
        {
            List<SelectListItem> Gradovi = new List<SelectListItem>();
            Gradovi.Add(new SelectListItem
            {
                Value = 0.ToString(),
                Text = "Odaberi grad!"
            });

            Gradovi.AddRange(ctx.Grad.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Naziv
            }).ToList());

            return Gradovi;
        }



        //[HttpPost]


        //private void SendActivationEmail(string email)
        //{

        //    using (MailMessage mm = new MailMessage("nino.omic@edu.fit.ba", email))
        //    {
        //        mm.Subject = "Account Activation";
        //        string body = "Hello glupane, ";
        //        body += "<br /><br />Please click the following link to activate your account";
        //        body += "<br />EMAIL";
        //        body += "<br /><br />Thanks";
        //        mm.Body = body;
        //        mm.IsBodyHtml = true;
        //        SmtpClient smtp = new SmtpClient();
        //        smtp.Host = "nino.omic@edu.fit.ba";
        //        smtp.EnableSsl = true;
        //        NetworkCredential NetworkCred = new NetworkCredential("nino.omic@edu.fit.ba", "HawlettPackard20");
        //        smtp.UseDefaultCredentials = true;
        //        smtp.Credentials = NetworkCred;
        //        smtp.Port = 587;
        //        smtp.Send(mm);
        //    }
        //}

    }
}