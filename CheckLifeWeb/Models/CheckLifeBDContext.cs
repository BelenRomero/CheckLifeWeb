using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CheckLifeWeb.Models
{
    public partial class CheckLifeBDContext : DbContext
    {
        public CheckLifeBDContext()
        {
        }

        public CheckLifeBDContext(DbContextOptions<CheckLifeBDContext> options)
            : base(options)
        {
        }

        public DbSet<Rol> Rols { get; set; }
        public DbSet<Vacunatorio> Vacunatorios { get; set; } //Centro de Vacunacion
        //public DbSet<Enfermero> Enfermeros { get; set; }
        public DbSet<Nacionalidad> Nacionalidades { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<CalendarioVacuna> CalendarioVacunas { get; set; }
        public DbSet<Vacuna> Vacunas { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Medico> Medicos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CheckLifeBD;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }

        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    OnModelCreatingPartial(modelBuilder);
        //}

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
