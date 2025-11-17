using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergiaRenovables.Models
{
    public class Informe
    {
        public int Id { get; set; }
        public DateTime Fecha {  get; set; }
        public double EnergiaGeneradaMWh {  get; set; }

        public int ProyectoId { get; set; }


        public virtual Proyecto? Proyecto { get; set; }


    }
}
