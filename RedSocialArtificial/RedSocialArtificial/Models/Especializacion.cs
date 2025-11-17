using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocialArtificial.Models
{
    public class Especializacion
    {

        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        public virtual ICollection<AIEntity> AIs { get; set; } = new List<AIEntity>();
        public virtual ICollection<ProyectoColaborativo> Proyectos { get; set; } = new List<ProyectoColaborativo>();

    }
}
