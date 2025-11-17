using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriptomonedasEF.Models
{
    public class Portafolio
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int CriptomonedaId { get; set; }
        public double CantidadActual { get; set; }

        public virtual Criptomoneda? Criptomoneda { get; set; }
        public virtual Usuario? Usuario { get; set; }

    }
}
