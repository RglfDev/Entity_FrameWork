using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocialIA.Model
{
    public class Especializacion
    {

        public int EspecializacionID { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }

        public ICollection<AI>? AIs { get; set; }
    }
}
