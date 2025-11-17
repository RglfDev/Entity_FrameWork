using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocialArtificial.Models
{
    public class AIEntity
    {

        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion {  get; set; } = string.Empty;

        public int EspecializacionId { get; set; }
        public Especializacion Especializacion { get; set; } = null!;


        public ICollection<AIProyecto> Proyectos { get; set; } = new List<AIProyecto>();
        public ICollection<Mensaje> MensajesEnviados { get; set; } = new List<Mensaje>();
        public ICollection<Mensaje> MensajesRecibidos { get; set; } = new List<Mensaje>();

    }
}
