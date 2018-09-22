using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Dogadjaj : IEntity
    {
        public int Id { get; set; }
        public bool isActive { get; set; }
        public int TerminId { get; set; }
        public Termin Termin { get; set; }
        public int BrojMjesta { get; set; }
        public float CijenaUlaza { get; set; }
        public int OrganizatorId { get; set; }
        public Osoba Organizator { get; set; }
        public int VrstaDogadjajaId { get; set; }
        public VrstaDogadjaja VrstaDogadjaja { get; set; }

    }
}