using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class RadnoMjesto:IEntity
    {
        public int Id { get; set; }
        public bool isActive { get; set; }

        public string Naziv { get; set; }
        public string Opis { get; set; }
        public string Prioritet { get; set; }
        public int DuzinaRadnogVremena { get; set; }
    }
}