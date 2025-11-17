using EnergiaRenovables.Data;
using EnergiaRenovables.Models;
using EnergiaRenovables.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

public class Program
{
    static async Task Main()
    {
        var context = new RenovableContext();
        await context.Database.MigrateAsync();

        var informesRepo = new RepositoryEF<Informe>(context);
        var InversionesRepo = new RepositoryEF<Inversion>(context);
        var InversorerRepo = new RepositoryEF<Inversor>(context);
        var proyectosRepo = new RepositoryEF<Proyecto>(context);
        var tipoenergiasRepo = new RepositoryEF<TipoEnergia>(context);
        var ubicacionesRepo = new RepositoryEF<Ubicacion>(context);


        Console.WriteLine("Datos iniciales cargados con exito");

        //Total de inversion por tipo de energia

        var query1 = await context.Proyectos
            .GroupBy(g => g.TipoEnergia.Nombre)
            .Select(n => new
            {
                TipoEnergia = n.Key,
                TotalInvertido = n.Sum(c => c.InversionTotal)
            }).OrderByDescending(g=> g.TotalInvertido).ToListAsync();


        //Promedio de produccion filtrado por ubicacion y tipo de energia

        var query2 = await context.Proyectos
            .Include(c => c.Informes)
            .GroupBy(c => new
            {
                TipoEnergia = c.TipoEnergia.Nombre,
                Pais = c.Ubicacion.Pais
            })
            .Select(x => new
            {
                Energia = x.Key.TipoEnergia,
                PaisE = x.Key.Pais,
                ProduccionMedia = x.SelectMany(c => c.Informes).Average(v => v.EnergiaGeneradaMWh)
            }).OrderByDescending(x => x.ProduccionMedia).ToListAsync();

    }
}