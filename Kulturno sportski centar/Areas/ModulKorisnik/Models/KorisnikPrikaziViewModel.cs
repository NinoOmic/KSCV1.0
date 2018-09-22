using Kulturno_sportski_centar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;


namespace Kulturno_sportski_centar.Areas.ModulKorisnik.Models
{
    public class KorisnikPrikaziViewModel
    {
        public class KorisnikInfo
        {
            public int Id { get; set; }
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public string UlogaNaSistemu { get; set; }
            public int BrojRezervacija { get; set; }
            public string KorisnickoIme { get; set; }
        }
        
        public List<KorisnikInfo> Korisnici;
        public List<UlogaNaSistemu> UlogeNaSistemu;
        public int UlogaNaSistemuId { get; set; }
        public IEnumerable<SelectListItem> ListaUloga
        {
            get
            {
                List<SelectListItem> lista = new List<SelectListItem>();
                //SelectListItem objekat = new SelectListItem();
                //lista.Add(new SelectListItem { Value = null, Text = "Sve uloge" });
                //foreach (UlogaNaSistemu x in UlogeNaSistemu)
                //{
                //    objekat.Value = x.Id.ToString();
                //    objekat.Text = x.Uloga;
                //    lista.Add(objekat);
                //}



                lista.Add(new SelectListItem { Value =null, Text = "Sve uloge" });
                lista.AddRange(UlogeNaSistemu.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Uloga }));

                return lista;
            }
        }
    }
}