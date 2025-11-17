using Microsoft.Identity.Client;
using RedSocialIA.Data;
using RedSocialIA.Model;
using System.Security.Cryptography;

namespace RedSocialIA
{
    public static class DataSeed 
    {
        public static void Seed(RedAIDbContext context)
        {
            if (context.AIs.Any())
                return; // Ya tiene datos

            // ------------------- ESPECIALIZACIONES -------------------
            var especializaciones = new List<Especializacion>
            {
                new Especializacion { Nombre = "Visión Artificial", Descripcion = "Procesamiento y análisis de imágenes y video." },
                new Especializacion { Nombre = "Procesamiento del Lenguaje Natural", Descripcion = "Análisis y generación de lenguaje humano." },
                new Especializacion { Nombre = "Aprendizaje Automático", Descripcion = "Modelos predictivos y aprendizaje supervisado." },
                new Especializacion { Nombre = "Robótica Inteligente", Descripcion = "Control autónomo de robots y sistemas físicos." },
                new Especializacion { Nombre = "Ciberseguridad con IA", Descripcion = "Detección y respuesta automatizada a amenazas." },
                new Especializacion { Nombre = "Optimización de Recursos", Descripcion = "Algoritmos de optimización y análisis de eficiencia." },
                new Especializacion { Nombre = "Análisis de Sentimiento", Descripcion = "Interpretación emocional de texto y voz." }
            };
            context.Especializaciones.AddRange(especializaciones);
            context.SaveChanges();


            // ------------------- AIs -------------------
            var rnd = new Random();

            var ais = new List<AI>();

            string[] nombresAI = {
                "Aida", "VisionBot", "PredictorX", "Linguo", "OptimusAI",
                "GuardianNet", "Sentio", "NeuraCore", "RoboThink", "DeepVoice",
                "CortexOne", "PathFinder", "EvoBot", "Symbio", "AlphaGuard",
                "Synthetix", "DataPulse", "VisionPrime", "EchoMind", "QuantumIA"
            };

            foreach (var nombre in nombresAI)
            {
                var ai = new AI
                {
                    Nombre = nombre,
                    Descripcion = $"Instancia de IA {nombre} creada para tareas avanzadas.",
                    NivelInteligencia = new[] { "Baja", "Media", "Alta", "Superinteligencia" }[rnd.Next(0, 4)],
                    Estado = new[] { "Activa", "Entrenando", "En pausa", "Depreciada" }[rnd.Next(0, 4)],
                    EspecializacionID = especializaciones[rnd.Next(especializaciones.Count)].EspecializacionID
                };
                ais.Add(ai);
            }
            context.AIs.AddRange(ais);
            context.SaveChanges();


            // ------------------- DATASETS -------------------
            var datasets = new List<Dataset>
            {
                new Dataset { Nombre = "ImagenNet Subset", Descripcion = "Colección etiquetada de imágenes naturales.", Tipo = "Imágenes", FechaPublicacion = DateTime.Now.AddMonths(-12) },
                new Dataset { Nombre = "Corpus Conversacional", Descripcion = "Dataset de diálogos reales y simulados.", Tipo = "Texto", FechaPublicacion = DateTime.Now.AddMonths(-5) },
                new Dataset { Nombre = "Human Motion Data", Descripcion = "Datos de movimientos humanos capturados por sensores.", Tipo = "Sensores", FechaPublicacion = DateTime.Now.AddMonths(-7) },
                new Dataset { Nombre = "Financial Trends 2024", Descripcion = "Datos históricos de mercado financiero.", Tipo = "Numérico", FechaPublicacion = DateTime.Now.AddMonths(-3) },
                new Dataset { Nombre = "AudioSpectrum DB", Descripcion = "Datos de audio para reconocimiento de voz.", Tipo = "Audio", FechaPublicacion = DateTime.Now.AddMonths(-8) },
                new Dataset { Nombre = "CyberAttack Logs", Descripcion = "Registros de ciberataques simulados.", Tipo = "Logs", FechaPublicacion = DateTime.Now.AddMonths(-2) },
                new Dataset { Nombre = "Traffic DataSet", Descripcion = "Datos de tráfico urbano en tiempo real.", Tipo = "Streaming", FechaPublicacion = DateTime.Now.AddMonths(-10) },
                new Dataset { Nombre = "EmotionSpeech", Descripcion = "Dataset de voces con diferentes emociones.", Tipo = "Audio", FechaPublicacion = DateTime.Now.AddMonths(-6) }
            };
            context.Datasets.AddRange(datasets);
            context.SaveChanges();


            // ------------------- PROYECTOS -------------------
            var proyectos = new List<ProyectoColaborativo>
            {
                new ProyectoColaborativo { Nombre = "Proyecto Alfa", Descripcion = "IA médica para diagnóstico automatizado.", FechaInicio = new DateTime(2024, 1, 1), FechaFin = new DateTime(2025, 12, 31), Estado = "En curso" },
                new ProyectoColaborativo { Nombre = "Proyecto Beta", Descripcion = "Análisis de emociones a partir de voz y texto.", FechaInicio = new DateTime(2025, 2, 15), FechaFin = new DateTime(2026, 2, 15), Estado = "Planificación" },
                new ProyectoColaborativo { Nombre = "Proyecto Gamma", Descripcion = "Predicción de tendencias financieras.", FechaInicio = new DateTime(2025, 3, 10), FechaFin = new DateTime(2026, 1, 30), Estado = "En curso" },
                new ProyectoColaborativo { Nombre = "Proyecto Delta", Descripcion = "Control autónomo de robots de limpieza.", FechaInicio = new DateTime(2024, 11, 1), FechaFin = new DateTime(2026, 5, 1), Estado = "Finalizado" },
                new ProyectoColaborativo { Nombre = "Proyecto Sigma", Descripcion = "Análisis de tráfico urbano con IA.", FechaInicio = new DateTime(2025, 4, 5), FechaFin = new DateTime(2026, 4, 5), Estado = "En curso" },
                new ProyectoColaborativo { Nombre = "Proyecto Omega", Descripcion = "Sistema de ciberseguridad predictivo.", FechaInicio = new DateTime(2025, 5, 15), FechaFin = new DateTime(2026, 7, 15), Estado = "En desarrollo" }
            };
            context.ProyectosColaborativos.AddRange(proyectos);
            context.SaveChanges();


            // ------------------- MENSAJES -------------------
            var mensajes = new List<Mensaje>();
            for (int i = 0; i < 30; i++)
            {
                var ai = ais[rnd.Next(ais.Count)];
                mensajes.Add(new Mensaje
                {
                    Contenido = $"Mensaje de {ai.Nombre} - iteración {i + 1}",
                    FechaEnvio = DateTime.Now.AddMinutes(-rnd.Next(1, 5000)),
                    AiId = ai.AiId
                });
            }
            context.Mensajes.AddRange(mensajes);
            context.SaveChanges();


            // ------------------- RELACIONES MUCHOS A MUCHOS -------------------
            foreach (var proyecto in proyectos)
            {
                // Asigna entre 2 y 5 AIs al proyecto
                var numAIs = rnd.Next(2, 6);
                proyecto.AIs = ais.OrderBy(x => rnd.Next()).Take(numAIs).ToList();

                // Asigna entre 1 y 3 datasets
                var numDS = rnd.Next(1, 4);
                proyecto.Datasets = datasets.OrderBy(x => rnd.Next()).Take(numDS).ToList();
            }

            context.SaveChanges();
        }
    }
    public class Program
    {
        public static void Main(string[] args)
        {

            CargarDatos();
            AIsMasActivas();
            DataSetMasUsados();
            ProyectosPorEspecializacion();

            Console.ReadKey();

        }

        private static void ProyectosPorEspecializacion()
        {
            using (var context = new RedAIDbContext())
            {
                var query = 
            }
        }

        private static void DataSetMasUsados()
        {
            using (var context = new RedAIDbContext())
            {

                var query = context.Datasets
                    .Select(x=> new
                    {
                        NombreDataset = x.Nombre,
                        Numero = x.ProyectosColaborativos.Count
                    }).OrderByDescending(x=>x.Numero).Take(3).ToList();



                foreach (var q in query)
                {
                    Console.WriteLine($"Nombre del Dataset: {q.NombreDataset} --> Nº de Proyectos en los que se usa: {q.Numero}");
                }


            }
        }

        private static void AIsMasActivas()
        {
            using (var context = new RedAIDbContext())
            {

                var query = (from ai in context.AIs
                            join m in context.Mensajes
                            on ai.AiId equals m.AiId
                            group m by ai.Nombre into g
                            select new
                            {
                                NombreAI = g.Key,
                                Mensajes = g.Count()
                            }).OrderByDescending(x=>x.Mensajes).Take(3).ToList();

                foreach(var q in query)
                {
                    Console.WriteLine($"Nombre de la IA: {q.NombreAI} --> Mensajes: {q.Mensajes}");
                }


            }
        }

        private static void CargarDatos()
        {
            using (var context = new RedAIDbContext())
            {
                DataSeed.Seed(context);
                Console.WriteLine("Revisa tu BBDD por favor");

            }
        }
    }
}