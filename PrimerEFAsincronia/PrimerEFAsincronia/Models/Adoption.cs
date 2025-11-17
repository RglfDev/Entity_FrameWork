using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimerEFAsincronia.Models
{
    public class Adoption
    {
        public int Id { get; set; }
        //Foreign key Animal
        public int AnimalId { get; set; }
        //Propiedad de navegacion para animal
        public virtual Animal Animal { get; set; } = null!; //No puede ser nulo

        //Foreign Key Adopter
        public int AdopterId { get; set; }

        //Propiedad de navegacion Adopter
        public virtual Adopter Adopter { get; set; } = null!;

        public DateTime AdoptionDate { get; set; }
    }
}
