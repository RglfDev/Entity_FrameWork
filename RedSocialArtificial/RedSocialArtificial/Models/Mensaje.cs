using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocialArtificial.Models
{
    public class Mensaje
    {
        public int Id { get; set; }
        public int EmisorId { get; set; }
        public int ReceptorId { get; set; }
        public string Contenido { get; set; } = string.Empty;
        public DateTime FechaEnvio { get; set; } = DateTime.UtcNow;


        public AIEntity Emisor { get; set; } = null!;
        public AIEntity Receptor { get; set; } = null!;
    }
}
