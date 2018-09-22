using Kulturno_sportski_centar.Controllers;
using Kulturno_sportski_centar.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace Kulturno_sportski_centar.ViewModel
{
    public class RegistracijaVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ime je obavezno polje")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Prezime je obavezno polje")]
        public string Prezime { get; set; }

        [Required(ErrorMessage = "Korisnicko ime je obavezno polje/ili već postoji to korisničko ime")]
        [StringLength(100, ErrorMessage = "Korisničko ime može imati minimalno 4 karaktera!", MinimumLength = 4)]
        public string KorisnckoIme { get; set; }

        [Required(ErrorMessage = "Lozinka je obavezno polje"), StringLength(100, ErrorMessage = "Lozinka može imati minimalno 4 karaktera!", MinimumLength = 4)]
        public string Lozinka { get; set; }

        [Required(ErrorMessage = "Email je obavezno polje!"), EmailAddress]
        public string Email { get; set; }

        public string Adresa { get; set; }

        [Phone]
        public string Telefon { get; set; }

        [StringLength(13, MinimumLength = 13, ErrorMessage = "JMBG moram imati 13 znakova")]
        public string JMBG { get; set; }
        public int GradId { get; set; }
        public int UlogaId { get; set; }

        [DataType(DataType.Date)]
        public DateTime DatumRodjenja { get; set; }
        public UlogaNaSistemu Uloga { get; set; }
        public List<SelectListItem> Gradovi { get; set; }
    }
}