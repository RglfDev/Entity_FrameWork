using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubenAppFinal.Models
{
    public class Inscripcion
    {
        public int InscripcionId { get; set; }
        public int SocioId { get; set; }
        public int ClaseId { get; set; }
        public DateTime FechaInicio { get; set; }
        public virtual Socio? Socio { get; set; }
        public virtual Clase? Clase { get; set; }

       
    }
}
