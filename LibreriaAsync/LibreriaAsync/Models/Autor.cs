using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaAsync.Models
{
    public class Autor
    {
        public int AutorId { get; set; }
        public required string Nombre { get; set; }
        public string? Nacionalidad { get; set; }


        public virtual ICollection<Libro>? Libros { get; set; }
    }
}
