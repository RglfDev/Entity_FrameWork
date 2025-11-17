using Microsoft.EntityFrameworkCore;
using PrimerEFAsincronia.Data;
using PrimerEFAsincronia.Models;
using PrimerEFAsincronia.Repositories;

public class Program
{
    static async Task Main()
    {
        using (var context = new ShelterContext())
        {

            await context.Database.MigrateAsync();

            var animalRepo = new RepositoryEF<Animal>(context);
            var adopterRepo = new RepositoryEF<Adopter>(context);
            var adoptionRepo = new RepositoryEF<Adoption>(context);

            Console.WriteLine("Datos iniciales cargados con exito");
            Console.WriteLine("Revisa la base de datos");


            //Mostrar nombre de animales adoptados y los adoptadores

            var adoptions = await context.Adoptions
                .Include(a => a.Animal)
                .Include(a => a.Adopter)
                .Select(a => new
                {
                    Animal = a.Animal.Name,
                    Especie = a.Animal.Species,
                    Adopter = a.Adopter.FullName,
                    F_Adopt = a.AdoptionDate
                }).ToListAsync();


            //Animales sin adoptar

            var adoptedAnimals = adoptions.Select(a => a.Animal).ToList();

            var notAdopted = await animalRepo.FindAsync(a => !adoptedAnimals.Contains(a.Name));


            Console.WriteLine("Animales no adoptados");
            foreach(var a in notAdopted)
            {
                Console.WriteLine($"{a.Name} ({a.Species}) , {a.Age} Años");
            }

        }
    }
}
