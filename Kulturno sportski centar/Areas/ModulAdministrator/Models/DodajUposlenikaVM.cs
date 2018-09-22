using Kulturno_sportski_centar.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace Kulturno_sportski_centar.Areas.ModulAdministrator.Models
{
    public class DodajUposlenikaVM
    {
        public int UposlenikId { get; set; }
        public DateTime DatumZaposljenja { get; set; }
        public string Iskustvo { get; set; }
        public string Zvanje { get; set; }
        public int OsobaId { get; set; }
        public virtual Osoba Osoba { get; set; }
        [Required(ErrorMessage ="Radno mjesto je obavezno polje!")]
        public int RadnoMjestoId { get; set; }
        public List<SelectListItem> RadnaMjesta { get; set; }

        public int Id { get; set; }

        [Required(ErrorMessage = "Ime je obavezno polje")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Prezime je obavezno polje")]
        public string Prezime { get; set; }

        [Required(ErrorMessage = "Korisnicko ime je obavezno polje!")]
        public string KorisnckoIme { get; set; }

        [Required(ErrorMessage = "Lozinka je obavezno polje")]
        public string Lozinka { get; set; }

        [Required(ErrorMessage = "Email je obavezno polje!"), EmailAddress]
        public string Email { get; set; }

        public string Adresa { get; set; }

        [Phone]
        public string Telefon { get; set; }

        [StringLength(13, MinimumLength = 13, ErrorMessage = "JMBG moram imati 13 znakova")]
        public string JMBG { get; set; }
        public int GradId { get; set; }
        [Required(ErrorMessage ="Uloga je obavezno polje!")]
        public int UlogaId { get; set; }
        public UlogaNaSistemu Uloga { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Datum mora biti formata : dd.mm.yy"), Required(ErrorMessage = "Datum rođenja je obavezan!")]
        public DateTime DatumRodjenja { get; set; }
        public List<SelectListItem> Gradovi { get; set; }
    }
}