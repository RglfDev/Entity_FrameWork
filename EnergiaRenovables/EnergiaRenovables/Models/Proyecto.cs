using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergiaRenovables.Models
{
    public class Proyecto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public double InversionTotal { get; set; }
        public int TipoEnergiaId { get; set; }
        public int UbicacionId { get; set; }

        public virtual TipoEnergia? TipoEnergia { get; set; }
        public virtual Ubicacion? Ubicacion { get; set; }

        public ICollection<Informe> Informes { get; set; } = new List<Informe>();
        public ICollection<Inversion> Inversiones { get; set; } = new List<Inversion>();

    }
}
