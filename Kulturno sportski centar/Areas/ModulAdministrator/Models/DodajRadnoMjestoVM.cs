using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kulturno_sportski_centar.Areas.ModulAdministrator.Models
{
    public class DodajRadnoMjestoVM
    {
        public int RadnoMjestoId { get; set; }
        [Required(ErrorMessage = "Naziv radnog mjesta je obavezan!")]
        public string Naziv { get; set; }
        public string Priotritet { get; set; }
        public string Opis { get; set; }
        [Range(2,8,ErrorMessage ="Dužina radnog vremena može biti samo između 2 i 8 sati!")]
        public int DuzinaRadnogVremena { get; set; }
    }
}