using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication2.Models;

namespace Kulturno_sportski_centar.Areas.ModulZaposlenik.Models
{
    public class DodajOpremuVM
    {
        public int Id { get; set; }
        public bool isActive { get; set; }
        [Required(ErrorMessage ="Naziv opreme je obavezno polje!")]
        public string Naziv { get; set; }
        public string Opis { get; set; }
        [Required(ErrorMessage ="Količina opreme je obavezno polje!"), RegularExpression("[0-9]", ErrorMessage = "Polje prihvata samo cifre!")]
        public int Kolicina { get; set; }
        public int SalaId { get; set; }
        public Sala Sala { get; set; }
    }
}