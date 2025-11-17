using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEFC.Models
{
    public class Suministro
    {
        public int ProductoId { get; set; }
        public int ProveedorId { get; set; }

        public virtual Producto? Producto { get; set; }
        public virtual Proveedor? Proveedor { get; set; }


    }
}
