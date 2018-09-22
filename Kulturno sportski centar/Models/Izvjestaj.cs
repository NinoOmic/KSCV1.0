using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Izvjestaj : IEntity
    {
        public int Id { get; set; }
        public bool isActive { get; set; }
        public DateTime Datum { get; set; }
        public string Opis { get; set; }
        public string Vrsta { get; set; }
        public Uposlenik Uposlenik { get; set; }
        public int UposlenikId { get; set; }
    }
}