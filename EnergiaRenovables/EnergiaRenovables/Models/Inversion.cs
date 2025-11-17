using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergiaRenovables.Models
{
    public class Inversion
    {
        public int InversorId { get; set; }
        public int ProyectoId { get; set; }
        public double MontoInvertido { get; set; }

        public virtual Proyecto? Proyecto { get; set; }
        public virtual Inversor? Inversor { get; set; }
    }
}
