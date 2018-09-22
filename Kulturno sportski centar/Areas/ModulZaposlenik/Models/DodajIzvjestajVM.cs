using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication2.Models;

namespace Kulturno_sportski_centar.Areas.ModulZaposlenik.Models
{
    public class DodajIzvjestajVM
    {
        public int Id { get; set; }
        public bool isActive { get; set; }
        public DateTime Datum { get; set; }
        [Required(ErrorMessage ="Opis je obavezno polje!")]
        public string Opis { get; set; }
        [Required(ErrorMessage = "Vrsta je obavezno polje!")]
        public string Vrsta { get; set; }
        public Uposlenik Uposlenik { get; set; }
        public int UposlenikId { get; set; }
    }
}