using Microsoft.EntityFrameworkCore;
using RedSocialArtificial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocialArtificial.Data
{
    public class AIColaboraContext : DbContext
    {
        public AIColaboraContext() { }


        public AIColaboraContext(DbContextOptions<AIColaboraContext> options) : base(options)
        {
        }

        public DbSet<AIEntity> AIs => Set <AIEntity>();
        public DbSet<AIProyecto> AIProyectos => Set <AIProyecto>();
        public DbSet<Dataset> Datasets => Set <Dataset>();
        public DbSet<ProyectoColaborativo> Proyectos => Set <ProyectoColaborativo>();
        public DbSet<Mensaje> Mensajes => Set <Mensaje>();
        public DbSet<Especializacion> Especializaciones => Set <Especializacion>();
        public DbSet<ProyectoDataset> ProyectoDatasets => Set <ProyectoDataset>();


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=AIColabDatabase;Trusted_Connection=True;");
            }
            
        }

        //Configuracion del modelo

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //tablas intermedias

            modelBuilder.Entity<AIProyecto>().HasKey(ap=> new {ap.AIEntityId, ap.ProyectoColaborativoId });
            modelBuilder.Entity<ProyectoDataset>().HasKey(pd=> new {pd.ProyectoColaborativoId, pd.DatasetId });

            //Relaciones
            modelBuilder.Entity<AIProyecto>()
                .HasOne(ap => ap.AIEntity)
                .WithMany(ap => ap.Proyectos)
                .HasForeignKey(ap => ap.AIEntityId)
                .OnDelete(DeleteBehavior.NoAction); //No permite la entidad principal si existen entidades dependientes.

            modelBuilder.Entity<AIProyecto>()
               .HasOne(ap => ap.ProyectoColaborativo)
               .WithMany(ap => ap.AIProyectos)
               .HasForeignKey(ap => ap.ProyectoColaborativoId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ProyectoDataset>()
               .HasOne(pd => pd.Dataset)
               .WithMany(d => d.proyectoDatasets)
               .HasForeignKey(ap => ap.DatasetId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Mensaje>()
               .HasOne(pd => pd.Emisor)
               .WithMany(d => d.MensajesEnviados)
               .HasForeignKey(ap => ap.EmisorId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Mensaje>()
               .HasOne(pd => pd.Receptor)
               .WithMany(d => d.MensajesRecibidos)
               .HasForeignKey(ap => ap.ReceptorId)
               .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Especializacion>().HasData(
                new Especializacion { Id = 1, Nombre = "Visión Artificial" },
                new Especializacion { Id = 2, Nombre = "Procesamiento de Lenguaje Natural" },
                new Especializacion { Id = 3, Nombre = "Análisis de Datos" }
);
            modelBuilder.Entity<AIEntity>().HasData(
                new AIEntity { Id = 1, Nombre = "VisionAI", Descripcion = "Especialista en reconocimiento de imágenes.", EspecializacionId = 1 },
                new AIEntity { Id = 2, Nombre = "LangMind", Descripcion = "Procesamiento de texto y lenguaje natural.", EspecializacionId = 2 },
                new AIEntity { Id = 3, Nombre = "DataCrunch", Descripcion = "Analista de grandes volúmenes de datos.", EspecializacionId = 3 }
            );
            modelBuilder.Entity<Dataset>().HasData(
                new Dataset { Id = 1, Nombre = "Imagenet Subset", Descripcion = "Conjunto de imágenes etiquetadas.", Fuente = "imagenet.org" },
                new Dataset { Id = 2, Nombre = "Wikipedia Corpus", Descripcion = "Artículos de Wikipedia para NLP.", Fuente = "wikipedia.org" },
                new Dataset { Id = 3, Nombre = "Kaggle Finance", Descripcion = "Datos financieros históricos.", Fuente = "kaggle.com" }
            );
            modelBuilder.Entity<ProyectoColaborativo>().HasData(
                new ProyectoColaborativo { Id = 1, Titulo = "Proyecto VisualNet", Descripcion = "Red neuronal para clasificación de imágenes.", EspecializacionId = 1 },
                new ProyectoColaborativo { Id = 2, Titulo = "Proyecto ChatNLP", Descripcion = "Modelo de diálogo y comprensión contextual.", EspecializacionId = 2 },
                new ProyectoColaborativo { Id = 3, Titulo = "Proyecto DataInsight", Descripcion = "Análisis predictivo de series temporales.", EspecializacionId = 3 }
            );
            modelBuilder.Entity<Mensaje>().HasData(
                new Mensaje { Id = 1, EmisorId = 1, ReceptorId = 2, Contenido = "¿Podrías ayudarme a generar descripciones de imágenes?", FechaEnvio = new DateTime(2025, 1, 10) },
                new Mensaje { Id = 2, EmisorId = 2, ReceptorId = 1, Contenido = "Claro, puedo procesar las etiquetas y crear textos naturales.", FechaEnvio = new DateTime(2025, 1, 11) },
                new Mensaje { Id = 3, EmisorId = 3, ReceptorId = 1, Contenido = "Tengo nuevos datos de rendimiento para tu red visual.", FechaEnvio = new DateTime(2025, 2, 5) }
            );
            modelBuilder.Entity<AIProyecto>().HasData(
                new AIProyecto { AIEntityId = 1, ProyectoColaborativoId = 1 },
                new AIProyecto { AIEntityId = 2, ProyectoColaborativoId = 2 },
                new AIProyecto { AIEntityId = 3, ProyectoColaborativoId = 3 },
                new AIProyecto { AIEntityId = 2, ProyectoColaborativoId = 3 }
            );
            modelBuilder.Entity<ProyectoDataset>().HasData(
                new ProyectoDataset { ProyectoColaborativoId = 1, DatasetId = 1 },
                new ProyectoDataset { ProyectoColaborativoId = 2, DatasetId = 2 },
                new ProyectoDataset { ProyectoColaborativoId = 3, DatasetId = 3 },
                new ProyectoDataset { ProyectoColaborativoId = 3, DatasetId = 2 }
            );
        }
    }
}
