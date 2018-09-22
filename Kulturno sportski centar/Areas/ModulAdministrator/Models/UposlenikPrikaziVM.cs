using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models;

namespace Kulturno_sportski_centar.Areas.ModulAdministrator.Models
{
    public class UposlenikPrikaziVM
    {
        public class UposlenikInfo
        {
            public int UposlenikId { get; set; }
            public string ImePrezime { get; set; }
            public DateTime DatumZaposljenja { get; set; }
            public string Iskustvo { get; set; }
            public string Zvanje { get; set; }
            public string RadnoMjesto { get; set; }
            public int OsobaId { get; set; }
        }

        public List<UposlenikInfo> Uposlenici;
    }
}