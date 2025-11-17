using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColoniasEspaciales.Models
{
    public class Habitante
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Rol { get; set; } = null!;
        public int ColoniaId { get; set; }

        public virtual Colonia? Colonia { get; set;}
    }
}
