using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaHoteles.Models
{
    public class Categoria
    {
        public int CategoriaId { get; set; }
        public string Descripcion { get; set; } = null!;
        public double TipoIVA { get; set; }

        public ICollection <Hotel> Hoteles { get; set; } = new List<Hotel>();
    }
}
