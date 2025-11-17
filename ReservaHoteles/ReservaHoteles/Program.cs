using Microsoft.EntityFrameworkCore;
using ReservaHoteles.Data;
using ReservaHoteles.Models;
using ReservaHoteles.Repositories;
using System.Security.Cryptography.X509Certificates;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class Program()
{
    static async Task Main()
    {
        var context = new HotelContext();
        await context.Database.MigrateAsync();

        var categoriasRepo = new RepositoryEF<Categoria>(context);
        var clientesRepo = new RepositoryEF<Cliente>(context);
        var habitacionesRepo = new RepositoryEF<Habitacion>(context);
        var hotelesRepo = new RepositoryEF<Hotel>(context);
        var reservasRepo = new RepositoryEF<Reserva>(context);

        Console.WriteLine("Datos insertados Correctamente, por favor revisa la BBDD");

        /*
         1. Consultas con Include (carga de relaciones)
        🔹 Nivel 1 — Básico

        Pregunta 1:
        Obtén todas las reservas e incluye la información del cliente asociado.
        */

        var query = await context.Reservas
            .Include(x => x.Cliente)
            .Select(v => new
            {
                NReserva = v.ReservaId,
                Habitacion = v.Habitacion.Numero,
                NombreCliente = v.Cliente.Nombre,

            }).ToListAsync();

        /*
         Nivel 2 — Intermedio

        Pregunta 2:
        Muestra todas las reservas incluyendo tanto la habitación como el hotel al 
        que pertenece esa habitación.*/

        var query2 = await context.Reservas
            .Include(x => x.Habitacion)
            .ThenInclude(y => y.Hotel)
            .Select(g => new
            {
                ID = g.ReservaId,
                Hotel = g.Habitacion.Hotel.Nombre,
                Habitacion = g.Habitacion
            }).ToListAsync();

        /*
         Nivel 3 — Avanzado

        Pregunta 3:
        Obtén todas las categorías e incluye los hoteles y, dentro de cada hotel, sus habitaciones.*/

        //var query3 = await context.Categorias
        //        .Select(g => new
        //        {
        //            Categoria = g.Descripcion,
        //            Hoteles = g.Hoteles.Select(b => new
        //            {
        //                NombreHotel = b.Nombre,
        //                Habitaciones = b.Habitaciones.Select(x => new
        //                {
        //                    Numero = x.Numero,
        //                    Tipo = x.TipoHabitacion
        //                }).ToList()
        //            }).ToList()
        //        })
        //        .ToListAsync();

        /*
         2. Consultas con SelectMany (proyecciones y aplanamiento de colecciones)
            🔹 Nivel 1 — Básico

            Pregunta 4:
            Obtén una lista plana con todas las habitaciones de todos los hoteles,
            mostrando el nombre del hotel y el número de habitación.*/

        var query4 = await context.Hoteles
            .SelectMany(x => x.Habitaciones)
            .Select(c => new
            {
                Hotel = c.Hotel.Nombre,
                NumHab = c.Numero
            }).ToListAsync();

        /*
         🔹 Nivel 2 — Intermedio

            Pregunta 5:
            Muestra una lista de todas las reservas con el nombre del cliente y el nombre
            del hotel donde reservó (sin usar Include).*/

        var query5 = context.Hoteles
            .SelectMany(c => c.Habitaciones)
            .SelectMany(x => x.Reservas)
            .Select(x => new
            {
                Reserva = x.ReservaId,
                Cliente = x.Cliente.Nombre,
                Hotel = x.Habitacion.Hotel.Nombre,
                HabitacionNum = x.Habitacion.Numero

            }).ToListAsync();

        /*
         🔹 Nivel 3 — Avanzado

            Pregunta 6:
            Genera una lista con todas las agencias de viaje (clientes tipo “Agencia”) y 
            todas las reservas que han realizado,
            incluyendo el nombre del cliente final (NombreClienteAgencia).*/

        var query6 = await context.Clientes
            .Where(x => x.TipoCliente == "Agencia")
            .SelectMany(c => c.Reservas)
            .Select(g => new
            {
                NumReserva = g.ReservaId,
                Agencia = g.Cliente.Nombre,
                Cliente = g.Cliente.NombreClienteAgencia
            }).OrderBy(c=>c.NumReserva).ToListAsync();

        /*
         3. Consultas con GroupBy (agrupación y agregación)
            🔹 Nivel 1 — Básico

            Pregunta 7:
            Agrupa las reservas por cliente y muestra cuántas reservas ha realizado cada uno.*/

        var query7 = await (from c in context.Clientes
                            join r in context.Reservas
                            on c.ClienteId equals r.ClienteId
                            group r by c.Nombre into g
                            select new
                            {
                                NCliente = g.Key,
                                NReservas = g.Count()
                            }).ToListAsync();


        /*
         🔹 Nivel 2 — Intermedio

            Pregunta 8:
            Agrupa las reservas por hotel y calcula el ingreso total obtenido por cada uno (suma de precios).*/

        var query8 = await (from c in context.Hoteles
                            join h in context.Habitaciones
                            on c.HotelId equals h.HotelId
                            join r in context.Reservas
                            on h.HabitacionId equals r.HabitacionId
                            group r by c.Nombre into g
                            select new
                            {
                                Hotel = g.Key,
                                Total = g.Sum(v => v.Precio)
                            }).ToListAsync();


        /*
         🔹 Nivel 3 — Avanzado

            Pregunta 9:
            Agrupa los clientes por tipo ("Particular" o "Agencia") y calcula:

            El número total de reservas realizadas.

            El promedio del precio por reserva.*/

        var query9 = await context.Reservas
                        .GroupBy(x => x.Cliente.TipoCliente)
                        .Select(c => new
                        {
                            Tipo = c.Key,
                            NumRes = c.Count(),
                            Media = c.Average(v => v.Precio)
                        }).ToListAsync();

        //Nombre, direccion y categoria de todos los hoteles

        var query10 = await context.Hoteles
            .Select(x => new
            {
                Nombre = x.Nombre,
                Direcc = x.Direccion,
                Cat = x.Categoria.Descripcion
            }).ToListAsync();

        //Muestra todas las habitaciones que pertenecen a un hotel especifico, incluyendo su numero y tipo

        var query11 = await context.Habitaciones
            .Where(c => c.Hotel.Nombre == "Hotel Sol")
            .Select(g => new
            {
                Hotel = g.Numero,
                Tipo = g.TipoHabitacion

            }).ToListAsync();

        /*
         Muestra todas las reservas que están activas en la fecha actual, incluyendo el nombre
        del cliente y el número de habitación.*/

        var query12 = await context.Reservas
            .Where(c => c.FechaInicio <= DateTime.Now && c.FechaFin >= DateTime.Now)
            .Select(g => new
            {
                Reserva = g.ReservaId,
                Cliente = g.Cliente.NombreClienteAgencia == null ? $"{g.Cliente.Nombre}" : $"{g.Cliente.NombreClienteAgencia}",
                NumeroHab = g.Habitacion.Numero
            }).ToListAsync();


        /*
         * Muestra el nombre de cada hotel junto con el número total de habitaciones que tiene.
         */

        var query13 = await (from h in context.Hoteles
                             join hab in context.Habitaciones
                             on h.HotelId equals hab.HotelId
                             group hab by h.Nombre into g
                             select new
                             {
                                 Hotel = g.Key,
                                 Numero = g.Count()
                             }).ToListAsync();

        /*
         Muestra los clientes que han realizado más de una reserva.
         */

        var query14 = await context.Clientes
            .Where(v => v.Reservas.Count() > 1)
            .Select(c => new
            {
                Nombre = c.Nombre
            }).ToListAsync();


        /*
         Muestra las agencias de viaje que han realizado reservas para más de un cliente distinto.
         */

        var query15 = await context.Clientes
            .Where(c => c.TipoCliente == "Agencia")
            .GroupBy(g => g.Nombre)
            .Select(g => new
            {
                Agencia = g.Key,
                NumeroClientes = g.Select(r => r.NombreClienteAgencia).Distinct().Count()
            }).Where(x=>x.NumeroClientes>1).ToListAsync();


        /*
         Muestra las habitaciones que han sido reservadas más veces, indicando su número y la cantidad
         de reservas que tienen.
         */


        var query16 = await context.Reservas
            .GroupBy(c => c.Habitacion.Numero)
            .Select(g => new
            {
                Habitacion = g.Key,
                NumeroVeces = g.Count()
            }).OrderByDescending(x => x.NumeroVeces).ToListAsync();

        /*
         Muestra cada hotel junto con la lista de clientes que han hecho reservas en él (sin repetir clientes).
         */

        //    var query17 = await context.Reservas
        //.Include(r => r.Cliente)
        //.Include(r => r.Habitacion)
        //    .ThenInclude(h => h.Hotel)
        //.GroupBy(r => r.Habitacion.Hotel.Nombre)
        //.Select(g => new
        //{
        //    Hotel = g.Key,
        //    Clientes = g
        //        .Select(r => r.Cliente.NombreClienteAgencia ?? r.Cliente.Nombre)
        //        .Distinct()
        //        .ToList()
        //})
        //.ToListAsync();



        /*
     Muestra el precio medio de las reservas agrupadas por la categoría del hotel al que pertenecen.
     */

        var query18 = await context.Reservas
            .Include(r => r.Habitacion)
            .ThenInclude(y => y.Hotel)
            .GroupBy(c => c.Habitacion.Hotel.Categoria.Descripcion)
            .Select(g => new
            {
                Categoria = g.Key,
                MediaPrecio = g.Average(x => x.Precio)
            }).ToListAsync();


        /*
         Muestra los clientes que han realizado reservas en más de un hotel diferente e indica cuántos
        hoteles distintos han reservado.
         */

        var query19 = context.Reservas
            .GroupBy(r => r.Cliente.Nombre)
            .Select(g => new
            {
                Cliente = g.Key,
                HotelesDistintos = g
                    .Select(r => r.Habitacion.HotelId)
                    .Distinct()
                    .Count()
            })
            .Where(x => x.HotelesDistintos > 1)
            .ToListAsync();

        //Insertar un nuevo Cliente

        //var nuevoCliente = new Cliente
        //{
        //    Nombre = "Pepin",
        //    Direccion = "calle Vedra",
        //    Telefono = "917778855",
        //    TipoCliente = "Particular",
        //    NombreClienteAgencia = null,
        //};

        //await clientesRepo.AddAsync(nuevoCliente);
        //Console.WriteLine("Cliente nuevo PEPIN insertado correctamente");


        //Actualizar el ultimo Cliente insertado:

        //var modificacion = await clientesRepo.FindAsync(x=>x.Nombre == "Pepin");
        //var final = modificacion.FirstOrDefault();

        //final.Direccion = "Nueva Calle de Prueba";
        //await clientesRepo.UpdateAsync(final);

        //Console.WriteLine("Cliente actualizado con la direccion en mayúsculas");



        //Borrar a Pepin (esta dos veces)

        //var id = 12;
        //var borrarCliente = await clientesRepo.FindAsync(x => x.ClienteId == id);
        //var borradoFinal = borrarCliente.FirstOrDefault();
        //if (borradoFinal == null)
        //{
        //    Console.WriteLine($"No se ha encontrado al cliente con el id {id}");
        //}
        //else
        //{
        //    await clientesRepo.DeleteAsync(id);
        //    Console.WriteLine($"Cliente con el id {id} ha sido borrado");
        //}

        //Insertar un nuevo cliente

        var newCliente = new Cliente
        {
            Nombre = "Halcon Viajes",
            Direccion = "Calle Martinez de la Riva",
            Telefono = "912223344",
            TipoCliente= "Agencia",
            NombreClienteAgencia = "Juan Ramon"
        };

        await clientesRepo.AddAsync(newCliente);
        Console.WriteLine("El cliente ha sido agregado Correctamente");


        //Obtener Todos los Hoteles:

        var query20 = await context.Hoteles
            .Select(c => c.Nombre).ToListAsync();


        //Buscar habitacion de un hotel específico:

        var query21 = await context.Hoteles
            .Include(c=>c.Habitaciones)
            .Select(v => new
            {
                Nombre= v.Nombre,
                NumHAb = v.Habitaciones.Select(g => new
                {
                    Num = g.Numero,
                    Tipo = g.TipoHabitacion,
                }),
            }).ToListAsync();

        //Hoteles con habitaciones incluidas:

        var query22 = await context.Habitaciones
            .Include(h=>h.Hotel)
            .Select(v => new
            {
                Hotel = v.Hotel.Nombre,
                NumHab = v.Numero,
            }).ToListAsync();

        //Muestra todas las reservas, incluyendo la información del cliente y de la habitación reservada.

        var query23 = await context.Reservas
            .Include(c => c.Habitacion)
            .Include(v => v.Cliente)
            .Select(g => new
            {
                ReservaID = g.ReservaId,
                Nombre = (g.Cliente.NombreClienteAgencia == null) ? $"Nombre: {g.Cliente.Nombre}" :
                        $"Cliente: {g.Cliente.NombreClienteAgencia} a traves de la agencia {g.Cliente.Nombre}",
                NumHab = g.Habitacion.Numero

            }).ToListAsync();


        //Obtén todas las categorías de hotel, incluyendo los hoteles dentro de cada categoría y sus habitaciones.
        //Muestra el nombre de la categoría, el hotel y los números de habitación.


        var query24 = await context.Hoteles
            .SelectMany(v=>v.Habitaciones)
            .Select(h => new
            {
                Categoria = h.Hotel.Categoria.Descripcion,
                Hotel = h.Hotel.Nombre,
                Habitaciones = h.Numero
            })
            .ToListAsync();

        //Muestra los hoteles con las reservas activas hoy (entre FechaInicio y FechaFin).
        //Incluye las habitaciones y las reservas en la carga.


        var query25 = await context.Reservas
            .Where(c => c.FechaInicio <= DateTime.Now && c.FechaFin >= DateTime.Now)
            .Include(x => x.Habitacion)
            .ThenInclude(v => v.Hotel)
            .Select(g => new
            {
                Hoteles = g.Habitacion.Hotel.Nombre,
                ReservaId = g.ReservaId,
                ClienteReserva = g.Cliente.Nombre
            }).ToListAsync();


        //Ejercicio 5 (Básico)

        //Enunciado:
        //Obtén una lista con todas las habitaciones de todos los hoteles, mostrando el nombre
        //del hotel y el número de habitación.

        var query26 = await context.Hoteles
            .SelectMany(x => x.Habitaciones)
            .Select(c => new
            {
                Hotel = c.Hotel.Nombre,
                HabitacionNum = c.Numero,
            }).ToListAsync();

        //Muestra el nombre de cada hotel junto con las reservas agrupadas por cliente,

        var query27 = await context.Reservas
            .Include(c => c.Habitacion)
            .ThenInclude(v => v.Hotel)
            .Include(x => x.Cliente)
            .GroupBy(g => g.Cliente.Nombre)
            .Select(g => new
            {
                ClienteNombre = g.Key,
                Hotel = g.Select(v => v.Habitacion.Hotel.Nombre).Distinct().ToList(),
                HabitacionNum = g.Select(v => v.Habitacion.Numero).Distinct().ToList(),
            }).ToListAsync();

        /**
         * // ========================================
// 1️⃣ Consultas con Include (Carga de relaciones)
// ========================================

// Nivel 1 — Básico
// Obtén todas las reservas incluyendo la información del cliente asociado.
var query1 = await context.Reservas
    .Include(x => x.Cliente)
    .Select(v => new
    {
        NReserva = v.ReservaId,
        Habitacion = v.Habitacion.Numero,
        NombreCliente = v.Cliente.Nombre,
    })
    .ToListAsync();

// Nivel 2 — Intermedio
// Muestra todas las reservas incluyendo tanto la habitación como el hotel.
var query2 = await context.Reservas
    .Include(x => x.Habitacion)
    .ThenInclude(y => y.Hotel)
    .Select(g => new
    {
        ID = g.ReservaId,
        Hotel = g.Habitacion.Hotel.Nombre,
        Habitacion = g.Habitacion
    })
    .ToListAsync();

// Nivel 3 — Avanzado
// Obtén todas las categorías e incluye hoteles y habitaciones (estructura jerárquica)
var query3 = await context.Categorias
    .Include(c => c.Hoteles)
        .ThenInclude(h => h.Habitaciones)
    .Select(c => new
    {
        Categoria = c.Descripcion,
        Hoteles = c.Hoteles.Select(h => new
        {
            NombreHotel = h.Nombre,
            Habitaciones = h.Habitaciones.Select(hb => new
            {
                Numero = hb.Numero,
                Tipo = hb.TipoHabitacion
            }).ToList()
        }).ToList()
    })
    .ToListAsync();

// ========================================
// 2️⃣ Consultas con SelectMany (Aplanamiento de colecciones)
// ========================================

// Nivel 1 — Básico
// Lista plana de todas las habitaciones de todos los hoteles
var query4 = await context.Hoteles
    .SelectMany(x => x.Habitaciones)
    .Select(c => new
    {
        Hotel = c.Hotel.Nombre,
        NumHab = c.Numero
    })
    .ToListAsync();

// Nivel 2 — Intermedio
// Lista de todas las reservas con nombre del cliente y hotel (sin usar Include)
var query5 = await context.Hoteles
    .SelectMany(c => c.Habitaciones)
    .SelectMany(x => x.Reservas)
    .Select(x => new
    {
        Reserva = x.ReservaId,
        Cliente = x.Cliente.Nombre,
        Hotel = x.Habitacion.Hotel.Nombre,
        HabitacionNum = x.Habitacion.Numero
    })
    .ToListAsync();

// Nivel 3 — Avanzado
// Agencias y todas las reservas realizadas, incluyendo el nombre final del cliente
var query6 = await context.Clientes
    .Where(x => x.TipoCliente == "Agencia")
    .SelectMany(c => c.Reservas)
    .Select(g => new
    {
        NumReserva = g.ReservaId,
        Agencia = g.Cliente.Nombre,
        Cliente = g.Cliente.NombreClienteAgencia
    })
    .OrderBy(c => c.NumReserva)
    .ToListAsync();

// ========================================
// 3️⃣ Consultas con GroupBy (Agrupación y agregación)
// ========================================

// Nivel 1 — Básico
// Agrupa reservas por cliente y cuenta cuántas reservas ha hecho cada uno
var query7 = await (from c in context.Clientes
                    join r in context.Reservas
                    on c.ClienteId equals r.ClienteId
                    group r by c.Nombre into g
                    select new
                    {
                        NCliente = g.Key,
                        NReservas = g.Count()
                    }).ToListAsync();

// Nivel 2 — Intermedio
// Agrupa reservas por hotel y calcula ingreso total
var query8 = await (from c in context.Hoteles
                    join h in context.Habitaciones
                    on c.HotelId equals h.HotelId
                    join r in context.Reservas
                    on h.HabitacionId equals r.HabitacionId
                    group r by c.Nombre into g
                    select new
                    {
                        Hotel = g.Key,
                        Total = g.Sum(v => v.Precio)
                    }).ToListAsync();

// Nivel 3 — Avanzado
// Agrupa clientes por tipo y calcula número total y promedio de reservas
var query9 = await context.Reservas
    .GroupBy(x => x.Cliente.TipoCliente)
    .Select(c => new
    {
        Tipo = c.Key,
        NumRes = c.Count(),
        Media = c.Average(v => v.Precio)
    })
    .ToListAsync();

// ========================================
// 4️⃣ Consultas con filtros de fechas
// ========================================

// Reservas activas en la fecha actual
var query12 = await context.Reservas
    .Where(c => c.FechaInicio <= DateTime.Now && c.FechaFin >= DateTime.Now)
    .Select(g => new
    {
        Reserva = g.ReservaId,
        Cliente = g.Cliente.NombreClienteAgencia ?? g.Cliente.Nombre,
        NumeroHab = g.Habitacion.Numero
    })
    .ToListAsync();

// Hoteles con reservas activas hoy incluyendo habitaciones y reservas (jerárquico)
var query25 = await context.Reservas
    .Where(c => c.FechaInicio <= DateTime.Now && c.FechaFin >= DateTime.Now)
    .Include(x => x.Habitacion)
    .ThenInclude(v => v.Hotel)
    .Select(g => new
    {
        Hoteles = g.Habitacion.Hotel.Nombre,
        ReservaId = g.ReservaId,
        ClienteReserva = g.Cliente.Nombre
    })
    .ToListAsync();

// ========================================
// 5️⃣ Consultas con Join implícito / SelectMany
// ========================================

// Lista plana de habitaciones de todos los hoteles con nombre de hotel y número de habitación
var query26 = await context.Hoteles
    .SelectMany(x => x.Habitaciones)
    .Select(c => new
    {
        Hotel = c.Hotel.Nombre,
        HabitacionNum = c.Numero
    })
    .ToListAsync();

// ========================================
// 6️⃣ Consultas combinadas GroupBy + Include
// ========================================

// Hoteles y reservas agrupadas por cliente
var query27 = await context.Reservas
    .Include(c => c.Habitacion)
    .ThenInclude(v => v.Hotel)
    .Include(x => x.Cliente)
    .ToListAsync(); // Materializamos antes del GroupBy

var groupedQuery27 = query27
    .GroupBy(r => r.Cliente.Nombre)
    .Select(g => new
    {
        ClienteNombre = g.Key,
        Hotel = g.Select(v => v.Habitacion.Hotel.Nombre).Distinct().ToList(),
        HabitacionNum = g.Select(v => v.Habitacion.Numero).Distinct().ToList()
    })
    .ToList();

// ========================================
// 7️⃣ Otras consultas útiles
// ========================================

// Nombre, dirección y categoría de todos los hoteles
var query10 = await context.Hoteles
    .Select(x => new
    {
        Nombre = x.Nombre,
        Direcc = x.Direccion,
        Cat = x.Categoria.Descripcion
    })
    .ToListAsync();

// Hoteles con sus habitaciones (nombre + tipo)
var query21 = await context.Hoteles
    .Include(c => c.Habitaciones)
    .Select(v => new
    {
        Nombre = v.Nombre,
        NumHAb = v.Habitaciones.Select(g => new
        {
            Num = g.Numero,
            Tipo = g.TipoHabitacion,
        }).ToList(),
    })
    .ToListAsync();

// Precio medio de reservas agrupadas por categoría de hotel
var query18 = await context.Reservas
    .Include(r => r.Habitacion)
    .ThenInclude(y => y.Hotel)
    .GroupBy(c => c.Habitacion.Hotel.Categoria.Descripcion)
    .Select(g => new
    {
        Categoria = g.Key,
        MediaPrecio = g.Average(x => x.Precio)
    })
    .ToListAsync();

// Clientes que han reservado en más de un hotel
var query19 = await context.Reservas
    .GroupBy(r => r.Cliente.Nombre)
    .Select(g => new
    {
        Cliente = g.Key,
        HotelesDistintos = g.Select(r => r.Habitacion.HotelId).Distinct().Count()
    })
    .Where(x => x.HotelesDistintos > 1)
    .ToListAsync();
         */



    }


}