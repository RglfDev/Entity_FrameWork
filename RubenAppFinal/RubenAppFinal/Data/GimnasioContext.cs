using Microsoft.EntityFrameworkCore;
using RubenAppFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace RubenAppFinal.Data
{
    public class GimnasioContext : DbContext
    {
        public GimnasioContext() { }
        public GimnasioContext(DbContextOptions options) : base(options) { }

        public DbSet<Inscripcion> Inscripciones => Set<Inscripcion>();
        public DbSet<Entrenador> Entrenadores => Set<Entrenador>();
        public DbSet<Clase> Clases => Set<Clase>();
        public DbSet<Socio> Socios => Set<Socio>();


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=GymExamen;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inscripcion>()
                .HasOne(c=>c.Socio)
                .WithMany(v=>v.Inscripciones)
                .HasForeignKey(c => c.SocioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Inscripcion>()
                .HasOne(c => c.Clase)
                .WithMany(v => v.Inscripciones)
                .HasForeignKey(c => c.ClaseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Clase>()
                .HasOne(c => c.Entrenador)
                .WithMany(v => v.Clases)
                .HasForeignKey(c => c.EntrenadorId)
                .OnDelete(DeleteBehavior.Restrict);
        

            modelBuilder.Entity<Entrenador>().HasData(
                   new Entrenador { EntrenadorId = 1, Nombre = "Carlos Ruiz", Especialidad = "CrossFit" },
                   new Entrenador { EntrenadorId = 2, Nombre = "Laura Sánchez", Especialidad = "Yoga" },
                   new Entrenador { EntrenadorId = 3, Nombre = "David López", Especialidad = "Boxeo" }
               );

            
            modelBuilder.Entity<Clase>().HasData(
                new Clase { ClaseId = 1, Nombre = "CrossFit Avanzado", Nivel = "Avanzado", PrecioMensual = 50m, EntrenadorId = 1 },
                new Clase { ClaseId = 2, Nombre = "Yoga Principiantes", Nivel = "Básico", PrecioMensual = 30m, EntrenadorId = 2 },
                new Clase { ClaseId = 3, Nombre = "Boxeo Intermedio", Nivel = "Intermedio", PrecioMensual = 40m, EntrenadorId = 3 },
                new Clase { ClaseId = 4, Nombre = "Pilates", Nivel = "Básico", PrecioMensual = 35m, EntrenadorId = 2 }
            );

            
            modelBuilder.Entity<Socio>().HasData(
                new Socio { SocioId = 1, Nombre = "Carlos Pérez", Email = "carlos@email.com" },
                new Socio { SocioId = 2, Nombre = "Ana Gómez", Email = "ana@email.com" },
                new Socio { SocioId = 3, Nombre = "Luis Martínez", Email = "luis@email.com" },
                new Socio { SocioId = 4, Nombre = "Marta Díaz", Email = "marta@email.com" },
                new Socio { SocioId = 5, Nombre = "Javier Torres", Email = "javier@email.com" },
                new Socio { SocioId = 6, Nombre = "Lucía Fernández", Email = "lucia@email.com" },
                new Socio { SocioId = 7, Nombre = "Pedro Morales", Email = "pedro@email.com" }
            );

           
            modelBuilder.Entity<Inscripcion>().HasData(
                
                new Inscripcion { InscripcionId = 1, SocioId = 1, ClaseId = 1, FechaInicio = new DateTime(2025, 9, 1) },
                new Inscripcion { InscripcionId = 2, SocioId = 1, ClaseId = 2, FechaInicio = new DateTime(2025, 9, 1) },

                new Inscripcion { InscripcionId = 3, SocioId = 2, ClaseId = 2, FechaInicio = new DateTime(2025, 10, 1) },
                new Inscripcion { InscripcionId = 4, SocioId = 2, ClaseId = 3, FechaInicio = new DateTime(2025, 10, 1) },

                new Inscripcion { InscripcionId = 5, SocioId = 3, ClaseId = 1, FechaInicio = new DateTime(2025, 10, 1) },
                new Inscripcion { InscripcionId = 6, SocioId = 3, ClaseId = 3, FechaInicio = new DateTime(2025, 10, 1) },

                new Inscripcion { InscripcionId = 7, SocioId = 4, ClaseId = 2, FechaInicio = new DateTime(2025, 10, 1) },
                new Inscripcion { InscripcionId = 8, SocioId = 4, ClaseId = 4, FechaInicio = new DateTime(2025, 10, 1) },

                new Inscripcion { InscripcionId = 9, SocioId = 5, ClaseId = 1, FechaInicio = new DateTime(2025, 10, 1) },
                new Inscripcion { InscripcionId = 10, SocioId = 5, ClaseId = 4, FechaInicio = new DateTime(2025, 10, 1) },

                new Inscripcion { InscripcionId = 11, SocioId = 6, ClaseId = 2, FechaInicio = new DateTime(2025, 10, 1) },
                new Inscripcion { InscripcionId = 12, SocioId = 6, ClaseId = 3, FechaInicio = new DateTime(2025, 10, 1) },

                new Inscripcion { InscripcionId = 13, SocioId = 7, ClaseId = 1, FechaInicio = new DateTime(2025, 10, 1) }
            );
        }
    }
}
