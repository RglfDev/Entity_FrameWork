using Microsoft.EntityFrameworkCore;
using RedSocialArtificial.Data;
using RedSocialArtificial.Models;
using RedSocialArtificial.Repositories;

public class Program
{
    static async Task Main()
    {
        var context = new AIColaboraContext();
        await context.Database.MigrateAsync();

        var aiRepo = new RepositoryEF<AIEntity>(context);
        var datasetRepo = new RepositoryEF<Dataset>(context);
        var proyectosRepo = new RepositoryEF<ProyectoColaborativo>(context);
        var mensajeRepo = new RepositoryEF<Mensaje>(context);
        var EspecializacionesRepo = new RepositoryEF<Especializacion>(context);


        Console.WriteLine("Datos iniciales cargados con exito");


        //AIs mas activas por cantidad de mensajes enviados

        var topAIs = await context.AIs
                .Include(e=>e.MensajesEnviados)
                .Include(e=>e.MensajesRecibidos)
                .OrderByDescending(a => a.MensajesEnviados.Count())
                .Take(5).ToListAsync();

        //Datasets mas utilizados 

        var DataSetMasPopulares= await context.Datasets
            .OrderByDescending (a => a.proyectoDatasets.Count())
            .Take(5).ToListAsync();

        //ProyectosPorEspecializacion

        var proyectosEspecializacion = await context.Proyectos
            .Include(a=> a.Especializacion)
            .GroupBy(b=> b.Especializacion.Nombre)
            .Select(g=> new {Especializacion = g.Key, Cantidad = g.Count()}).ToListAsync();




        Console.ReadKey();
    }
}