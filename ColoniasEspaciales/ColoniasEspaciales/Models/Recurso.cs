using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColoniasEspaciales.Models
{
    public class Recurso
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public int CantidadTotal { get; set; }

        public ICollection <ColoniaRecurso> ColoniaRecursos { get; set; } = new List<ColoniaRecurso>();
    }
}
