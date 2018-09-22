using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace Kulturno_sportski_centar.Areas.ModulZaposlenik.Models
{
    public class DodajSaluVM
    {
        public int Id { get; set; }
        public bool isActive { get; set; }

        [Required(ErrorMessage ="Ime sale je obavezno polje!")]
        public string Naziv { get; set; }

        [Required(ErrorMessage = "Vrsta sale je obavezno polje!")]
        public string Vrsta { get; set; }

        [Required(ErrorMessage = "Broj sjedišta je obavezno polje!")]
        public int BrojSjedista { get; set; }
        public string Opis { get; set; }
        public string Kapacitet { get; set; }

        [Required(ErrorMessage = "Kojem KSC-u pripada sala?")]
        public int KulturnoSportskiCentarId { get; set; }
        public List<SelectListItem> KulturnoSportskiCentri { get; set; }
    }
}