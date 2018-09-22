using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kulturno_sportski_centar.Areas.ModulAdministrator.Models
{
    public class UrediUposlenikVM
    {
        public int UposlenikId { get; set; }
        public string Zvanje { get; set; }
        [Required(ErrorMessage ="Datum zapošljenja je obavezno polje!")]
        public DateTime DatumZaposljenja { get; set; }
        [Required(ErrorMessage = "Radno mjesto je obavezno polje!")]
        public int RadnoMjestoId{get;set;}
        public List<SelectListItem> RadnaMjesta { get; set; }
    }
}