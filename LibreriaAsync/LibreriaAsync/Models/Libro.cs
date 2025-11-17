using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaAsync.Models
{
    public class Libro
    {
        public int LibroId { get; set; }
        public string? Titulo { get; set; }
        public string? Genero { get; set; }
        public int AutorId { get; set; }


        public virtual Autor? Autor { get; set; }
        public virtual ICollection<Prestamo>? Prestamos { get; set; }
    }
}
