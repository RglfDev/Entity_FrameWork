using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaHoteles.Models
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string Nombre { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string TipoCliente { get; set; } = null!;
        public string? NombreClienteAgencia { get; set; }

        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

    }
}
