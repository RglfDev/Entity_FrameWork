using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocialArtificial.Models
{
    public class ProyectoDataset
    {

        public int ProyectoColaborativoId { get; set; }
        public ProyectoColaborativo ProyectoColaborativo { get; set; } = null!;
        public int DatasetId { get; set; }
        public Dataset Dataset { get; set; } = null!;

    }
}
