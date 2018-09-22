using Kulturno_sportski_centar.Areas.ModulKorisnik.Models;
using Kulturno_sportski_centar.DAL;
using Kulturno_sportski_centar.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace Kulturno_sportski_centar.Areas.ModulZaposlenik.Models
{
    public class DodajDogadjajVM
    {
        public int DogadjajId { get; set; }
        public bool isActive { get; set; }
        public int TerminId { get; set; }
        public Termin Termin { get; set; }
        [Required(ErrorMessage ="Broj mjesta je obavezno polje!")]
        public int BrojMjesta { get; set; }
        [Required(ErrorMessage = "Cijena ulaznice je obavezno polje!")]
        public float CijenaUlaza { get; set; }
        public int OrganizatorId { get; set; }
        public Osoba Organizator { get; set; }
        [Required(ErrorMessage = "Vrsta događaja je obavezno polje!")]
        public int VrstaDogadjajaId { get; set; }
        public List<SelectListItem> VrsteDogadjaja { get; set; }
    }
}