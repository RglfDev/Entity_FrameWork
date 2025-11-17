using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocialArtificial.Models
{
    public class ProyectoColaborativo
    {

        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int EspecializacionId { get; set; }
        public Especializacion Especializacion { get; set; } = null!;

        public ICollection <AIProyecto> AIProyectos { get; set; } = new List<AIProyecto>();
        public ICollection<ProyectoDataset> ProyectoDataset { get; set; } = new List<ProyectoDataset>();

    }
}
