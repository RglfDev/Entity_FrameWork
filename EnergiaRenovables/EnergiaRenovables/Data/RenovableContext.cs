using EnergiaRenovables.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergiaRenovables.Data
{
    public class RenovableContext : DbContext
    {
        public RenovableContext() { }

        public RenovableContext(DbContextOptions options) : base(options) { }

        public DbSet<Ubicacion> Ubicaciones => Set<Ubicacion>();
        public DbSet<TipoEnergia> TipoEnergias => Set<TipoEnergia>();
        public DbSet<Proyecto> Proyectos => Set<Proyecto>();
        public DbSet<Inversor> Inversores => Set<Inversor>();
        public DbSet<Inversion> Inversiones => Set<Inversion>();
        public DbSet<Informe> Informes => Set<Informe>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=EcoEnergyDatabase;Trusted_Connection=True;");
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inversion>().HasKey(ap => new { ap.InversorId, ap.ProyectoId });

            modelBuilder.Entity<Proyecto>()
                .HasOne(x=> x.TipoEnergia)
                .WithMany(c=> c.Proyectos)
                .HasForeignKey(v=> v.TipoEnergiaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Proyecto>()
                .HasOne(x => x.Ubicacion)
                .WithMany(c => c.Proyectos)
                .HasForeignKey(v => v.UbicacionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Inversion>()
                .HasOne(x => x.Proyecto)
                .WithMany(c => c.Inversiones)
                .HasForeignKey(v => v.ProyectoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Inversion>()
                .HasOne(z => z.Inversor)
                .WithMany(c => c.Inversiones)
                .HasForeignKey(v => v.InversorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Informe>()
                .HasOne(x => x.Proyecto)
                .WithMany(c => c.Informes)
                .HasForeignKey(v => v.ProyectoId)
                .OnDelete(DeleteBehavior.Restrict);


            
            modelBuilder.Entity<TipoEnergia>().HasData(
                new TipoEnergia { Id = 1, Nombre = "Solar" },
                new TipoEnergia { Id = 2, Nombre = "Eólica" },
                new TipoEnergia { Id = 3, Nombre = "Hidráulica" },
                new TipoEnergia { Id = 4, Nombre = "Geotérmica" },
                new TipoEnergia { Id = 5, Nombre = "Biomasa" }
            );

            
            modelBuilder.Entity<Ubicacion>().HasData(
                new Ubicacion { Id = 1, Ciudad = "Sevilla", Pais = "España" },
                new Ubicacion { Id = 2, Ciudad = "Hamburgo", Pais = "Alemania" },
                new Ubicacion { Id = 3, Ciudad = "Santiago", Pais = "Chile" },
                new Ubicacion { Id = 4, Ciudad = "Lisboa", Pais = "Portugal" },
                new Ubicacion { Id = 5, Ciudad = "Toronto", Pais = "Canadá" }
            );

            
            modelBuilder.Entity<Proyecto>().HasData(
                new Proyecto { Id = 1, Nombre = "Solar Sur", InversionTotal = 500000, TipoEnergiaId = 1, UbicacionId = 1 },
                new Proyecto { Id = 2, Nombre = "Viento Norte", InversionTotal = 850000, TipoEnergiaId = 2, UbicacionId = 2 },
                new Proyecto { Id = 3, Nombre = "Río Verde", InversionTotal = 1200000, TipoEnergiaId = 3, UbicacionId = 3 },
                new Proyecto { Id = 4, Nombre = "Calor Tierra", InversionTotal = 650000, TipoEnergiaId = 4, UbicacionId = 4 },
                new Proyecto { Id = 5, Nombre = "BioFuturo", InversionTotal = 900000, TipoEnergiaId = 5, UbicacionId = 5 }
            );

            
            modelBuilder.Entity<Inversor>().HasData(
                new Inversor { Id = 1, Nombre = "Inversiones Globales S.A.", CapitalDisponible = 2000000 },
                new Inversor { Id = 2, Nombre = "EcoCapital", CapitalDisponible = 1500000 },
                new Inversor { Id = 3, Nombre = "GreenFund", CapitalDisponible = 1800000 },
                new Inversor { Id = 4, Nombre = "SolarTrust", CapitalDisponible = 1200000 },
                new Inversor { Id = 5, Nombre = "BlueEnergy", CapitalDisponible = 2200000 }
            );

            
            modelBuilder.Entity<Inversion>().HasData(
                new Inversion { InversorId = 1, ProyectoId = 1, MontoInvertido = 250000 },
                new Inversion { InversorId = 2, ProyectoId = 2, MontoInvertido = 400000 },
                new Inversion { InversorId = 3, ProyectoId = 3, MontoInvertido = 600000 },
                new Inversion { InversorId = 4, ProyectoId = 4, MontoInvertido = 300000 },
                new Inversion { InversorId = 5, ProyectoId = 5, MontoInvertido = 500000 }
            );

           
            modelBuilder.Entity<Informe>().HasData(
                new Informe { Id = 1, ProyectoId = 1, Fecha = new DateTime(2024, 6, 1), EnergiaGeneradaMWh = 1200 },
                new Informe { Id = 2, ProyectoId = 2, Fecha = new DateTime(2024, 7, 1), EnergiaGeneradaMWh = 2300 },
                new Informe { Id = 3, ProyectoId = 3, Fecha = new DateTime(2024, 8, 1), EnergiaGeneradaMWh = 3100 },
                new Informe { Id = 4, ProyectoId = 4, Fecha = new DateTime(2024, 9, 1), EnergiaGeneradaMWh = 1800 },
                new Informe { Id = 5, ProyectoId = 5, Fecha = new DateTime(2024, 10, 1), EnergiaGeneradaMWh = 2600 }
            );
        }
    }
}
