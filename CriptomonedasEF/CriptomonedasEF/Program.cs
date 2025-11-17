using CriptomonedasEF.Data;
using CriptomonedasEF.Models;
using CriptomonedasEF.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Numerics;

public class Program
{
    static async Task Main()
    {
        var context = new CriptoContext();
        await context.Database.MigrateAsync();

        var portafoliosRepo = new RepositoryEF<Portafolio>(context);
        var usuariosRepo = new RepositoryEF<Usuario>(context);
        var operacionesRepo = new RepositoryEF<Operacion>(context);
        var criptomonedasRepo = new RepositoryEF<Criptomoneda>(context);

        Console.WriteLine("Datos iniciales cargados con exito");

        //Ranking de usuarios por saldo

        var ranking = await context.Usuarios
            .OrderByDescending(u => u.SaldoVirtual)
            .Select(u => new
            {
                Nombre = u.Nombre,
                saldo = u.SaldoVirtual,
            }).ToListAsync();

        //Criptomonedas mas populares

        var CriptoPop = await context.Portafolios
            .Include(c => c.Criptomoneda)
            .GroupBy(b => b.Criptomoneda.Nombre)
            .Select(g => new
            {
                Criptomoneda = g.Key,
                Usuarios = g.Count()
            }).OrderByDescending(v => v.Usuarios).ToListAsync();

        //Ganacias o perdidas por usuario
        //var query2 = await context.Operaciones
        //    .Include(o => o.Usuario)
        //    .GroupBy(o => o.Usuario.Nombre)
        //    .Select(g => new
        //    {
        //        Usuario = g.Key,
        //        TotalCompras = g.Where(o => o.TipoOperacion == "Compra").Sum(o => o.Cantidad * PrecioUnitario),
        //        totalVentas = g.Where(o => o.TipoOperacion == "Venta").Sum(o => o.Cantidad * PrecioUnitario),
        //        Ganancia = g.Where(o => o.TipoOperacion == "Venta").Sum(o => o.Cantidad * PrecioUnitario) -
        //        g.Where(o => o.TipoOperacion == "Compra").Sum(o => o.Cantidad * PrecioUnitario)
        //    }).ToListAsync();


        //Valor total del portafolio por cada usuario

        var query3 = await context.Portafolios
            .Include(p => p.Usuario)
            .Include(p => p.Criptomoneda)
            .GroupBy(p => p.Usuario.Nombre)
            .Select(g => new
            {
                usuario = g.Key,
                ValorTotal = g.Sum(x => Convert.ToDecimal(x.CantidadActual) * Convert.ToDecimal(x.Criptomoneda.ValorActual))
            }).OrderByDescending(z => z.ValorTotal).ToListAsync();

    }
}