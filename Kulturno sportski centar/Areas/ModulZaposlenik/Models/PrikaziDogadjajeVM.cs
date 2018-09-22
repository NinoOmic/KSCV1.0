using Kulturno_sportski_centar.Areas.ModulKorisnik.Models;
using Kulturno_sportski_centar.DAL;
using Kulturno_sportski_centar.Helper;
using Kulturno_sportski_centar.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace Kulturno_sportski_centar.Areas.ModulZaposlenik.Models
{
    public class PrikaziDogadjajeVM
    {
        
        public class DogadjajInfo
        {
            public int DogadjajId { get; set; }
            public string VrstaDogadjaja { get; set; }
            public DateTime Datum { get; set; }
            public TimeSpan VrijemePocetka { get; set; }
            public string Organizator { get; set; }
            public float CijenaUlaza { get; set; }
            public float BrojSjedista { get; set; }
            public string NazivSale { get; set; }
        }

        public List<DogadjajInfo> Dogadjaji;

    }
}