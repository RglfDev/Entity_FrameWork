using FacturacionApp.Data;
using FacturacionApp.Models;

namespace FacturacionApp
{
    public static class DataSeeder
    {
        public static void Seed(AppDbContext context)
        {
            // Comprobar si ya hay datos
            if (!context.Clients.Any())
            {
                var client1 = new Client { FirstName = "Ana", LastName = "Pérez" };
                var client2 = new Client { FirstName = "Juan", LastName = "López" };

                var project1 = new Project
                {
                    Title = "Proyecto A",
                    StartDate = DateTime.Now.AddMonths(-3),
                    EndDate = DateTime.Now.AddMonths(3),
                    Client = client1
                };

                var project2 = new Project
                {
                    Title = "Proyecto B",
                    StartDate = DateTime.Now.AddMonths(-1),
                    EndDate = DateTime.Now.AddMonths(5),
                    Client = client2
                };

                var invoice1 = new Invoice { AmountDue = 1500, DueDate = DateTime.Now.AddDays(30), Project = project1 };
                var invoice2 = new Invoice { AmountDue = 2500, DueDate = DateTime.Now.AddDays(45), Project = project2 };

                context.AddRange(client1, client2, project1, project2, invoice1, invoice2);
                context.SaveChanges();
            }

        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            crearDatos();
            //Sobre este proyecto ya construido se pueden realizar consultas.


        }

        private static void crearDatos()
        {
            using (var context = new AppDbContext())
            {
                DataSeeder.Seed(context);
                Console.WriteLine("Cargando datos");
                Console.ReadKey();
            }
        }
    }
}
