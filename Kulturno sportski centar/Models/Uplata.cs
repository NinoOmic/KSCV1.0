using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Uplata:IEntity
    {
        public int Id { get; set; }
        public bool isActive { get; set; }
        public float Iznos { get; set; }
        public DateTime DatumUplate { get; set; }
        public string SvrhaUplate { get; set; }
        public int OsobaId { get; set; }
        public Osoba Osoba { get; set; }
    }
}