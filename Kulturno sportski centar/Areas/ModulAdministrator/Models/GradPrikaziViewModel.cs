using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace Kulturno_sportski_centar.Areas.ModulAdministrator.Models
{
    public class GradPrikaziViewModel
    {
        public List<Grad> Gradovi;
        public List<Regija> Regije;
        public List<Drzava> Drzave;
        public int RegijaId { get; set; }

        public IEnumerable<SelectListItem> ListaRegija
        {
            get
            {
                List<SelectListItem> lista = new List<SelectListItem>();
                lista.Add(new SelectListItem { Value = null, Text = "Svi gradovi" });
                lista.AddRange(Regije.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Drzava.Naziv+ " - "+x.Naziv}));

                return lista;
            }
        }
    }
}