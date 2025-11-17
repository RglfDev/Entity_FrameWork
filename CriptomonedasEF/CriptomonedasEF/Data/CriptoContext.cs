using CriptomonedasEF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriptomonedasEF.Data
{
    public class CriptoContext : DbContext
    {
        public CriptoContext() { }
        public CriptoContext(DbContextOptions<CriptoContext> options) : base(options) { }

        public DbSet<Portafolio> Portafolios => Set<Portafolio>();
        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Operacion> Operaciones => Set<Operacion>();
        public DbSet<Criptomoneda> Criptomonedas => Set<Criptomoneda>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=CriptoDatabase;Trusted_Connection=True;");
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Portafolio>()
                .HasOne(a=> a.Criptomoneda)
                .WithMany(b=> b.Portafolios)
                .HasForeignKey(x=> x.CriptomonedaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Portafolio>()
                .HasOne(a => a.Usuario)
                .WithMany(b => b.Portafolios)
                .HasForeignKey(x => x.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Operacion>()
                .HasOne(a => a.Criptomoneda)
                .WithMany(b => b.Operaciones)
                .HasForeignKey(x => x.CriptomonedaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Operacion>()
                .HasOne(a => a.Usuario)
                .WithMany(b => b.Operaciones)
                .HasForeignKey(x => x.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Usuario>().HasData(
    new Usuario { Id = 1, Nombre = "Alice", Email = "alice@crypto.com", SaldoVirtual = 2500.0 },
    new Usuario { Id = 2, Nombre = "Bob", Email = "bob@crypto.com", SaldoVirtual = 1200.5 },
    new Usuario { Id = 3, Nombre = "Charlie", Email = "charlie@crypto.com", SaldoVirtual = 3400.75 }
);

            
            modelBuilder.Entity<Criptomoneda>().HasData(
                new Criptomoneda { Id = 1, Nombre = "Bitcoin", Simbolo = "BTC", ValorActual = 65000.0 },
                new Criptomoneda { Id = 2, Nombre = "Ethereum", Simbolo = "ETH", ValorActual = 3400.5 },
                new Criptomoneda { Id = 3, Nombre = "Cardano", Simbolo = "ADA", ValorActual = 0.45 },
                new Criptomoneda { Id = 4, Nombre = "Solana", Simbolo = "SOL", ValorActual = 180.75 }
            );

            
            modelBuilder.Entity<Portafolio>().HasData(
                new Portafolio { Id = 1, UsuarioId = 1, CriptomonedaId = 1, CantidadActual = 0.05 },
                new Portafolio { Id = 2, UsuarioId = 1, CriptomonedaId = 2, CantidadActual = 2.3 },
                new Portafolio { Id = 3, UsuarioId = 2, CriptomonedaId = 3, CantidadActual = 1000 },
                new Portafolio { Id = 4, UsuarioId = 3, CriptomonedaId = 4, CantidadActual = 5.5 },
                new Portafolio { Id = 5, UsuarioId = 3, CriptomonedaId = 1, CantidadActual = 0.12 }
            );

            
            modelBuilder.Entity<Operacion>().HasData(
                new Operacion
                {
                    Id = 1,
                    TipoOperacion = "Compra",
                    Cantidad = 0.03,
                    Fecha = new DateTime(2025, 11, 1),
                    UsuarioId = 1,
                    CriptomonedaId = 1
                },
                new Operacion
                {
                    Id = 2,
                    TipoOperacion = "Venta",
                    Cantidad = 1.0,
                    Fecha = new DateTime(2025, 11, 2),
                    UsuarioId = 2,
                    CriptomonedaId = 3
                },
                new Operacion
                {
                    Id = 3,
                    TipoOperacion = "Compra",
                    Cantidad = 2.0,
                    Fecha = new DateTime(2025, 10, 30),
                    UsuarioId = 3,
                    CriptomonedaId = 2
                },
                new Operacion
                {
                    Id = 4,
                    TipoOperacion = "Compra",
                    Cantidad = 0.07,
                    Fecha = new DateTime(2025, 11, 3),
                    UsuarioId = 3,
                    CriptomonedaId = 1
                }
            );

        }
    }
}
