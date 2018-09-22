using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Rezervacija:IEntity
    {
        public int Id { get; set; }
        public bool isActive { get; set; }
        public int TerminId { get; set; }
        public Termin Termin { get; set; }


        public bool Zavrsena { get; set; }
       

        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }
        

    }
}