using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocialIA.Model
{
    public class ProyectoColaborativo
    {
        public int ProyectoColaborativoId { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string? Estado { get; set; }

        public ICollection<AI>? AIs { get; set; }
        public ICollection<Dataset>? Datasets { get; set; }
    }
}
