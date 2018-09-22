using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kulturno_sportski_centar.Areas.ModulZaposlenik.Models
{
    public class TerminEditViewModel
    {
       public List<SelectListItem> Sale { get; set; }
        public int SalaId { get; set; }
        public int Id { get; set; }
        [Required(ErrorMessage ="Datum je obavezno polje"),DataType(DataType.Date)]
        public DateTime Datum { get; set; }
        [Required(ErrorMessage = "Pocetak je obavezno polje"), DataType(DataType.Time)]
        public TimeSpan Pocetak { get; set; }
        [Required(ErrorMessage = "Kraj je obavezno polje"), DataType(DataType.Time)]
        public TimeSpan Kraj { get; set; }
    }
}