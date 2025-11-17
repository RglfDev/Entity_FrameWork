using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pruebaEF
{
    internal class Program
    {
        public static class DataSeeder
        {
            public static void Seed(Model1Container context)
            {
                // === Productos ===
                if (!context.Products.Any())
                {
                    context.Products.Add(new Products { ProductName = "Laptop Dell XPS 13", UnitPrice = 1200 });
                    context.Products.Add(new Products { ProductName = "iPhone 15", UnitPrice = 999 });
                    context.Products.Add(new Products { ProductName = "Smartwatch Samsung", UnitPrice = 299 });
                    context.Products.Add(new Products { ProductName = "Auriculares Sony WH-1000XM5", UnitPrice = 399 });
                    context.SaveChanges();
                }

                // === Clientes ===
                if (!context.Clients.Any())
                {
                    context.Clients.Add(new Clients { CompanyName = "Tech Solutions", Address = "123 Main Street, New York", Phone = "+1-555-1001" });
                    context.Clients.Add(new Clients { CompanyName = "Innovatech Corp", Address = "742 Evergreen Terrace, Springfield", Phone = "+1-555-2002" });
                    context.Clients.Add(new Clients { CompanyName = "Global Traders", Address = "99 Market Road, San Francisco", Phone = "+1-555-3003" });
                    context.SaveChanges();
                }

                // === Pedidos ===
                if (!context.Orders.Any())
                {
                    // Recuperar las entidades para usar sus IDs y referencias
                    var products = context.Products.ToList();
                    var clients = context.Clients.ToList();

                    var laptop = products.First(p => p.ProductName.Contains("Laptop"));
                    var iphone = products.First(p => p.ProductName.Contains("iPhone"));
                    var smartwatch = products.First(p => p.ProductName.Contains("Smartwatch"));
                    var auriculares = products.First(p => p.ProductName.Contains("Auriculares"));

                    var techSolutions = clients.First(c => c.CompanyName == "Tech Solutions");
                    var innovatech = clients.First(c => c.CompanyName == "Innovatech Corp");
                    var globalTraders = clients.First(c => c.CompanyName == "Global Traders");

                    // Crear órdenes con FK + navegación
                    var orders = new List<Orders>
                {
                 new Orders { Quantity = 3, Client = techSolutions.Id_Client, Product = laptop.Id_Product, Clients = techSolutions, Products = laptop },
                 new Orders { Quantity = 5, Client = techSolutions.Id_Client, Product = auriculares.Id_Product, Clients = techSolutions, Products = auriculares },
                 new Orders { Quantity = 2, Client = innovatech.Id_Client, Product = iphone.Id_Product, Clients = innovatech, Products = iphone },
                 new Orders { Quantity = 10, Client = globalTraders.Id_Client, Product = smartwatch.Id_Product, Clients = globalTraders, Products = smartwatch }
                };

                    context.Orders.AddRange(orders);
                    context.SaveChanges();
                }
            }
        }

        static void CargarDatos()
        {
            using (var context = new Model1Container())
            {
                DataSeeder.Seed(context);

                Console.WriteLine("Datos Cargados correctamente...");
                Console.WriteLine("Comprueba la base de datos");

            }
        }


        static void Main(string[] args)
        {
            CargarDatos();
            ObtenerTodoClientes();
            PedidosConCantidadYIdCliente();
            ClientesConSuNombreDeProducto();
            ClientesConCantidadTotalDePedidos();
            ImportetotalDeCadaPedido();
            ClientesJuntoTotalGastado();
            ProductosPedidosAlMenosUnaVez();
            ProductoMasCaroPorCliente();
            ClientesConImporteTotalMayorAMil();

        }

        private static void ClientesConImporteTotalMayorAMil()
        {
            using (var context = new Model1Container())
            {

                var query = (from c in context.Clients
                             join o in context.Orders
                             on c.Id_Client equals o.Client
                             join p in context.Products
                             on o.Product equals p.Id_Product
                             group new
                             {
                                 o,
                                 p
                             } by c.CompanyName into grupo
                             where grupo.Sum(x => x.o.Quantity * x.p.UnitPrice) > 1000
                             select new
                             {
                                 Nombre = grupo.Key,
                                 Total = grupo.Sum(x => x.o.Quantity * x.p.UnitPrice)
                             }).ToList();


                foreach (var item in query)
                {
                    Console.WriteLine($"Cliente: {item.Nombre} - Total: {item.Total}$");
                }
            }
        }

        private static void ProductoMasCaroPorCliente()
        {
            using (var context = new Model1Container())
            {

                var query = (from c in context.Clients
                             join o in context.Orders
                             on c.Id_Client equals o.Client
                             join p in context.Products
                             on o.Product equals p.Id_Product
                             group p by c.CompanyName into g
                             select new
                             {
                                 Cliente = g.Key,
                                 MasCaro = g.OrderByDescending(x => x.UnitPrice).FirstOrDefault().ProductName
                             }).ToList();

                foreach (var item in query)
                {
                    Console.WriteLine($"Cliente: {item.Cliente} - Producto: {item.MasCaro}");
                }
            }
        }

        private static void ProductosPedidosAlMenosUnaVez()
        {
            using (var context = new Model1Container())
            {

                var query = (from p in context.Products
                            join o in context.Orders
                            on p.Id_Product equals o.Product
                            select p.ProductName).Distinct().ToList();


                foreach (var i in query)
                {
                    Console.WriteLine($"Producto {i}");
                }
            }
        }

        private static void ClientesJuntoTotalGastado()
        {
            using (var context = new Model1Container())
            {

                var query = (from c in context.Clients
                            join o in context.Orders
                            on c.Id_Client equals o.Client
                            join p in context.Products
                            on o.Product equals p.Id_Product
                            group new { o, p} by c.CompanyName into g
                            select new
                            {
                                Nombre = g.Key,
                                Total = g.Sum(x=> x.o.Quantity * x.p.UnitPrice)
                            }).ToList();


                foreach(var item in query)
                {
                    Console.WriteLine($"Cliente: {item.Nombre} - Total: {item.Total}$");
                }

                             

            }
        }

        private static void ImportetotalDeCadaPedido()
        {
            using (var context = new Model1Container())
            {

                var query = (from p in context.Products
                             join o in context.Orders
                             on p.Id_Product equals o.Product
                             select new
                             {
                                 Pedido = p.Id_Product,
                                 Total = p.UnitPrice * o.Quantity,
                             }).ToList();


                foreach (var i in query)
                {
                    Console.WriteLine($"Pedido {i.Pedido}, Total: {i.Total}");
                }

            }
        }

        private static void ClientesConCantidadTotalDePedidos()
        {
            using (var context = new Model1Container())
            {

                var query = (from c in context.Clients
                             join o in context.Orders
                             on c.Id_Client equals o.Client
                             group o by o.Client into grupo
                             select new
                             {
                                 Cliente = grupo.Key,
                                 CantidadTotal = grupo.Sum(x => x.Quantity)
                             }).ToList();


                foreach (var i in query)
                {
                    Console.WriteLine($"Cliente {i.Cliente}, Producto: {i.CantidadTotal}");
                }

            }
        }

        private static void ClientesConSuNombreDeProducto()
        {
            using (var context = new Model1Container())
            {

                var query = (from c in context.Clients
                             join o in context.Orders
                             on c.Id_Client equals o.Client
                             join p in context.Products
                             on o.Product equals p.Id_Product
                             select new
                             {
                                 Cliente = c.CompanyName,
                                 Producto = p.ProductName,
                             }).ToList();
                           

                foreach (var i in query)
                {
                    Console.WriteLine($"Cliente {i.Cliente}, Producto: {i.Producto}");
                }

            }
        }

        private static void PedidosConCantidadYIdCliente()
        {
            using (var context = new Model1Container())
            {

                var query = (from p in context.Orders
                             select new
                             {
                                 Orden = p.Id_Order,
                                 Cantidad = p.Quantity,
                                 idClient = p.Clients.Id_Client,
                             }).ToList();

                foreach(var item in query)
                {
                    Console.WriteLine($"Nº {item.Orden} : Cantidad: {item.Cantidad} - IDCliente: {item.idClient}");
                }

            }
        }

        private static void ObtenerTodoClientes()
        {
            using (var context = new Model1Container())
            {
                var query = (from c in context.Clients
                             select c.CompanyName).ToList();

                foreach (var c in query)
                {
                    Console.WriteLine($"Nombre: {c}");
                }

            }
        }
    }
}

