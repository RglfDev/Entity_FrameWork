using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaAsync.Models
{
    public class Prestamo
    {
        public int PrestamoId { get; set; }
        public int LibroId { get; set; }
        public required string Usuario { get; set; }

        public DateTime FechaPrestamo { get; set; }
        public DateTime? FechaDevolucion { get; set; }

        public virtual Libro? Libro { get; set; }



    }
}
