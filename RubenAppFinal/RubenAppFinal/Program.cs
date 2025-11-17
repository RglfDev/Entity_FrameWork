using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using RubenAppFinal.Data;
using RubenAppFinal.Models;
using RubenAppFinal.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class Program
{
    static async Task Main()
    {
        var context = new GimnasioContext();
        await context.Database.MigrateAsync();

        var inscripcionesRepo = new RepositoryEF<Inscripcion>(context);
        var entrenadoresRepo = new RepositoryEF<Entrenador>(context);
        var clasesRepo = new RepositoryEF<Clase>(context);
        var sociosRepo = new RepositoryEF<Socio>(context);

        var socios = await context.Socios.ToListAsync();
        Console.WriteLine($"Número real de socios en DB: {socios.Count}");
        foreach (var s in socios)
            Console.WriteLine($"{s.SocioId} - {s.Nombre}");

        int opcionMenu;

        do
        {
            Console.WriteLine();
            Console.WriteLine("====================");
            Console.WriteLine("MENÚ DE OPCIONES");
            Console.WriteLine("====================");
            Console.WriteLine("1. Listar todas las inscripciones");
            Console.WriteLine("2. Mostrar los socios con mayor gasto mensual");
            Console.WriteLine("3. Listar las clases más populares");
            Console.WriteLine("4. Registrar nueva inscripción");
            Console.WriteLine("0. Salir");
            Console.Write("Selecciona una opción: ");

            if (!int.TryParse(Console.ReadLine(), out opcionMenu) || opcionMenu < 0 || opcionMenu > 4)
            {
                Console.WriteLine("Error, por favor introduce un numero del 1 al 4");
                continue;
            }

            switch (opcionMenu)
            {
                case 1:
                    await Ejercicio1(context);
                    break;
                case 2:
                    await Ejercicio2(context);
                    break;
                case 3:
                    await Ejercicio3(context);
                    break;
                case 4:
                    await Ejercicio4(context, sociosRepo, clasesRepo,inscripcionesRepo);
                    break;

                case 0:
                    Console.WriteLine("Saliendo del programa...");
                    break;
            }
        } while (opcionMenu != 0);
        Console.ReadKey();
      
    }

    

    private static async Task Ejercicio1(GimnasioContext context)
    {
        //1.Listar todas las inscripciones, mostrando el nombre del socio, la clase y el entrenador.
        Console.WriteLine();
        Console.WriteLine("====================");
        Console.WriteLine("Ejercicio 1:");
        Console.WriteLine("====================");
        Console.WriteLine();
        
        var query1 = await context.Inscripciones
           .Include(c => c.Clase)
           .ThenInclude(v => v.Entrenador)
           .Include(x => x.Socio)
           .Select(g => new
           {
               IdInscripcion = g.InscripcionId,
               SocioName = g.Socio.Nombre,
               ClaseName = g.Clase.Nombre,
               EntrenadorName = g.Clase.Entrenador.Nombre
           }).ToListAsync();


        foreach (var item in query1)
        {
            Console.WriteLine($"Inscripcion nº{item.IdInscripcion}: Socio: {item.SocioName}, " +
                $"Clase: {item.ClaseName}, Entrenador: {item.EntrenadorName}");
        }
    }
    private static async Task Ejercicio2(GimnasioContext context)
    {
        //2.Mostrar los socios con mayor gasto mensual total, calculando la suma de las clases a las que están inscritos.
        //Voy a coger los 3 que mas han gastado

        Console.WriteLine();
        Console.WriteLine("====================");
        Console.WriteLine("Ejercicio 2:");
        Console.WriteLine("====================");
        Console.WriteLine();

        var query2 = await context.Socios
            .Include(c => c.Inscripciones)
            .ThenInclude(n => n.Clase)
            .Select(g => new
            {
                NombreSocio = g.Nombre,
                NumeroCLases = g.Inscripciones.Count(),
                TotalGastadoEnClases = g.Inscripciones.Sum(x => x.Clase.PrecioMensual)
            }).OrderByDescending(b => b.TotalGastadoEnClases).Take(3).ToListAsync();

        foreach (var item in query2)
        {
            Console.WriteLine($"{item.NombreSocio}: Numero de clases: {item.NumeroCLases} -> Total: {item.TotalGastadoEnClases}");
        }

    }
    private static async Task Ejercicio3(GimnasioContext context)
    {
        //3.Listar las clases más populares, ordenadas por cantidad de socios inscritos.

        Console.WriteLine();
        Console.WriteLine("====================");
        Console.WriteLine("Ejercicio 3:");
        Console.WriteLine("====================");
        Console.WriteLine();

        var query3 = await context.Clases
            .Include(c => c.Inscripciones)
            .Include(v => v.Entrenador)
            .Select(g => new
            {
                ClaseId = g.ClaseId,
                NombreClase = g.Nombre,
                EntrenadorClase = g.Entrenador.Nombre,
                Inscritos = g.Inscripciones.Count()
            }).OrderByDescending(v => v.Inscritos).ToListAsync();

        foreach (var item in query3)
        {
            Console.WriteLine($"{item.ClaseId} -> {item.NombreClase}// Entrenador: {item.EntrenadorClase} -- Alumnos Inscritos: {item.Inscritos} ");
        }
    }
    private static async Task Ejercicio4(GimnasioContext context,RepositoryEF<Socio> sociosRepo,
        RepositoryEF<Clase> clasesRepo, RepositoryEF<Inscripcion>inscripcionesRepo)
    {
        Console.WriteLine();
        Console.WriteLine("====================");
        Console.WriteLine("Ejercicio 1:");
        Console.WriteLine("====================");
        Console.WriteLine();
        Console.WriteLine("Selecciona un número de socio existente por favor (del 1 al 7):");
        Console.Write("Respuesta: ");

        

        var totalSocios = (await sociosRepo.GetAllAsync()).ToList();
        var todasClases = (await clasesRepo.GetAllAsync()).ToList();

        int opcion;

        do
        {

            if (!int.TryParse(Console.ReadLine(), out opcion) || opcion < 0 || opcion > totalSocios.Count())
            {
                Console.WriteLine($"Error, por favor, introduce un valor entre 1 y {totalSocios.Count()} ");

                opcion = -1;
            }
        } while (opcion == -1);

        var seleccionadoSocio = totalSocios[opcion - 1];

        Console.WriteLine($"Has seleccionado a {seleccionadoSocio.Nombre}");
        Console.WriteLine();
        Console.WriteLine("Selecciona ahora una clase disponible: ");
        foreach(var item in todasClases) 
        {
            Console.WriteLine($"Clase {item.ClaseId}: {item.Nombre}");

        }
        Console.WriteLine();
        Console.Write("Respuesta: ");

        int opcion2;
        Clase seleccionadoClases = null!;

        do
        {

            if (!int.TryParse(Console.ReadLine(), out opcion2) || opcion2 <= 0 || opcion2 > todasClases.Count())
            {
                Console.WriteLine($"Error, por favor, introduce un valor entre 1 y {todasClases.Count()} ");

                opcion2 = -1;
            }
        

            seleccionadoClases = todasClases[opcion2 - 1];

            var yaInscrito = await context.Inscripciones
                .Where(c => c.SocioId == seleccionadoSocio.SocioId && c.ClaseId == seleccionadoClases.ClaseId)
                .FirstOrDefaultAsync();

            if (yaInscrito != null)
            {
                Console.WriteLine($"{seleccionadoSocio.Nombre} ya esta inscrito a {seleccionadoClases.Nombre}.Apuntalo a otra clase por favor.");
                opcion2 = -1;
        
            }
        } while (opcion2 == -1);

        Console.WriteLine($"Has seleccionado a {seleccionadoClases.Nombre}");
        Console.WriteLine();

        var fechaActual = DateTime.Now;

        var newInscripcion = new Inscripcion
        {
            SocioId = seleccionadoSocio.SocioId,
            ClaseId = seleccionadoClases.ClaseId,
            FechaInicio = fechaActual
        };

        await inscripcionesRepo.AddAsync(newInscripcion);
        

        Console.WriteLine($"Se ha registrado a {seleccionadoSocio.Nombre} en la clase" +
            $" {seleccionadoClases.Nombre} con a fecha de {fechaActual}");

    }


}

