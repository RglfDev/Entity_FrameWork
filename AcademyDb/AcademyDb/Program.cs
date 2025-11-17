using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyDb
{
    internal class Program
    {
        public static class DataSeeder
        {
            public static void Seed(AcademyDBEntities context)
            {
                if (context.Students.Any()) return;

                // =============================
                // === Estudiantes (Students) ===
                // =============================
                var students = new List<Student>
            {
                new Student { FirstName = "Laura", LastName = "García", Email = "laura.garcia@example.com", EnrollmentDate = new DateTime(2023, 9, 1) },
                new Student { FirstName = "Carlos", LastName = "Pérez", Email = "carlos.perez@example.com", EnrollmentDate = new DateTime(2023, 9, 2) },
                new Student { FirstName = "Ana", LastName = "Martínez", Email = "ana.martinez@example.com", EnrollmentDate = new DateTime(2023, 9, 3) },
                new Student { FirstName = "David", LastName = "López", Email = "david.lopez@example.com", EnrollmentDate = new DateTime(2023, 9, 4) },
                new Student { FirstName = "Lucía", LastName = "Sánchez", Email = "lucia.sanchez@example.com", EnrollmentDate = new DateTime(2023, 9, 5) }
            };
                context.Students.AddRange(students);
                context.SaveChanges();

                // ==========================
                // === Cursos (Courses) ===
                // ==========================
                var courses = new List<Course>
            {
                new Course { CourseName = "Programación en C#", Credits = 6, Department = "Informática" },
                new Course { CourseName = "Bases de Datos", Credits = 5, Department = "Informática" },
                new Course { CourseName = "Diseño Web", Credits = 4, Department = "Multimedia" },
                new Course { CourseName = "Inteligencia Artificial", Credits = 8, Department = "Informática" },
                new Course { CourseName = "Matemáticas Aplicadas", Credits = 6, Department = "Ciencias" }
            };
                context.Courses.AddRange(courses);
                context.SaveChanges();

                // ==============================
                // === Matrículas (Enrollments) ===
                // ==============================
                var enrollments = new List<Enrollment>
            {
                new Enrollment { Student = students[0], Course = courses[0], Grade = 9.5m },
                new Enrollment { Student = students[0], Course = courses[1], Grade = 8.7m },
                new Enrollment { Student = students[1], Course = courses[0], Grade = 7.8m },
                new Enrollment { Student = students[1], Course = courses[3], Grade = 6.5m },
                new Enrollment { Student = students[2], Course = courses[2], Grade = 9.0m },
                new Enrollment { Student = students[2], Course = courses[4], Grade = 8.2m },
                new Enrollment { Student = students[3], Course = courses[1], Grade = 7.0m },
                new Enrollment { Student = students[3], Course = courses[2], Grade = 7.9m },
                new Enrollment { Student = students[4], Course = courses[3], Grade = 9.8m },
                new Enrollment { Student = students[4], Course = courses[0], Grade = 8.9m }
            };
                context.Enrollments.AddRange(enrollments);
                context.SaveChanges();
            }
        }
        static void Main(string[] args)
        {

            CargarDatos();
            CuantosEstudiantesEnProgramacion();

        }

        private static void CuantosEstudiantesEnProgramacion()
        {
            using (var context = new AcademyDBEntities()) {
                var query = (from e in context.Enrollments
                            join c in context.Courses
                            on e.CourseId equals c.CourseId
                            group e by c.CourseName into g
                            select new
                            {
                                Curso = g.Key,
                                Estudiantes = g.Count()
                            }).ToList();

                foreach(var q in query)
                {
                    Console.WriteLine($"Curso: {q.Curso} -> Nº estudiantes: {q.Estudiantes}");
                }
            }
        }

        private static void CargarDatos()
        {
            
            using(var context = new AcademyDBEntities())
            {
                DataSeeder.Seed(context);
                Console.WriteLine("Comprueba la base de datos, datos insertados");
            }
        }
    }
}
