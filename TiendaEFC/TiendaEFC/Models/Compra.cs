using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEFC.Models
{
    public class Compra
    {
        public int CompraId { get; set; }
        public int ProductoId { get; set; }
        public int ClienteId { get; set; }
        public DateTime FechaCompra { get; set; }

        public virtual Cliente? Cliente { get; set; }
        public virtual Producto? Producto { get; set; }
    }
}
