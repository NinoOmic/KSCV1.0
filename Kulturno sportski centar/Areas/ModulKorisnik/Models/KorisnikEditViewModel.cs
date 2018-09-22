using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kulturno_sportski_centar.Areas.ModulKorisnik.Models
{
    public class KorisnikEditViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ime je obavezno polje")]
        public string Ime { get; set; }
        [Required(ErrorMessage = "Prezime je obavezno polje")]
        public string Prezime { get; set; }
        [Required(ErrorMessage = "Korisnicko ime je obavezno polje")]
        public string KorisnckoIme { get; set; }
        [Required(ErrorMessage = "Lozinka je obavezno polje")]
        public string Lozinka { get; set; }
        [Required(ErrorMessage = "Email je obavezno polje"),EmailAddress]
        public string Email  { get; set; }
        public string Adresa { get; set; }
        [Phone]
        public string Telefon  {     get; set;  }
        [StringLength(13,MinimumLength =13,ErrorMessage ="JMBG moram imati 13 znakova")]
        public string JMBG { get; set; }
        [DataType(DataType.Date)]
        public DateTime DatumRodjenja { get; set; }
        [DataType(DataType.Date,ErrorMessage ="Datum mora biti formata : dd.mm.yy"), Required(ErrorMessage ="Datum rođenja je obavezan!")]
        public DateTime DatumRegistracije { get; set; }
        public int GradId { get; set; }
        public int UlogaId { get; set; }
        public List<SelectListItem> Uloge { get; set; }
        public List<SelectListItem> Gradovi { get; set; }
    }
}