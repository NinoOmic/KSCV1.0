using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Oprema:IEntity
    {
        public int Id { get; set; }
        public bool isActive { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
           
        public int Kolicina { get; set; }
        public int SalaId { get; set; }
        public virtual Sala Sala { get; set; }


    }
}