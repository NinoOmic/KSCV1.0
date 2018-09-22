using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kulturno_sportski_centar.Areas.ModulAdministrator.Models
{
    public class DrzavaEditModelView
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Naziv je obavezno polje")]
        public string Naziv { get; set; }
        [Required(ErrorMessage ="Oznaka mora imati minimalno 2 znaka a maksimalno 3"),StringLength(3,MinimumLength =2)]
        public string Oznaka { get; set; }
    }
}