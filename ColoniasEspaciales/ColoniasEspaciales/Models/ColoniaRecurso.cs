using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColoniasEspaciales.Models
{
    public class ColoniaRecurso
    {
        public int Id { get; set; }
        public int ColoniaId { get; set; }
        public int RecursoId { get; set; }
        public int Cantidad { get; set; }

        public virtual Colonia? Colonia { get; set; }
        public virtual Recurso? Recurso { get; set; }




    }
}
