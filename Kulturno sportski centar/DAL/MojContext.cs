using Kulturno_sportski_centar.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

using WebApplication2.Models;

namespace Kulturno_sportski_centar.DAL
{
    public class MojContext : DbContext
    {
        public MojContext() : base("MojString")
        {

        }

        public DbSet<Dogadjaj> Dogadjaj { get; set; }
        public DbSet<Drzava> Drzava { get; set; }
        public DbSet<Grad> Grad { get; set; }
        public DbSet<RezervacijaZaDogadjaj> RezervacijaZaDogadjaj { get; set; }
        public DbSet<Izvjestaj> Izvjestaj { get; set; }
        public DbSet<Korisnik> Korisnik { get; set; }
        public DbSet<KulturnoSportskiCentar> KulturnoSportskiCentar { get; set; }
        public DbSet<Oprema> Oprema { get; set; }
        public DbSet<Osoba> Osoba { get; set; }
        public DbSet<RadnoMjesto> RadnoMjesto { get; set; }
        public DbSet<Regija> Regija { get; set; }
        public DbSet<Rezervacija> Rezervacija { get; set; }
        public DbSet<Sala> Sala { get; set; }
        public DbSet<Termin> Termin { get; set; }
        public DbSet<Uplata> Uplata { get; set; }
        public DbSet<Uposlenik> Uposlenik { get; set; }
        public DbSet<VrstaDogadjaja> VrstaDogadjaja { get; set; }
        public DbSet<UlogaNaSistemu> UlogaNaSistemu { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Osoba>().HasOptional(x => x.Uposlenik).WithRequired(x => x.Osoba);
            modelBuilder.Entity<Osoba>().HasOptional(x => x.Korisnik).WithRequired(x => x.Osoba);
        }
    }
}