using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEFC.Models;

namespace TiendaEFC.Data
{
    public class TiendaContext :DbContext
    {
        public TiendaContext() { }
        public TiendaContext(DbContextOptions options) : base(options) { }


        public DbSet<Suministro> Suministros => Set<Suministro>();
        public DbSet<Compra> Compras => Set<Compra>();
        public DbSet<Proveedor> Proveedores => Set<Proveedor>();
        public DbSet<Cliente> Clientes => Set<Cliente>();
        public DbSet<Producto> Productos => Set<Producto>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=TiendaEFDatabase;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Suministro>().HasKey(fk=> new {fk.ProveedorId, fk.ProductoId});

            modelBuilder.Entity<Compra>()
                .HasOne(c => c.Producto)
                .WithMany(x => x.Compras)
                .HasForeignKey(f => f.ProductoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Compra>()
                .HasOne(c => c.Cliente)
                .WithMany(x => x.Compras)
                .HasForeignKey(f => f.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Suministro>()
                .HasOne(c => c.Producto)
                .WithMany(x => x.Suministros)
                .HasForeignKey(f => f.ProductoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Suministro>()
                .HasOne(c => c.Proveedor)
                .WithMany(x => x.Suministros)
                .HasForeignKey(f => f.ProveedorId)
                .OnDelete(DeleteBehavior.Restrict)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Proveedor>().HasData(
                new Proveedor { ProveedorId = 1, Nombre = "ElectroMax", Direccion = "C/ Mayor 15", Provincia = "Madrid", Telefono = "911223344" },
                new Proveedor { ProveedorId = 2, Nombre = "TechPlus", Direccion = "Av. América 22", Provincia = "Madrid", Telefono = "912334455" },
                new Proveedor { ProveedorId = 3, Nombre = "Distribuciones Vega", Direccion = "C/ Gran Vía 120", Provincia = "Toledo", Telefono = "925123456" },
                new Proveedor { ProveedorId = 4, Nombre = "Logística Sur", Direccion = "Polígono El Olivar", Provincia = "Sevilla", Telefono = "954998877" }
            );

            // Productos
            modelBuilder.Entity<Producto>().HasData(
                new Producto { ProductoId = 1, Codigo = "P001", Descripcion = "Portátil HP 15\"", Precio = 699.99, NumeroExistencias = 25 },
                new Producto { ProductoId = 2, Codigo = "P002", Descripcion = "Ratón inalámbrico Logitech", Precio = 29.90, NumeroExistencias = 150 },
                new Producto { ProductoId = 3, Codigo = "P003", Descripcion = "Teclado mecánico Corsair", Precio = 89.99, NumeroExistencias = 75 },
                new Producto { ProductoId = 4, Codigo = "P004", Descripcion = "Monitor Samsung 27\"", Precio = 229.50, NumeroExistencias = 40 },
                new Producto { ProductoId = 5, Codigo = "P005", Descripcion = "Impresora HP LaserJet", Precio = 179.00, NumeroExistencias = 30 },
                new Producto { ProductoId = 6, Codigo = "P006", Descripcion = "Auriculares Sony WH-1000XM4", Precio = 299.99, NumeroExistencias = 60 }
            );

            // Clientes
            modelBuilder.Entity<Cliente>().HasData(
                new Cliente { ClienteId = 1, Nombre = "Ana", Apellido = "López", Direccion = "C/ Alcalá 10", Telefono = "600123456" },
                new Cliente { ClienteId = 2, Nombre = "Luis", Apellido = "Pérez", Direccion = "Av. de América 80", Telefono = "600654321" },
                new Cliente { ClienteId = 3, Nombre = "Marta", Apellido = "Gómez", Direccion = "C/ Serrano 42", Telefono = "601998877" },
                new Cliente { ClienteId = 4, Nombre = "Carlos", Apellido = "Ruiz", Direccion = "C/ Toledo 14", Telefono = "602112233" },
                new Cliente { ClienteId = 5, Nombre = "Elena", Apellido = "Fernández", Direccion = "C/ Princesa 5", Telefono = "603445566" }
            );

            // Suministros (muchos a muchos)
            modelBuilder.Entity<Suministro>().HasData(
                new Suministro { ProveedorId = 1, ProductoId = 1 },
                new Suministro { ProveedorId = 1, ProductoId = 2 },
                new Suministro { ProveedorId = 2, ProductoId = 3 },
                new Suministro { ProveedorId = 2, ProductoId = 4 },
                new Suministro { ProveedorId = 3, ProductoId = 5 },
                new Suministro { ProveedorId = 4, ProductoId = 6 },
                new Suministro { ProveedorId = 3, ProductoId = 2 },
                new Suministro { ProveedorId = 4, ProductoId = 4 }
            );

            // Compras
            modelBuilder.Entity<Compra>().HasData(
                new Compra { CompraId = 1, ClienteId = 1, ProductoId = 1, FechaCompra = new DateTime(2024, 5, 10) },
                new Compra { CompraId = 2, ClienteId = 2, ProductoId = 3, FechaCompra = new DateTime(2024, 6, 15) },
                new Compra { CompraId = 3, ClienteId = 3, ProductoId = 2, FechaCompra = new DateTime(2024, 6, 20) },
                new Compra { CompraId = 4, ClienteId = 4, ProductoId = 5, FechaCompra = new DateTime(2024, 7, 1) },
                new Compra { CompraId = 5, ClienteId = 5, ProductoId = 4, FechaCompra = new DateTime(2024, 8, 12) },
                new Compra { CompraId = 6, ClienteId = 1, ProductoId = 6, FechaCompra = new DateTime(2024, 9, 5) },
                new Compra { CompraId = 7, ClienteId = 3, ProductoId = 1, FechaCompra = new DateTime(2024, 9, 30) },
                new Compra { CompraId = 8, ClienteId = 2, ProductoId = 2, FechaCompra = new DateTime(2024, 10, 2) }
            );
        }

    }
}
