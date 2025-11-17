using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergiaRenovables.Models
{
    public class TipoEnergia
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;


        public ICollection<Proyecto> Proyectos { get; set; } = new List<Proyecto>();
    }
}
