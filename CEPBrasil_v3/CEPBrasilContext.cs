using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CEPBrasil_v3
{
    class CEPBrasilContext : DbContext
    {

        public DbSet<Estado> Estados { get; set; }

        public DbSet<Cidade> Cidades { get; set; }

        public DbSet<Bairro> Bairros { get; set; }
        public DbSet<Logradouro> Logradouros { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CEPBrasilDB;Trusted_Connection=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Estado>()
                .HasKey(e => new { e.Id_Estado });

            modelBuilder
               .Entity<Cidade>()
               .HasKey(c => new { c.Id_Estado, c.Id_Cidade });

            modelBuilder
            .Entity<Cidade>()
            .Property(c => c.Id_Cidade).ValueGeneratedOnAdd();

            modelBuilder
               .Entity<Bairro>()
               .HasKey(c => new { c.Id_Estado, c.Id_Cidade, c.Id_Bairro });


            modelBuilder
            .Entity<Bairro>()
            .Property(c => c.Id_Bairro).ValueGeneratedOnAdd();

            modelBuilder
               .Entity<Logradouro>()
               .HasKey(c => new { c.Id_Estado, c.Id_Cidade, c.Id_Bairro, c.Id_Logradouro });


            modelBuilder
            .Entity<Logradouro>()
            .Property(c => c.Id_Logradouro).ValueGeneratedOnAdd();
        }

    }
}
