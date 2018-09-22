using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kulturno_sportski_centar.Areas.ModulKorisnik.Models
{
    public class RezervacijaDodajModelView
    {
        public int TerminId { get; set; }
        public bool Zavrsena { get; set; }
        public int KorisnikId { get; set; }

    }
}