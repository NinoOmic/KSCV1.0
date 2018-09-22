using Kulturno_sportski_centar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Korisnik:IEntity
    {
        public int Id { get; set; }
        public bool isActive { get; set; }

        public DateTime DatumRegistracije { get; set; }
        public int UlogaNaSistemuId { get; set; }
        public UlogaNaSistemu UlogaNaSistemu { get; set; }
        public Osoba Osoba { get; set; }
        public  int OsobaId{get; set;}
        
    }
}