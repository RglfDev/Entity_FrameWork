using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubenAppFinal.Models
{
    public class Entrenador
    {
        public int EntrenadorId { get; set; }
        public string Nombre { get; set; } = null!;
        public string Especialidad { get; set; } = null!;

        public ICollection<Clase> Clases { get; set; } = new List<Clase>();

        

    }
}
