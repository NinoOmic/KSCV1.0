using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models;

namespace Kulturno_sportski_centar.Areas.ModulKorisnik.Models
{
    public class RezervacijaPrikaziViewModel
    {
        public class RezervacijaInfo
        {
            public int Id { get; set; }
            public DateTime Datum { get; set; }
            public TimeSpan Pocetak { get; set; }
            public TimeSpan Kraj { get; set; }
            public string Sala { get; set; }
            public string Korisnik { get; set; }
            public bool Zavrsena { get; set; }
        }
        public List<Termin> Termini;
        public List<Korisnik> Korisnici;
        public List<RezervacijaInfo> Rezervacije;
    }
}