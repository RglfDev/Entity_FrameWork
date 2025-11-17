using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubenAppFinal.Models
{
    public class Clase
    {
        public int ClaseId { get; set; }
        public string Nombre { get; set; } = null!;
        public string Nivel { get; set; } = null!;
        public decimal PrecioMensual { get; set; }
        public int EntrenadorId { get; set; }

        public virtual Entrenador? Entrenador { get; set; }
        public ICollection<Inscripcion>? Inscripciones { get; set; }

       
    }
}
