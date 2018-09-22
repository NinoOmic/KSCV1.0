using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kulturno_sportski_centar.Areas.ModulZaposlenik.Models
{
    public class DodajVrstuDogadjajaVM
    {
        public int VrstaDogadjajaId { get; set; }
        public bool IsActive { get; set; }

        [Required(ErrorMessage ="Naziv je obavezno polje!")]
        public string Naziv { get; set; }
        public string Opis { get; set; }
    }
}