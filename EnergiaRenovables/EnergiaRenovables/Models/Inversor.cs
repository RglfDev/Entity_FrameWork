using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergiaRenovables.Models
{
    public class Inversor
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public double CapitalDisponible { get; set; }

        public ICollection<Inversion> Inversiones { get; set; } = new List<Inversion>();
    }
}
