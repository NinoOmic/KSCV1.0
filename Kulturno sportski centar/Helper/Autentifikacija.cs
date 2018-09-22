using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace Kulturno_sportski_centar.Helper
{
    public class Autentifikacija
    {
        public static Korisnik KorisnikSesija
        {
            get { return (Korisnik)HttpContext.Current.Session["user"]; }
            set { HttpContext.Current.Session["user"] = value; }
        }
        public static void odjava()
        {
            HttpContext.Current.Session.Remove("user");
            
        }

        
    }
}