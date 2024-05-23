using HRORMApp.DbContexts;
using HRORMApp.Entities.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HRORMApp.Repositories
{
    public class LibraryRepository
    {
        private LibraryContext _context;
        public LibraryRepository(LibraryContext context)
        {
            _context = context;
        }

        public void Initialize()
        {
            using (var context = new LibraryContext())
            {
                var author = new Author { Name = "P.L. Deshpande" };
                author.Books = new List<Book>
                {
                    new Book { Title = "Batatychi Chal" },
                    new Book { Title = "Purvrang" },
                    new Book { Title = "Gulacha Ganpati" }
                };
                context.Authors.Add(author);
                context.SaveChanges();
            }
        }
        public void ShowAll()
        {
            using (var context = new LibraryContext())
            {
                var authorsWithBooks = context.Authors.Include(a => a.Books).ToList();

                foreach (var author in authorsWithBooks)
                {
                    Console.WriteLine($"Author: {author.Name}");

                    foreach (var book in author.Books)
                    {
                        Console.WriteLine($" - Book: {book.Title}");
                    }
                }
            }
        }
    }
}
