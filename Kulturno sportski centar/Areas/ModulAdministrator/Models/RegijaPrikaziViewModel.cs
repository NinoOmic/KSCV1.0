using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace Kulturno_sportski_centar.Areas.ModulAdministrator.Models
{
    public class RegijaPrikaziViewModel
    {
       public List<Regija> Regije;
        public List<Drzava> Drzave;
        public int DrzavaId { get; set;  }
        public IEnumerable<SelectListItem> ListaDrzava {
            get {
                List<SelectListItem> lista = new List<SelectListItem>();
                lista.Add(new SelectListItem { Value = null, Text = "Sve države" });
                lista.AddRange(Drzave.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }));

                return lista;
            }
        }
    }
}