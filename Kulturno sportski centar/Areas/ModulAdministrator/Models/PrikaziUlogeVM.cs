using Kulturno_sportski_centar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kulturno_sportski_centar.Areas.ModulAdministrator.Models
{
    public class PrikaziUlogeVM
    {
        public List<UlogaNaSistemu> Uloge { get; set; }
    }
}