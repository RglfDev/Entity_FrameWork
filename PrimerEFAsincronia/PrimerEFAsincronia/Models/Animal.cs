using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimerEFAsincronia.Models
{
    public class Animal
    {
        public int Id {  get; set; }
        public string Name { get; set; } = "";
        public string? Species { get; set; }
        public int Age { get; set; }


        public virtual ICollection<Adoption> Adoptions { get; set; } = new List<Adoption>();

    }
}
