using LibreriaAsync.Data;
using LibreriaAsync.Models;
using LibreriaAsync.Repositories;
using Microsoft.EntityFrameworkCore;

public class Program
{
    static async Task Main()
    {
        using (var context = new LibreriaDBContext())
        {

            await context.Database.MigrateAsync();

            var LibroRepo = new RepositoryEF<Libro>(context);
            var AutorRepo = new RepositoryEF<Autor>(context);
            var PrestamoRepo = new RepositoryEF<Prestamo>(context);


            Console.WriteLine("Datos Insertados correctamente, por favor revisa tu BBDD");

        }
    }
}