using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Grad : IEntity
    {
        public int Id { get; set; }
        public bool isActive { get; set; }
        public string Naziv { get; set; }
        public string Oznaka { get; set; }
        public string PostanskiBroj { get; set; }
        public int RegijaId { get; set; }
        public Regija Regija { get; set; }
    }
}
