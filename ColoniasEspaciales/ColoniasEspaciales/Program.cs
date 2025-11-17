using ColoniasEspaciales.Data;
using ColoniasEspaciales.Models;
using ColoniasEspaciales.Repositories;
using Microsoft.EntityFrameworkCore;

public class Program
{
    static async Task Main()
    {
        var context = new ColoniasContext();
        await context.Database.MigrateAsync();

        var recursosRepo = new RepositoryEF<Recurso>(context);
        var eventosRepo = new RepositoryEF<Evento>(context);
        var coloniaRecursosRepo = new RepositoryEF<ColoniaRecurso>(context);
        var habitantesRepo = new RepositoryEF<Habitante>(context);
        var coloniasRepo = new RepositoryEF<Colonia>(context);
        var planetasRepo = new RepositoryEF<Planeta>(context);

        Console.WriteLine("Datos iniciales cargados con exito");

        //

        Console.ReadKey();

        //Colonias mas Sostenibles

        var topcolonias = await context.Colonias
            .Include(c=> c.Planeta)
            .OrderByDescending(c=>c.NivelSostenibilidad).Take(5).ToListAsync();

        //Recursos mas escasos

        var recursosEscasos = await context.Recursos
            .OrderBy(r=> r.CantidadTotal).Take(5).ToListAsync();

        //PoblacionPorPlaneta

        var poblacionPorPlaneta = await context.Planetas
            .Include(c => c.Colonias)
            .ThenInclude(c => c.Habitantes)
            .Select(p => new
            {
                p.Nombre,
                Poblacion = p.Colonias.Sum(c => c.Habitantes.Count)
            }).ToListAsync();


        //Simulacion evento aleatorio (impacta sensibilidad y recursos)

        var random = new Random();
        var colonias = await context.Colonias.ToListAsync();
        var recursos = await context.Recursos.ToListAsync();

        if (colonias.Any())
        {
            var coloniasAfectadas = colonias[random.Next(colonias.Count)];

            string[] tipos = { "Tormenta Solar", "Epidemia", "Descubrimiento", "Averia", "Escasez" };
            string tipoEvento = tipos[random.Next(tipos.Length)];

            string descripcion = tipoEvento switch
            {
                "Tormenta Solar" => "Radiación dañó algunos sistemas eléctricos",
                "Epidemia" => "Una enfermedad afecto a varios habitantes",
                "Descubrimiento" => "Se ha encontrado un nuevo mineral",
                "Averia" => "El generador principal ha fallado temporalmente",
                "Escasez" => "Han dismimuido las reservas de oxígeno",
                _ => "Evento desconocido"
            };

            //Modificar Sostenibilidad o recursos

            if(tipoEvento == "Escasez" && recursos.Any())
            {
                var recurso = recursos[random.Next(recursos.Count())];
                recurso.CantidadTotal = Math.Max(0, recurso.CantidadTotal - random.Next(50, 150));
                context.Recursos.Update(recurso);
                Console.WriteLine($"La colonia {coloniasAfectadas.Nombre} sufrió escasez de {recurso.Nombre}");
            }

            //Falta pegar el codigo del profesor de Descubrimiento


            var evento = new Evento
            {
                Tipo = tipoEvento,
                Descripcion = descripcion,
                Fecha = DateTime.Now,
                ColoniaId = coloniasAfectadas.Id,
            };
            await eventosRepo.UpdateAsync( evento );
            //await context.SaveChanges(evento);
        }
    }
}