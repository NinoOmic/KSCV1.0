using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Termin:IEntity
    {
        public int Id { get; set; }
        public bool isActive { get; set; }

        public DateTime Datum { get; set; }
        public TimeSpan Pocetak { get; set; }
        public int SalaId { get; set; }

        public Sala Sala { get; set; }
        public TimeSpan Kraj { get; set; }
        public bool Rezervisan { get; set; }
        public bool Zavrsena { get; set; }




    }
}