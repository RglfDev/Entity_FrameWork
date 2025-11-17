using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedSocialIA.Model;

namespace RedSocialIA.Model
{
    public class AI
    {
        public int AiId { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? NivelInteligencia { get; set; }
        public string? Estado { get; set; }

        public int EspecializacionID { get; set; }
        public Especializacion? Especializacion { get; set; }

        // 🔹 Eliminamos MensajesEnviados/MensajesRecibidos porque crean ambigüedad
        // 🔹 Si más adelante quieres mensajes, los podrás consultar con LINQ filtrando por EmisorID o ReceptorID

        public ICollection<ProyectoColaborativo>? ProyectosColaborativos { get; set; }

    }
}
