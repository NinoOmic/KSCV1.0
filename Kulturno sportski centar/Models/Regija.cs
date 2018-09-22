using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Regija:IEntity
    {
        public int Id { get; set; }
        public bool isActive { get; set; }
        public string Naziv { get; set; }
        public string Oznaka { get; set; }
        public int DrzavaId { get; set; }
        public Drzava Drzava { get; set; }
    }
}