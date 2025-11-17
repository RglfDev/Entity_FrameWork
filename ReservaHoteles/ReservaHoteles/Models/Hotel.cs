using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ReservaHoteles.Models
{
    public class Hotel
    {
        public int HotelId { get; set; }
        public string Nombre { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Telefono {  get; set; } = null!;
        public DateTime FechaConstruccion { get; set; }
        public int CategoriaId { get; set; }

        public virtual Categoria? Categoria { get; set; }
        public ICollection<Habitacion> Habitaciones { get; set; } = new List<Habitacion>();

    }


}
