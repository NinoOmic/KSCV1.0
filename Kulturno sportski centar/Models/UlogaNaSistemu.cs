using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models;

namespace Kulturno_sportski_centar.Models
{
    public class UlogaNaSistemu:IEntity
    {
        public int Id { get; set; }
        public bool isActive { get; set; }

        public string Uloga { get; set; }
    }
}