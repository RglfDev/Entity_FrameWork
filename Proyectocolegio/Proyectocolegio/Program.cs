using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Proyectocolegio
{
    public static class DataSeeder
    {

        public static void Seed(AcademiaDBEntities context)
        {

            if (context.Alumnos.Any()) return;

            var empresas = new List<Empresa>
    {
        new Empresa { CIF = "A11111111", Nombre = "TechCorp", Telefono = "911111111", Direccion = "Calle 1" },
        new Empresa { CIF = "B22222222", Nombre = "DataSoft", Telefono = "922222222", Direccion = "Calle 2" },
        new Empresa { CIF = "C33333333", Nombre = "WebSolutions", Telefono = "933333333", Direccion = "Calle 3" }
    };
            context.Empresas.AddRange(empresas);
            context.SaveChanges();

            var alumnos = new List<Alumno>
    {
        new Alumno { DNI = "11111111A", Nombre = "Pablo", Direccion = "Calle A", Telefono = "600111111", Edad = 28, Empresa = empresas[0] },
        new Alumno { DNI = "22222222B", Nombre = "Maria", Direccion = "Calle B", Telefono = "600222222", Edad = 32, Empresa = empresas[1] },
        new Alumno { DNI = "33333333C", Nombre = "Luis", Direccion = "Calle C", Telefono = "600333333", Edad = 25, Empresa = null }, // desempleado
        new Alumno { DNI = "44444444D", Nombre = "Ana", Direccion = "Calle D", Telefono = "600444444", Edad = 40, Empresa = empresas[2] },
        new Alumno { DNI = "55555555E", Nombre = "Sofia", Direccion = "Calle E", Telefono = "600555555", Edad = 30, Empresa = null }, // desempleado
        new Alumno { DNI = "66666666F", Nombre = "Carlos", Direccion = "Calle F", Telefono = "600666666", Edad = 27, Empresa = null } // alumno sin cursos
    };
            context.Alumnos.AddRange(alumnos);
            context.SaveChanges();

            var profesores = new List<Profesor>
    {
        new Profesor { DNI = "P1111111", Nombre = "Laura", Apellidos = "Gomez", Direccion = "Calle P1", Telefono = "911111111" },
        new Profesor { DNI = "P2222222", Nombre = "Jose", Apellidos = "Martinez", Direccion = "Calle P2", Telefono = "922222222" },
        new Profesor { DNI = "P3333333", Nombre = "Ana", Apellidos = "Lopez", Direccion = "Calle P3", Telefono = "933333333" }
    };
            context.Profesores.AddRange(profesores);
            context.SaveChanges();

            var cursos = new List<Curso>
    {
        new Curso { Codigo = "C001", Titulo = "C# Básico", Programa = "Introducción a C#", Horas = 40 },
        new Curso { Codigo = "C002", Titulo = "ASP.NET Core", Programa = "Web con .NET", Horas = 50 },
        new Curso { Codigo = "C003", Titulo = "LINQ Avanzado", Programa = "Consultas y agregaciones", Horas = 30 },
        new Curso { Codigo = "C004", Titulo = "JavaScript", Programa = "JS moderno", Horas = 35 } // curso sin alumnos
    };
            context.Cursos.AddRange(cursos);
            context.SaveChanges();

            var imparticiones = new List<Imparticion>
    {
        new Imparticion { Curso = cursos[0], Numero = 1, FechaInicio = new DateTime(2025,1,10), FechaFin = new DateTime(2025,1,20), Profesor = profesores[0] },
        new Imparticion { Curso = cursos[1], Numero = 1, FechaInicio = new DateTime(2025,2,5), FechaFin = new DateTime(2025,2,25), Profesor = profesores[1] },
        new Imparticion { Curso = cursos[2], Numero = 1, FechaInicio = new DateTime(2025,3,1), FechaFin = new DateTime(2025,3,10), Profesor = profesores[2] },
        new Imparticion { Curso = cursos[0], Numero = 2, FechaInicio = new DateTime(2025,4,1), FechaFin = new DateTime(2025,4,15), Profesor = profesores[0] }
    };
            context.Imparticiones.AddRange(imparticiones);
            context.SaveChanges();
            
            var notas = new List<Nota>
    {
        new Nota { Alumno = alumnos[0], Imparticion = imparticiones[0], Valor = 9 },
        new Nota { Alumno = alumnos[1], Imparticion = imparticiones[0], Valor = 8 },
        new Nota { Alumno = alumnos[0], Imparticion = imparticiones[1], Valor = 7 },
        new Nota { Alumno = alumnos[2], Imparticion = imparticiones[1], Valor = 6 },
        new Nota { Alumno = alumnos[3], Imparticion = imparticiones[2], Valor = 9 },
        new Nota { Alumno = alumnos[4], Imparticion = imparticiones[2], Valor = 7 },
        new Nota { Alumno = alumnos[0], Imparticion = imparticiones[3], Valor = 10 },
        new Nota { Alumno = alumnos[1], Imparticion = imparticiones[3], Valor = 9 }
    };
            context.Notas.AddRange(notas);
            context.SaveChanges();
        }
    }

    
    internal class Program
    {
        static void Main(string[] args)
        {
            CargarDatos();
            Ejercicio1();
            Ejercicio2();
            Ejercicio3();
            Ejercicio4();
            Ejercicio5();
            Ejercicio6();
            Ejercicio7();
            Ejercicio8();
            Ejercicio9();
            Ejercicio10();
            Ejercicio11();
            Ejercicio12();
            Ejercicio13();
            Ejercicio14();
            Ejercicio15();
            Ejercicio16();
            Ejercicio17();
            Ejercicio18();
            Ejercicio19();
            Ejercicio20();
            Ejercicio21();

        }


        private static void CargarDatos()
        {
            using (var context = new AcademiaDBEntities())
            {
                DataSeeder.Seed(context);
                Console.WriteLine("Datos Insertados, por favor, revisa tu base de datos");

            }
        }
        private static void Ejercicio1()
            //Listar todos los alumnos trabajadores junto con el nombre de la empresa
        {
            using (var context = new AcademiaDBEntities())
            {
                var query = (from a in context.Alumnos
                             join e in context.Empresas
                             on a.EmpresaId equals e.EmpresaId
                             select new
                             {
                                 DNI = a.DNI,
                                 NombreA = a.Nombre,
                                 NombreE = e.Nombre
                             }).ToList();

                

            }
        }
        private static void Ejercicio2()
            //Obtener la lista de cursos que ha realizado un alumno concreto, indicando las fechas de inicio
            //y fin de cada curso y la nota obtenida.
            //Entrada: DNI del alumno.

        {
            using(var context = new AcademiaDBEntities())
            {
                var query = (from a in context.Alumnos
                             where a.DNI == "11111111A"
                             select a).SelectMany(x => x.Notas)
                            .Select(c => new
                            {
                                Nombre = c.Alumno.Nombre,
                                NCurso = c.Imparticion.Curso.Titulo,
                                FInicio = c.Imparticion.FechaInicio,
                                FFin = c.Imparticion.FechaFin,
                                NotaAlumno = c.Valor
                            }).ToList().Select(c => new
                            {
                                c.Nombre,
                                c.NCurso,
                                Inicio = c.FInicio.ToShortDateString(),
                                Fin = c.FFin.ToShortDateString(),
                                c.NotaAlumno
                            }).ToList();

            }
        }
        private static void Ejercicio3()
        {
            using (var context = new AcademiaDBEntities())
                //Listar los profesores junto con el número de cursos que han impartido.
                //Incluir solo aquellos profesores que hayan impartido al menos un curso.

            {
                var query = (from p in context.Profesores
                             join i in context.Imparticiones
                             on p.ProfesorId equals i.ProfesorId
                             group i by p.Nombre into g
                             select new
                             {
                                 NombreProfe = g.Key,
                                 NumeroClases = g.Count()
                             }).Where(x=>x.NumeroClases>0).ToList();
            }

        }
        private static void Ejercicio4()
        {
            //Obtener todos los alumnos, independientemente de si han hecho algún curso o no, y mostrar su DNI,
            //nombre y el número de cursos realizados.
            //Esto implica un LEFT JOIN o equivalente en LINQ.

            using ( var context = new AcademiaDBEntities())
            {

                var query = (from a in context.Alumnos
                             join n in context.Notas
                             on a.AlumnoId equals n.AlumnoId
                             into conjunto
                             from sub in conjunto.DefaultIfEmpty()
                             group sub by new { a.Nombre, a.DNI } into g
                             select new
                             {
                                 NombreAlumno = g.Key.Nombre,
                                 DNI = g.Key.DNI,
                                 Cursos = g.Count(x=>x!=null)
                             }).ToList();
            }

        }
        private static void Ejercicio5()
        {
            using (var context = new AcademiaDBEntities())
            {
                //Listar los cursos con su duración total (horas) y el número de alumnos matriculados en cada curso.
                var query = (from c in context.Cursos
                             join im in context.Imparticiones
                             on c.CursoId equals im.CursoId
                             group im by c.Titulo into g
                             select new
                             {
                                 Nombre = g.Key,
                                 Horas = g.Sum(x =>DbFunctions.DiffHours(x.FechaInicio, x.FechaFin)),
                                 Matriculados = g.SelectMany(x => x.Notas)
                                  .Select(n => n.AlumnoId)
                                  .Distinct()
                                  .Count()
                             }).ToList();
            }
        }
        private static void Ejercicio6()
        {
            //Listar todos los alumnos trabajadores mayores de 30 años que hayan
            //obtenido una nota superior a 8 en algún curso.

            using (var context = new AcademiaDBEntities())
            {

                var query = (from a in context.Alumnos
                             join n in context.Notas
                             on a.AlumnoId equals n.AlumnoId
                             where a.EmpresaId != null && a.Edad > 30 && n.Valor > 8
                             select new
                             {
                                 NAlumno = a.Nombre,
                                 Edad = a.Edad,
                                 NEmpresa = a.Empresa.Nombre,
                                 Anota = n.Valor

                             }).ToList();

            }

        }
        private static void Ejercicio7()
        //Obtener el promedio de notas por curso, mostrando el código de curso y el promedio
        //de todos los alumnos que lo han realizado.
        {
            using (var context = new AcademiaDBEntities())
            {

                var query = (from c in context.Cursos
                             join im in context.Imparticiones
                             on c.CursoId equals im.CursoId
                             group im by c.Codigo into g
                             select new
                             {
                                 CodigoCurso = g.Key,
                                 MediaNotas = g.SelectMany(x => x.Notas).Average(x=>x.Valor),
                                 MediaAlumnos = g.Average(x=>x.Notas.Count())

                             }).ToList();

            }
        }
        private static void Ejercicio8()
        //Listar todos los alumnos y sus cursos, incluyendo los cursos
        //que todavía no han sido calificados (nota nula).////////////////////////////////////////////////////////
        {
            using (var context = new AcademiaDBEntities())
            {
                var query = context.Alumnos
                    .Select(a => new
                    {
                        Alumno = a.Nombre,
                        Cursos = a.Notas.DefaultIfEmpty() // cada alumno puede no tener notas
                    })
                    .SelectMany(a => a.Cursos, (a, n) => new
                    {
                        Alumno = a.Alumno,
                        Curso = n != null ? n.Imparticion.Curso.Titulo : "Sin curso",
                        Nota = n != null ? n.Valor : (decimal?)null
                    })
                    .ToList();

                foreach (var item in query)
                {
                    Console.WriteLine($"Alumno: {item.Alumno}, Curso: {item.Curso}, Nota: {(item.Nota.HasValue ? item.Nota.Value.ToString("F2") : "Sin calificar")}");
                }
            }
        }
        private static void Ejercicio9()
        //Listar los alumnos que han realizado más de 3 cursos distintos,
        //mostrando su DNI, nombre y número total de cursos.

        {
            using (var context = new AcademiaDBEntities())
            {
                var query = (from a in context.Alumnos
                             join n in context.Notas
                             on a.AlumnoId equals n.AlumnoId
                             group n by new { a.Nombre, a.DNI } into g
                             where g.Select(x => x.Imparticion.CursoId).Distinct().Count() > 3
                             select new
                             {
                                 Nombre = g.Key.Nombre,
                                 DNI = g.Key.DNI,
                                 Totalcursos = g.Select(x => x.Imparticion.CursoId).Distinct().Count()
                             }).ToList();

                foreach (var item in query)
                {
                    Console.WriteLine($"Alumno: {item.Nombre}, DNI: {item.DNI}, Cursos realizados: {item.Totalcursos}");
                }
            }
        }
        private static void Ejercicio10()
        {
            //Obtener todos los cursos impartidos por un profesor concreto, mostrando el titulo del curso,
            //las fechas de inicio y fin y el numero de alumnos matriculados en cada curso.

            using (var context = new AcademiaDBEntities())
            {
                var query = context.Imparticiones
                        .SelectMany(x => x.Notas)
                        .Where(c => c.Imparticion.Profesor.DNI == "P1111111")
                        .GroupBy(x => new
                        {
                            x.Imparticion.Curso.Titulo,
                            x.Imparticion.FechaInicio,
                            x.Imparticion.FechaFin
                        }).ToList()
                        .Select(x => new
                        {
                            Nombre = x.Key.Titulo,
                            Inicio = x.Key.FechaInicio.ToShortDateString(),
                            Fin = x.Key.FechaFin.ToShortDateString(),
                            Alumnos = x.Select(z => z.AlumnoId).Distinct().Count()
                        }).ToList();
            }
        }
        private static void Ejercicio11()
        {
            using (var context = new AcademiaDBEntities())
            {
                //Listar cursos que no tienen ningun alumno matriculado

                var query = (from c in context.Cursos
                            where c.Imparticions
                            .SelectMany(x => x.Notas).Count()==0
                            select new
                            {
                                Curso = c.Titulo,
                                Codigo = c.Codigo
                            }).ToList();
            }
        }
        private static void Ejercicio12()
        {
            //Obtener un listado de alumnos trabajadores que hayan realizado algun curso
            //junto con el nombre de su empresa y la nota mas alta obtenida por cada alumno.
            using (var context = new AcademiaDBEntities())
            {

                var query = (from a in context.Alumnos
                             where a.Notas.Count() > 0 && a.EmpresaId != null
                             select new
                             {
                                 Nombre = a.Nombre,
                                 Empresa = a.Empresa.Nombre,
                                 NotaMasAlta = a.Notas.Max(x => x.Valor)
                             }).ToList();
            }
        }
        private static void Ejercicio13()
        {
            using (var context = new AcademiaDBEntities())
            {
                //Listar todos los cursos ordenados por fecha de inicio, incluyendo el nombre del profesor
                //y el numero de alumnos matriculados

                var query = context.Imparticiones
                        .GroupBy(x => new
                        {
                            x.FechaInicio,
                            NombreProf = x.Profesor.Nombre
                        })
                        .ToList().Select(x => new
                        {
                            Fecha = x.Key.FechaInicio.ToShortDateString(),
                            NombreProfe = x.Key.NombreProf,
                            NumeroA = x.SelectMany(z => z.Notas)
                                       .Select(n => n.AlumnoId)
                                       .Distinct()
                                       .Count()
                        })
                        .OrderBy(x => x.Fecha)
                        .ToList();
            }
        }
        private static void Ejercicio14()
        {
            //Obtener los alumnos desempleados que hayan realizado cursos de mas de 40 horas,
            //mostrando su nombre, edad y titulo del curso

            using (var context = new AcademiaDBEntities())
            {

                var query = (from n in context.Notas
                             where n.Alumno.EmpresaId == null
                             && DbFunctions.DiffHours(n.Imparticion.FechaInicio, n.Imparticion.FechaFin) > 40
                             select new
                             {
                                 Nombre = n.Alumno.Nombre,
                                 Edad = n.Alumno.Edad,
                                 Curso = n.Imparticion.Curso.Titulo
                             }).ToList();
            }
        }
        private static void Ejercicio15()
        {
            using (var context = new AcademiaDBEntities())
                //Listar los cursos que se han impartido mas de una vez,
                //mostrando el codigo del curso, numero de veces que se ha impartido
                //y la fecha de la primera y la ultima imparticion.
            {

                var query = (from i in context.Imparticiones
                             group i by i.CursoId into g
                             where g.Count() > 1
                             select new
                             {
                                 CodigoCurso = g.Key,
                                 VecesImpartido = g.Count(),
                                 PrimeraFecha = g.Min(x => x.FechaInicio),
                                 UltimaFecha = g.Max(x => x.FechaFin)
                             })
                            .ToList()
                            .Select(n=> new
                            {
                                n.CodigoCurso,
                                n.VecesImpartido,
                                PrimeroaFecha = n.PrimeraFecha.ToShortDateString(),
                                UltimaFecha = n.UltimaFecha.ToShortDateString()
                            }).ToList();
            }
        }
        private static void Ejercicio16()
            //Listar los cursos agrupados por profesor, mostrando cuantos alumnos ha tenido cada curso.
        {
            using(var context =  new AcademiaDBEntities())
            {
                var query = (from im in context.Imparticiones
                            join p in context.Profesores on im.ProfesorId equals p.ProfesorId
                            join c in context.Cursos on im.CursoId equals c.CursoId
                            join n in context.Notas on im.ImparticionId equals n.ImparticionId
                            join al in context.Alumnos on n.AlumnoId equals al.AlumnoId
                            group new { c, al } by new { p.Nombre, c.Titulo } into grupo
                            select new
                            {
                                Profesor = grupo.Key.Nombre,
                                Curso = grupo.Key.Nombre, 
                                CantidadAlumnos = grupo.Select(x => x.al.AlumnoId).Distinct().Count()
                            }).ToList();


            }
            
        }
        private static void Ejercicio17()
            //Obtener los cursos de mayor duracion, mostrando titulo, horas y el nombre del profesor
        {
            using (var context = new AcademiaDBEntities())
            {
                var query = (from c in context.Cursos
                            join im in context.Imparticiones
                            on c.CursoId equals im.CursoId
                            group im by c.Titulo into g
                            select new
                            {
                                NombreCurso = g.Key,
                                Horas = g.Sum(x => DbFunctions.DiffHours(x.FechaInicio, x.FechaFin)),
                                Profe = g.Select(c => c.Profesor.Nombre).FirstOrDefault()
                            }).ToList();
            }
        }
        private static void Ejercicio18()
        //Obtener el promedio de edad de los alumnos que han realizado cursos de mas de 50 horas
        {
            using (var context = new AcademiaDBEntities())
            {
                var query =  context.Notas
                            .Where(x => DbFunctions.DiffHours(x.Imparticion.FechaInicio, x.Imparticion.FechaFin) > 50)
                            .Select(n => n.Alumno.Edad)
                            .Distinct().
                            Average();
            }
        }
        private static void Ejercicio19()
        {
            //Listar los cursos que tienen alumnos trabajadores y desempleados, mostrando el
            //titulo del curso y el numero de cada tipo de alumno.
            using(var context = new AcademiaDBEntities())
            {
                var query = (from c in context.Cursos
                             join im in context.Imparticiones
                             on c.CursoId equals im.CursoId
                             join n in context.Notas
                             on im.ImparticionId equals n.ImparticionId
                             select new
                             {
                                 Curso = c.Titulo,
                                 Alumno = n.Alumno
                             }).GroupBy(g => g.Curso)
                            .Select(g => new
                            {
                                Titulo = g.Key,
                                Empleados = g.Count(a=>a.Alumno.EmpresaId !=null),
                                Desempleados = g.Count(a=>a.Alumno.EmpresaId ==null)
                            }).Where(x=> x.Empleados >0 && x.Desempleados >0).ToList();
            }
        }
        private static void Ejercicio20()
        {
            //Obtener los alumnos que han participado en cursos
            //impartidos por un profesor concreto y han obtenido
            //una nota superior al promedio de ese curso

            using(var context = new AcademiaDBEntities())
            {
                var query = (from a in context.Alumnos
                             join n in context.Notas
                             on a.AlumnoId equals n.AlumnoId
                             join im in context.Imparticiones
                             on n.ImparticionId equals im.ImparticionId
                             where im.Profesor.Nombre == "Ana" && n.Valor > im.Notas.Average(c => c.Valor)
                             select new
                             {
                                 Profe = im.Profesor.Nombre,
                                 Curso = im.Curso.Titulo,
                                 Alumno = a.Nombre,
                                 Nota = n.Valor
                             }).ToList();
            }
        }
        private static void Ejercicio21()
        {
            using(var context = new AcademiaDBEntities())
                //Obtener el curso con la mayor cantidad de alumnos matriculados y
                //mostrar el titulo, el profesor y el numero total de alumnos.
            {
                var query = (from c in context.Cursos
                             join im in context.Imparticiones 
                             on c.CursoId equals im.CursoId
                             select new
                             {
                                 Curso = c.Titulo,
                                 NombreProfe = im.Profesor.Nombre,
                                 NumAlumnos = im.Notas.Select(n => n.AlumnoId).Distinct().Count()
                             })
             .OrderByDescending(x => x.NumAlumnos)
             .FirstOrDefault();
            }
        }


    }
}
