using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models;

namespace Kulturno_sportski_centar.Models
{
    public class RezervacijaZaDogadjaj : IEntity
    {
        public int Id { get; set; }
        public bool isActive { get; set; }
        public int OsobaId { get; set; }
        public Osoba Osoba { get; set; }
        public Dogadjaj Dogadjaj { get; set; }
        public int DogadjajId { get; set; }
        public float Cijena { get; set; }
    }
}