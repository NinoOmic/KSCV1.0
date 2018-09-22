using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models;

namespace Kulturno_sportski_centar.Areas.ModulZaposlenik.Models
{
    public class PrikaziOpremuVM
    {
        public int SalaId { get; set; }

        public class Podaci
        {
            public int Id { get; set; }
            public string Naziv { get; set; }
            public string Opis { get; set; }
            public int Kolicina { get; set; }
            public string Sala { get; set; }
        }

        public List<Podaci> Oprema { get; set; }
    }
}