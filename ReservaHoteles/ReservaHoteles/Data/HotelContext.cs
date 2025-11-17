using Microsoft.EntityFrameworkCore;
using ReservaHoteles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaHoteles.Data
{
    public class HotelContext : DbContext
    {
        public DbSet<Reserva> Reservas => Set <Reserva> ();
        public DbSet<Hotel> Hoteles => Set <Hotel> ();
        public DbSet<Cliente> Clientes => Set <Cliente> ();
        public DbSet<Habitacion> Habitaciones => Set <Habitacion> ();
        public DbSet<Categoria> Categorias => Set <Categoria> ();

        public HotelContext() { }
        public HotelContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=HotelesDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reserva>()
                .HasOne(x=> x.Habitacion)
                .WithMany(c=>c.Reservas)
                .HasForeignKey(v=> v.HabitacionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reserva>()
                .HasOne(x => x.Cliente)
                .WithMany(c => c.Reservas)
                .HasForeignKey(v => v.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Habitacion>()
                .HasOne(x => x.Hotel)
                .WithMany(c => c.Habitaciones)
                .HasForeignKey(v => v.HotelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Hotel>()
                .HasOne(x => x.Categoria)
                .WithMany(c => c.Hoteles)
                .HasForeignKey(v => v.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Categoria>().HasData(
        new Categoria { CategoriaId = 1, Descripcion = "Tres Estrellas", TipoIVA = 10 },
        new Categoria { CategoriaId = 2, Descripcion = "Cuatro Estrellas", TipoIVA = 10 },
        new Categoria { CategoriaId = 3, Descripcion = "Cinco Estrellas", TipoIVA = 21 }
    );

            // === HOTELES ===
            modelBuilder.Entity<Hotel>().HasData(
                new Hotel { HotelId = 1, Nombre = "Hotel Sol", Direccion = "Calle Mayor 10", Telefono = "911223344", FechaConstruccion = new DateTime(1995, 5, 10), CategoriaId = 1 },
                new Hotel { HotelId = 2, Nombre = "Gran Palace", Direccion = "Av. Castellana 200", Telefono = "912334455", FechaConstruccion = new DateTime(2005, 3, 15), CategoriaId = 3 },
                new Hotel { HotelId = 3, Nombre = "Costa Azul", Direccion = "Paseo del Mar 22", Telefono = "933556677", FechaConstruccion = new DateTime(2010, 9, 1), CategoriaId = 2 }
            );

            // === HABITACIONES ===
            modelBuilder.Entity<Habitacion>().HasData(
                new Habitacion { HabitacionId = 1, TipoHabitacion = "Individual", Numero = 101, HotelId = 1 },
                new Habitacion { HabitacionId = 2, TipoHabitacion = "Doble", Numero = 102, HotelId = 1 },
                new Habitacion { HabitacionId = 3, TipoHabitacion = "Suite", Numero = 201, HotelId = 2 },
                new Habitacion { HabitacionId = 4, TipoHabitacion = "Doble", Numero = 202, HotelId = 2 },
                new Habitacion { HabitacionId = 5, TipoHabitacion = "Individual", Numero = 301, HotelId = 3 },
                new Habitacion { HabitacionId = 6, TipoHabitacion = "Suite", Numero = 302, HotelId = 3 }
            );

            // === CLIENTES ===
            modelBuilder.Entity<Cliente>().HasData(
                // 5 particulares
                new Cliente { ClienteId = 1, Nombre = "Carlos Gómez", Direccion = "Calle Luna 5", Telefono = "600112233", TipoCliente = "Particular", NombreClienteAgencia = null },
                new Cliente { ClienteId = 2, Nombre = "Laura Pérez", Direccion = "Av. Sol 23", Telefono = "600223344", TipoCliente = "Particular", NombreClienteAgencia = null },
                new Cliente { ClienteId = 3, Nombre = "Antonio Ruiz", Direccion = "Calle Flor 8", Telefono = "600334455", TipoCliente = "Particular", NombreClienteAgencia = null },
                new Cliente { ClienteId = 4, Nombre = "María López", Direccion = "Plaza Norte 4", Telefono = "600445566", TipoCliente = "Particular", NombreClienteAgencia = null },
                new Cliente { ClienteId = 5, Nombre = "Pedro Jiménez", Direccion = "Calle Prado 7", Telefono = "600556677", TipoCliente = "Particular", NombreClienteAgencia = null },

                // 5 agencias
                new Cliente { ClienteId = 6, Nombre = "Viajes SolTour", Direccion = "Calle Comercio 22", Telefono = "910223344", TipoCliente = "Agencia", NombreClienteAgencia = "Marta Díaz" },
                new Cliente { ClienteId = 7, Nombre = "Aventuras Travel", Direccion = "Av. Europa 18", Telefono = "910334455", TipoCliente = "Agencia", NombreClienteAgencia = "Javier Cano" },
                new Cliente { ClienteId = 8, Nombre = "HolidayExpress", Direccion = "Calle Jardín 9", Telefono = "910445566", TipoCliente = "Agencia", NombreClienteAgencia = "Lucía Torres" },
                new Cliente { ClienteId = 9, Nombre = "GlobalTour", Direccion = "Av. Central 12", Telefono = "910556677", TipoCliente = "Agencia", NombreClienteAgencia = "David Martín" },
                new Cliente { ClienteId = 10, Nombre = "TravelPro", Direccion = "Paseo del Arte 3", Telefono = "910667788", TipoCliente = "Agencia", NombreClienteAgencia = "Elena Ramos" }
            );

            // === RESERVAS ===
            modelBuilder.Entity<Reserva>().HasData(
                // === PARTICULARES ===
                new Reserva { ReservaId = 1, FechaInicio = new DateTime(2024, 6, 1), FechaFin = new DateTime(2024, 6, 5), Precio = 400, HabitacionId = 1, ClienteId = 1 },
                new Reserva { ReservaId = 2, FechaInicio = new DateTime(2024, 6, 20), FechaFin = new DateTime(2024, 6, 25), Precio = 420, HabitacionId = 2, ClienteId = 1 }, // Carlos repite
                new Reserva { ReservaId = 3, FechaInicio = new DateTime(2024, 7, 10), FechaFin = new DateTime(2024, 7, 12), Precio = 250, HabitacionId = 2, ClienteId = 2 },
                new Reserva { ReservaId = 4, FechaInicio = new DateTime(2024, 8, 15), FechaFin = new DateTime(2024, 8, 20), Precio = 700, HabitacionId = 3, ClienteId = 3 },
                new Reserva { ReservaId = 5, FechaInicio = new DateTime(2024, 9, 3), FechaFin = new DateTime(2024, 9, 6), Precio = 300, HabitacionId = 4, ClienteId = 4 },
                new Reserva { ReservaId = 6, FechaInicio = new DateTime(2024, 10, 10), FechaFin = new DateTime(2024, 10, 13), Precio = 500, HabitacionId = 5, ClienteId = 5 },
                new Reserva { ReservaId = 7, FechaInicio = new DateTime(2024, 11, 2), FechaFin = new DateTime(2024, 11, 5), Precio = 450, HabitacionId = 1, ClienteId = 2 }, // Laura repite (2 reservas)
                new Reserva { ReservaId = 8, FechaInicio = new DateTime(2025, 1, 10), FechaFin = new DateTime(2025, 1, 15), Precio = 520, HabitacionId = 3, ClienteId = 3 }, // Antonio repite

                // === AGENCIAS ===
                new Reserva { ReservaId = 9, FechaInicio = new DateTime(2024, 5, 2), FechaFin = new DateTime(2024, 5, 6), Precio = 350, HabitacionId = 1, ClienteId = 6 },
                new Reserva { ReservaId = 10, FechaInicio = new DateTime(2024, 5, 10), FechaFin = new DateTime(2024, 5, 15), Precio = 600, HabitacionId = 2, ClienteId = 6 }, // Viajes SolTour repite
                new Reserva { ReservaId = 11, FechaInicio = new DateTime(2024, 11, 1), FechaFin = new DateTime(2024, 11, 4), Precio = 600, HabitacionId = 3, ClienteId = 7 },
                new Reserva { ReservaId = 12, FechaInicio = new DateTime(2024, 12, 10), FechaFin = new DateTime(2024, 12, 15), Precio = 950, HabitacionId = 3, ClienteId = 8 },
                new Reserva { ReservaId = 13, FechaInicio = new DateTime(2024, 12, 20), FechaFin = new DateTime(2024, 12, 25), Precio = 980, HabitacionId = 4, ClienteId = 8 }, // HolidayExpress repite
                new Reserva { ReservaId = 14, FechaInicio = new DateTime(2025, 1, 5), FechaFin = new DateTime(2025, 1, 10), Precio = 800, HabitacionId = 4, ClienteId = 9 },
                new Reserva { ReservaId = 15, FechaInicio = new DateTime(2025, 2, 12), FechaFin = new DateTime(2025, 2, 14), Precio = 280, HabitacionId = 5, ClienteId = 10 },
                new Reserva { ReservaId = 16, FechaInicio = new DateTime(2025, 3, 1), FechaFin = new DateTime(2025, 3, 4), Precio = 310, HabitacionId = 6, ClienteId = 7 }, // Aventuras Travel repite
                new Reserva { ReservaId = 17, FechaInicio = new DateTime(2025, 4, 15), FechaFin = new DateTime(2025, 4, 18), Precio = 400, HabitacionId = 6, ClienteId = 8 }, // HolidayExpress repite otra vez
                new Reserva { ReservaId = 18, FechaInicio = new DateTime(2025, 5, 20), FechaFin = new DateTime(2025, 5, 23), Precio = 390, HabitacionId = 2, ClienteId = 9 }, // GlobalTour repite
                new Reserva { ReservaId = 19, FechaInicio = new DateTime(2025, 6, 10), FechaFin = new DateTime(2025, 6, 14), Precio = 500, HabitacionId = 5, ClienteId = 6 }, // Viajes SolTour tercera
                new Reserva { ReservaId = 20, FechaInicio = new DateTime(2025, 7, 1), FechaFin = new DateTime(2025, 7, 5), Precio = 720, HabitacionId = 4, ClienteId = 10 }  // TravelPro repite
            );


        }
    }
}
