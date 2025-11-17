using Microsoft.EntityFrameworkCore;
using RedSocialIA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocialIA.Data
{
    public class RedAIDbContext : DbContext
    {

        public DbSet<Especializacion> Especializaciones { get; set; }
        public DbSet<Mensaje> Mensajes { get; set; }
        public DbSet<ProyectoColaborativo> ProyectosColaborativos { get; set; }
        public DbSet<Dataset> Datasets { get; set; }
        public DbSet<AI> AIs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=EFCodeRedSocialIA;Trusted_Connection=True;");
        }

    }
}
