using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColoniasEspaciales.Models
{
    public class Evento
    {
        public int Id { get; set; }
        public string Tipo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public int ColoniaId { get; set; }
        public virtual Colonia? Colonia { get; set; }
    }
}
