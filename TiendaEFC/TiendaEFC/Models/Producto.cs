using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEFC.Models
{
    public class Producto
    {
        public int ProductoId { get; set; }
        public string Codigo { get; set; } = null!;
        public string? Descripcion { get; set; }
        public double Precio { get; set; }
        public int NumeroExistencias { get; set; }

        public ICollection<Compra>Compras { get; set; } = new List<Compra>();
        public ICollection<Suministro> Suministros { get; set; } = new List<Suministro>();
    }
}
