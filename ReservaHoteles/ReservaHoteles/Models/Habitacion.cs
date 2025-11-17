using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaHoteles.Models
{
    public class Habitacion
    {
        public int HabitacionId { get; set; }
        public string TipoHabitacion { get; set; } = null!;
        public int Numero { get; set; }
        public int HotelId { get; set; }

        public virtual Hotel? Hotel { get; set; }
        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();


    }
}
