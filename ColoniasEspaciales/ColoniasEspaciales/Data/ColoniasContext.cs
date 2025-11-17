using ColoniasEspaciales.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColoniasEspaciales.Data
{
    public class ColoniasContext : DbContext
    {
        public ColoniasContext() { }
        public ColoniasContext(DbContextOptions<ColoniasContext> options) :base(options) { }

        public DbSet<Recurso> Recursos => Set<Recurso>();
        public DbSet<Evento> Eventos => Set<Evento>();
        public DbSet<ColoniaRecurso> ColoniaRecursos => Set<ColoniaRecurso>();
        public DbSet<Habitante> Habitantes => Set<Habitante>();
        public DbSet<Colonia> Colonias => Set<Colonia>();
        public DbSet<Planeta> Planetas => Set<Planeta>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=PlanetasDatabase;Trusted_Connection=True;");
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Evento>()
                .HasOne(a => a.Colonia)
                .WithMany(b => b.Eventos)
                .HasForeignKey(b => b.ColoniaId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ColoniaRecurso>()
                .HasOne(a => a.Colonia)
                .WithMany(b => b.ColoniaRecursos)
                .HasForeignKey(x => x.ColoniaId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ColoniaRecurso>()
                .HasOne(a => a.Recurso)
                .WithMany(z => z.ColoniaRecursos)
                .HasForeignKey(x => x.RecursoId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Habitante>()
               .HasOne(a => a.Colonia)
               .WithMany(z => z.Habitantes)
               .HasForeignKey(x => x.ColoniaId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Colonia>()
               .HasOne(a => a.Planeta)
               .WithMany(z => z.Colonias)
               .HasForeignKey(x => x.PlanetaId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Planeta>().HasData(
    new Planeta { Id = 1, Nombre = "Aurelia", Tipo = "Terrestre", TemperaturaPromedio = 22.5 },
    new Planeta { Id = 2, Nombre = "Zephyria", Tipo = "Gaseoso", TemperaturaPromedio = -80.0 }
);

            
            modelBuilder.Entity<Colonia>().HasData(
                new Colonia { Id = 1, Nombre = "Nova Terra", PlanetaId = 1, NivelSostenibilidad = "Alta" },
                new Colonia { Id = 2, Nombre = "Viento Azul", PlanetaId = 2, NivelSostenibilidad = "Media" }
            );

            
            modelBuilder.Entity<Habitante>().HasData(
                new Habitante { Id = 1, Nombre = "Lucía Vega", Rol = "Comandante", ColoniaId = 1 },
                new Habitante { Id = 2, Nombre = "Carlos Luna", Rol = "Ingeniero", ColoniaId = 1 },
                new Habitante { Id = 3, Nombre = "Marta Sol", Rol = "Bióloga", ColoniaId = 2 }
            );

            
            modelBuilder.Entity<Recurso>().HasData(
                new Recurso { Id = 1, Nombre = "Agua", CantidadTotal = 10000 },
                new Recurso { Id = 2, Nombre = "Oxígeno", CantidadTotal = 8000 },
                new Recurso { Id = 3, Nombre = "Metal", CantidadTotal = 12000 }
            );

            
            modelBuilder.Entity<ColoniaRecurso>().HasData(
                new ColoniaRecurso { Id = 1, ColoniaId = 1, RecursoId = 1, Cantidad = 5000 },
                new ColoniaRecurso { Id = 2, ColoniaId = 1, RecursoId = 2, Cantidad = 4000 },
                new ColoniaRecurso { Id = 3, ColoniaId = 2, RecursoId = 1, Cantidad = 3000 },
                new ColoniaRecurso { Id = 4, ColoniaId = 2, RecursoId = 3, Cantidad = 6000 }
            );

            
            modelBuilder.Entity<Evento>().HasData(
                new Evento { Id = 1, Tipo = "Tormenta Solar", Descripcion = "Una tormenta afecta las comunicaciones.", Fecha = new DateTime(2124, 3, 15), ColoniaId = 1 },
                new Evento { Id = 2, Tipo = "Descubrimiento", Descripcion = "Se ha encontrado un nuevo mineral.", Fecha = new DateTime(2124, 5, 22), ColoniaId = 2 }
            );
        }
    }
}
