using LibreriaAsync.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaAsync.Data
{
    public class LibreriaDBContext : DbContext
    {
        public DbSet<Libro> Libros => Set<Libro>();
        public DbSet<Prestamo> Prestamos => Set<Prestamo>();
        public DbSet<Autor> Autores => Set<Autor>();


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=LibreriaAsyncDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Libro>()
               .HasOne(x => x.Autor)
               .WithMany(c => c.Libros)
               .HasForeignKey(x => x.AutorId);

            modelBuilder.Entity<Prestamo>()
                .HasOne(x => x.Libro)
                .WithMany(c => c.Prestamos)
                .HasForeignKey(c => c.LibroId);

            modelBuilder.Entity<Autor>().HasData(
        new Autor { AutorId = 1, Nombre = "Gabriel García Márquez", Nacionalidad = "Colombia" },
        new Autor { AutorId = 2, Nombre = "Jane Austen", Nacionalidad = "Reino Unido" },
        new Autor { AutorId = 3, Nombre = "Haruki Murakami", Nacionalidad = "Japón" }
    );

            // Libros
            modelBuilder.Entity<Libro>().HasData(
                new Libro { LibroId = 1, Titulo = "Cien Años de Soledad", Genero = "Realismo mágico", AutorId = 1 },
                new Libro { LibroId = 2, Titulo = "El Amor en los Tiempos del Cólera", Genero = "Romance", AutorId = 1 },
                new Libro { LibroId = 3, Titulo = "Orgullo y Prejuicio", Genero = "Romance", AutorId = 2 },
                new Libro { LibroId = 4, Titulo = "Kafka en la Orilla", Genero = "Ficción surrealista", AutorId = 3 }
            );

            // Préstamos
            modelBuilder.Entity<Prestamo>().HasData(
                new Prestamo
                {
                    PrestamoId = 1,
                    LibroId = 1,
                    Usuario = "María López",
                    FechaPrestamo = new DateTime(2025, 11, 1),
                    FechaDevolucion = null
                },
                new Prestamo
                {
                    PrestamoId = 2,
                    LibroId = 3,
                    Usuario = "Juan Pérez",
                    FechaPrestamo = new DateTime(2025, 10, 25),
                    FechaDevolucion = new DateTime(2025, 11, 3)
                }
            );


        }


    }
}
