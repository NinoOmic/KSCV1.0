using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kulturno_sportski_centar.Areas.ModulAdministrator.Models
{
    public class DodajUloguVM
    {
        public int UlogaId { get; set; }
        [Required(ErrorMessage ="Naziv uloge je obavezno polje!")]
        public string Uloga { get; set; }
    }
}