using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEFC.Models
{
    public class Proveedor
    {
        public int ProveedorId { get; set; }
        public string Nombre { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Provincia { get; set; } = null!;
        public string Telefono { get; set; } = null!;

        public ICollection<Suministro> Suministros { get; set; } = new List<Suministro>();

    }
}
