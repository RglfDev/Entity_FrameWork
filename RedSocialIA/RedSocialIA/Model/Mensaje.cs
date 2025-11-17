using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocialIA.Model
{
    public class Mensaje
    {
        public int MensajeId { get; set; }
        public string? Contenido { get; set; }
        public DateTime FechaEnvio { get; set; }

        public int AiId { get; set; }   
        public AI? AI { get; set; }


    }
}
