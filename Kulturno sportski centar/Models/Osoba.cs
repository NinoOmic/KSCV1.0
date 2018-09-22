using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Osoba:IEntity
    {
        public int Id { get; set; }
        public bool isActive { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }

        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public int GradId { get; set; }
        public Grad Grad { get; set; }
        public string Adresa { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string JMBG { get; set; }
        public Korisnik Korisnik { get; set; }
        public Uposlenik Uposlenik { get; set; }      

     
       
    }
}