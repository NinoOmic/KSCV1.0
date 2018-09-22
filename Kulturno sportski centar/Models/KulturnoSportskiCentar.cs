using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class KulturnoSportskiCentar:IEntity
    {
        public int Id { get; set; }
        public bool isActive { get; set; }
        public string Naziv { get; set; }
        public string Adresa { get; set; }
        public string Telefon { get; set; }
        public string Faks{ get; set; }
        public string Email { get; set; }

        public string Web { get; set; }
        public int BrojSala { get; set; }

        public int GradId { get; set; }
        public Grad Grad { get; set; }

        

    }
}