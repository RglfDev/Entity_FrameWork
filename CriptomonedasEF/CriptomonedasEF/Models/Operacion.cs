using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriptomonedasEF.Models
{
    public class Operacion
    {
        public int Id { get; set; }
        public string TipoOperacion { get; set; } = null!;
        public double Cantidad { get; set; }
        public DateTime Fecha { get; set; }

        public int UsuarioId { get; set; }
        public int CriptomonedaId { get; set; }

        public virtual Usuario? Usuario { get; set; }
        public virtual Criptomoneda? Criptomoneda { get; set; }
    }
}
