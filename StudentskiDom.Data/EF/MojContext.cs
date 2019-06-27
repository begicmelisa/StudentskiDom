using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using StudentskiDom.Data.Models;

namespace StudentskiDom.Data.EF
{
   public class MojContext: DbContext
    {
     
        public MojContext(DbContextOptions<MojContext> options)
      : base(options)
        {


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {

            base.OnModelCreating(modelBuilder);



            modelBuilder.Entity<StudentSoba>()

                .HasOne(s => s._Student)

                .WithMany()

                .HasForeignKey(s => s._StudentId)

                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<StudentSoba>()

               .HasOne(so => so._Soba)

               .WithMany()

               .HasForeignKey(so => so._SobaId)

               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StudentSoba>()

               .HasOne(z => z._Zaposlenik)

               .WithMany()

               .HasForeignKey(z => z._ZaposlenikId)

               .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<StudentSoba>()

            //   .HasOne(ag => ag._AkademskaGodina)

            //   .WithMany()

            //   .HasForeignKey(ag => ag._AkademskaGodinaId)

            //   .OnDelete(DeleteBehavior.Restrict);


          


        }


        public DbSet<Drzava> Drzave { get; set; }
		
		public DbSet<Grad> Gradovi { get; set; }
	
		public DbSet<Fakultet> Fakulteti { get; set; }
		public DbSet<Student> Studenti { get; set; }
		public DbSet<Posjetilac> Pojsjetioci { get; set; }
		public DbSet<VrstaZaposlenika> VrsteZaposlenika { get; set; }
		public DbSet<Zaposlenik> Zaposlenici { get; set; }
        public DbSet<KorisnickiNalog> KorisnickiNalozi { get; set; }
		public DbSet<Obavijesti> Obavijesti { get; set; }

		public DbSet<Soba> Sobe { get; set; }
		public DbSet<Sala> Sale { get; set; }
		public DbSet<TipUplate> TipoviUplata { get; set; }
		public DbSet<TipSobe> TipoviSoba { get; set; }
		
		public DbSet<StudentSoba> StudentiSobe { get; set; }
		
		public DbSet<RezervacijaSale> RezervacijeSale { get; set; }
		public DbSet<Uplata> Uplate { get; set; }
  public DbSet<Obracun> Obracun { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{

        //    optionsBuilder.UseSqlServer("Server=localhost;Database=BazaStudentskiDom;Trusted_Connection=True;MultipleActiveResultSets=true;User ID=sa;Password=test");
        //}


    }
}
