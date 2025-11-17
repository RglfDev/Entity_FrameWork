using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

//Ejercico contectado a LibraryDb perfecto para realizar consultas Linq Sincronas


namespace LibrarySb
{
    public static class DataSeeder
    {
        public static void Seed(LibraryDBEntities context)
        {
            if (context.Authors.Any()) return;

            // === Géneros literarios ===
            var genres = new List<Genre>
            {
                new Genre { GenreName = "Poetry" },
                new Genre { GenreName = "Novel" },
                new Genre { GenreName = "Drama" },
                new Genre { GenreName = "Essay" },
                new Genre { GenreName = "Short Story" }
            };

            context.Genres.AddRange(genres);
            context.SaveChanges();

            // === Autores ===
            var authors = new List<Author>
            {
                new Author { FirstName = "William", LastName = "Shakespeare", BirthDate = new DateTime(1564, 4, 23) },
                new Author { FirstName = "Jane", LastName = "Austen", BirthDate = new DateTime(1775, 12, 16) },
                new Author { FirstName = "Pablo", LastName = "Neruda", BirthDate = new DateTime(1904, 7, 12) },
                new Author { FirstName = "Charles", LastName = "Dickens", BirthDate = new DateTime(1812, 2, 7) },
                new Author { FirstName = "Miguel", LastName = "de Cervantes", BirthDate = new DateTime(1547, 9, 29) }
            };

            context.Authors.AddRange(authors);
            context.SaveChanges();

            // === Libros ===
            var books = new List<Book>
            {
                new Book { Title = "Hamlet", PublicationYear = 1603, Author = authors[0], Genre = genres[2] },
                new Book { Title = "Romeo and Juliet", PublicationYear = 1597, Author = authors[0], Genre = genres[2] },
                new Book { Title = "Pride and Prejudice", PublicationYear = 1813, Author = authors[1], Genre = genres[1] },
                new Book { Title = "Emma", PublicationYear = 1815, Author = authors[1], Genre = genres[1] },
                new Book { Title = "Twenty Love Poems and a Song of Despair", PublicationYear = 1924, Author = authors[2], Genre = genres[0] },
                new Book { Title = "Oliver Twist", PublicationYear = 1838, Author = authors[3], Genre = genres[1] },
                new Book { Title = "A Tale of Two Cities", PublicationYear = 1859, Author = authors[3], Genre = genres[1] },
                new Book { Title = "Don Quijote", PublicationYear = 1605, Author = authors[4], Genre = genres[1] }
            };

            context.Books.AddRange(books);
            context.SaveChanges();
        }
    }
    

    internal class Program
    {
       
        static void Main(string[] args)
        {
            CrearDatos();

            ConsultarAutores();

        }

        private static void ConsultarAutores()
        {
            using (var context = new LibraryDBEntities())
            {
                var query = from a in context.Authors
                            select a.FirstName + " " + a.LastName;
            

            foreach(var q in query)
            {
                Console.WriteLine($"Autor {q}");
            }
            }
        }

        private static void CrearDatos()
        {
            using (var context = new LibraryDBEntities())
            {
                DataSeeder.Seed(context);
                Console.WriteLine("Datos insertados correctamente, por favor, mira en tu base de datos");
            }
        }
    }
}
