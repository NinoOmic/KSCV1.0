using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Sala:IEntity
    {
        public int Id { get; set; }
        public bool isActive { get; set; }

        public string Naziv { get; set; }
        public string Vrsta { get; set; }
        public int  BrojSjedista{ get; set; }
        public string Opis { get; set; }
        public string Kapacitet { get; set; }
        public int KulturnoSportskiCentarId { get; set; }
        public KulturnoSportskiCentar KulturnoSportskiCentar { get; set; }
    }
}