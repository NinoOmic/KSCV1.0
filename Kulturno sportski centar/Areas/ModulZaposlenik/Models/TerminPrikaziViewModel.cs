using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace Kulturno_sportski_centar.Areas.ModulZaposlenik.Models
{
    public class TerminPrikaziViewModel
    {
        public List<Termin> Termini;
        
        public List<Sala> sale;
        public TimeSpan kraj = new TimeSpan(23, 0, 0);
        public TimeSpan pocetak = new TimeSpan(10, 0, 0);
        public TimeSpan pocetna = new TimeSpan(10, 0, 0);
        public TimeSpan uvecalo = new TimeSpan(1, 0, 0);
        public DateTime Datum;

        public int SalaId { get; set; }
        public IEnumerable<SelectListItem> ListaSala
        {
            get
            {
                List<SelectListItem> lista = new List<SelectListItem>();
                lista.Add(new SelectListItem{ Value = null, Text = "Sve sale" });
                lista.AddRange(sale.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.KulturnoSportskiCentar.Naziv + " - " + x.Naziv }));

                return lista;
            }
        }
    }
}

