using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriptomonedasEF.Models
{
    public class Criptomoneda
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Simbolo { get; set; } = null!;
        public double ValorActual { get; set; }

        public ICollection<Operacion> Operaciones {  get; set; } = new List<Operacion>();
        public ICollection<Portafolio> Portafolios { get; set; } = new List<Portafolio>();
    }
}
