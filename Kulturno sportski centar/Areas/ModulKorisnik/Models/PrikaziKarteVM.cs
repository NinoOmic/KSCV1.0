using Kulturno_sportski_centar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kulturno_sportski_centar.Areas.ModulKorisnik.Models
{
    public class PrikaziKarteVM
    {
        public class KarteInfo
        {
            public int Id { get; set; }
            public string Dogadjaj { get; set; }
            public float Cijena { get; set; }
            public DateTime Datum { get; set; }
            public string Korisnik { get; set; }
        }
        public List<KarteInfo> Karte;
    }
}