using Kulturno_sportski_centar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kulturno_sportski_centar.Areas.ModulKorisnik.Models
{
    public class KorisnikPrikaziDetaljnoViewModel
    {



        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string UlogaNaSistemu { get; set; }
        public int BrojRezervacija { get; set; }
        public string KorisnickoIme { get; set; }
        public DateTime DatumRegistracije { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Adresa { get; set; }
        public string JMBG { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string Grad { get; set; }

    }
}
