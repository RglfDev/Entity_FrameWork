using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEFC.Models
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string? Direccion {  get; set; }
        public string? Telefono { get; set; }

        public ICollection<Compra> Compras { get; set; } = new List<Compra>();


    }
}
