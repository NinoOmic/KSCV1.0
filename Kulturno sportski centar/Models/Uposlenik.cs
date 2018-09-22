using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Uposlenik:IEntity
    {
        public int Id { get; set; }
        public bool isActive { get; set; }

        public DateTime DatumZaposljenja { get; set; }

        public string Iskustvo { get; set; }
        public string Zvanje { get; set; }
        public int OsobaId { get; set; }
        public Osoba Osoba { get; set; }

        public int RadnoMjestoId { get; set; }
        public RadnoMjesto RadnoMjesto { get; set; }
        
    }
}