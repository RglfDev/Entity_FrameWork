using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColoniasEspaciales.Models
{
    public class Colonia
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int PlanetaId { get; set; }
        public string NivelSostenibilidad { get; set; } = null!;
        public virtual Planeta? Planeta {  get; set; }
        public ICollection<ColoniaRecurso>ColoniaRecursos { get; set; } = new List<ColoniaRecurso>();
        public ICollection<Evento>Eventos { get; set; } = new List<Evento>();
        public ICollection<Habitante>Habitantes { get; set; } = new List<Habitante>();

    }
}
