using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaHoteles.Models
{
    public class Reserva
    {
        public int ReservaId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin {  get; set; }
        public double Precio { get; set; }
        public int HabitacionId { get; set; }
        public int ClienteId { get; set; }

        public virtual Habitacion? Habitacion { get; set; }
        public virtual Cliente? Cliente { get; set; }


    }
}
