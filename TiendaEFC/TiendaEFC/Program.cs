using Microsoft.EntityFrameworkCore;
using TiendaEFC.Data;
using TiendaEFC.Models;
using TiendaEFC.Repositories;

public class Program()
{
    static async Task Main()
    {

        var context = new TiendaContext();
        await context.Database.MigrateAsync();


        var suministrosRepo = new RepositoryEF<Suministro>(context);
        var comprasRepo = new RepositoryEF<Compra>(context);
        var ProveedoresRepo = new RepositoryEF<Proveedor>(context);
        var clientesRepo = new RepositoryEF<Cliente>(context);
        var productosRepo = new RepositoryEF<Producto>(context);


        Console.WriteLine("Todo Correcto, por favor revisa la base de datos");

        //Consultas:

        /*
            * 1 — Listar productos

                Muestra el código, descripción y precio de todos los productos disponibles en la base de datos.

        2 — Compras de un cliente

                Obtén todas las compras realizadas por el cliente "Ana López", mostrando la fecha de compra y la descripción del producto.

        3 — Productos con sus proveedores

                Muestra una lista con los productos y los nombres de los proveedores que los suministran.
                Cada combinación producto–proveedor debe aparecer en una línea.

        4 — Clientes frecuentes

                Obtén los clientes que han realizado más de una compra distinta, mostrando su nombre completo y el número total de compras.

        5 — Proveedor con más productos

                Muestra el proveedor que suministra más productos distintos, junto con la cantidad total de productos que ofrece.
        */


        var query1 = await context.Productos
            .Where(c => c.NumeroExistencias > 0)
            .Select(v => new
            {
                Codigo = v.Codigo,
                Descripcion = v.Descripcion,
                Precio = v.Precio,
                Existencias = v.NumeroExistencias

            }).ToListAsync();


        var query2 = (await context.Compras
                .Where(c => c.Cliente.Nombre == "Ana")
                .Select(b => new
                {
                    Nombre = b.Cliente.Nombre,
                    Fecha = b.FechaCompra,
                    Descripcion = b.Producto.Descripcion
                })
                .ToListAsync()) 
                .Select(c => new
                {
                    Name = c.Nombre,
                    Fecha = c.Fecha.ToShortDateString(),
                    Description = c.Descripcion
                })
                .ToList();

        var query3 = await context.Productos
                .SelectMany(p => p.Suministros.Select(s => new
                {
                    Codigo = p.Codigo,
                    Proveedor = s.Proveedor.Nombre
                }))
                .ToListAsync();

        var query4 = await context.Clientes
                .Select(c => new
                {
                    Nombre = c.Nombre + " " + c.Apellido,
                    ComprasDistintas = c.Compras.Select(x => x.ProductoId).Distinct().Count()
                })
                .Where(c => c.ComprasDistintas > 1)
                .ToListAsync();

        var query5 = await context.Suministros
                .GroupBy(s => new { s.ProveedorId, s.Proveedor.Nombre })
                .Select(g => new
                {
                    Proveedor = g.Key.Nombre,
                    TotalProductos = g.Count()
                })
                .OrderByDescending(g => g.TotalProductos)
                .FirstOrDefaultAsync();




    }
}
