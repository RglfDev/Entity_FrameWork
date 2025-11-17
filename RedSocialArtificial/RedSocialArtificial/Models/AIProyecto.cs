using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocialArtificial.Models
{
    public class AIProyecto
    {

        public int AIEntityId { get; set; }
        public AIEntity AIEntity { get; set; } = null!;
        public int ProyectoColaborativoId { get; set; }
        public ProyectoColaborativo ProyectoColaborativo { get; set; } = null!;


    }
}
