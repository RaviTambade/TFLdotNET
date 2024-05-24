using HRORMApp.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HRORMApp.Entities.SchoolLibrary;
using HRORMApp.DbContexts.SchoolLibrary;

namespace HRORMApp.Repositories.SchoolLibrary
{
    public class SchoolLibraryRepository
    {
        private SchoolLibraryContext _context;
        
        public SchoolLibraryRepository(SchoolLibraryContext context)
        {
            _context = context;
        }

        //CRUD Operations
        public void Insert()
        {
            using (var context = new SchoolLibraryContext())
            {
                // Create Authors
                var author1 = new Author { Name = "Yashwant Kanetkar" };
                var author2 = new Author { Name = "Charles Petzold" };
                var author3 = new Author { Name = "Jefry Rischter" };
                var author4 = new Author { Name = "Don Box" };
                var author5 = new Author { Name = "Ratan Tata" };
                var author6 = new Author { Name = "Anand Mahindra" };
                var author7 = new Author { Name = "Sachin Tendulkar" };
                var author8 = new Author { Name = "Somesh Kumar" };


                // Create Books
                var book1 = new Book { Title = "Let us C" };
                var book2 = new Book { Title = "Advaned Windows" };
                var book3 = new Book { Title = "Modern India Vision 2050" };

                // Associate Authors with Books
                book1.BookAuthors = new List<BookAuthor>
                {
                    new BookAuthor { Author = author1 },
                    new BookAuthor { Author = author2 }
                };

                book2.BookAuthors = new List<BookAuthor>
                {
                    new BookAuthor { Author = author3 }
                };

                book3.BookAuthors = new List<BookAuthor>
                {
                    new BookAuthor { Author = author5 },
                    new BookAuthor { Author = author6 },
                    new BookAuthor { Author = author7 },
                    new BookAuthor { Author = author8 }
                };

                // Create Students
                var student1 = new Student { Name = "Sanjeev" };
                var student2 = new Student { Name = "Rajeev" };
                var student3 = new Student { Name = "Amit" };
                var student4 = new Student { Name = "Chirag" };
                var student5 = new Student { Name = "Manisha" };
                var student6 = new Student { Name = "Sarika" };
                var student7 = new Student { Name = "Manoj" };
                var student8 = new Student { Name = "Ramakant" };


                // Associate Books with Students
                book1.BookStudents = new List<BookStudent>
                {
                    new BookStudent { Student = student1 }
                };
                book2.BookStudents = new List<BookStudent>
                {
                    new BookStudent { Student = student1 },
                    new BookStudent { Student = student2 }
                };
                book3.BookStudents = new List<BookStudent>
                {
                    new BookStudent { Student = student1 },
                    new BookStudent { Student = student3},
                    new BookStudent { Student = student2 },
                    new BookStudent { Student = student5 },
                    new BookStudent { Student = student7 },
                    new BookStudent { Student = student4 }
                };

                // Add entities to the context and save changes
                context.AddRange(author1, author2,author3,author4,author5,author6,author7,author8,
                                book1, book2,book3,
                                student1, student2,student3,student4,student5,student6,student7,student8);
                context.SaveChanges();
            }
        }
        public void ShowBookAuthors()
        {
            using (var context = new SchoolLibraryContext())
            {
                // Retrieve Books with Authors
                var booksWithAuthors = context.Books.Include(b => b.BookAuthors).ThenInclude(ba => ba.Author).ToList();
              
                foreach (var book in booksWithAuthors)
                {
                    Console.WriteLine($"Book: {book.Title}");
                    foreach (var bookAuthor in book.BookAuthors)
                    {
                        Console.WriteLine($"  Author: {bookAuthor.Author.Name}");         //good practice
                        //Console.WriteLine("Author : " + bookAuthor.Author.Name + " ");  //bad practice
                    }
                }
            }
        }

        public void ShowBookStudents()
        {
            using (var context = new SchoolLibraryContext())
            {  
                // Retrieve Books with Students
                var booksWithStudents = context.Books.Include(b => b.BookStudents).ThenInclude(bs => bs.Student).ToList();
                foreach (var book in booksWithStudents)
                {
                    Console.WriteLine($"Book: {book.Title}");
                    foreach (var bookStudent in book.BookStudents)
                    {
                        Console.WriteLine($"  Student: {bookStudent.Student.Name}");
                    }
                }
            }
        }

        public void Update()
        {
            using (var context = new SchoolLibraryContext())
            {
                var bookToUpdate = context.Books.FirstOrDefault(b => b.Title == "Let us C");

                if (bookToUpdate != null)
                {
                    // Update book's title
                    bookToUpdate.Title = "Updated Let us C";

                    // Update book's authors
                    foreach (var bookAuthor in bookToUpdate.BookAuthors)
                    {
                        bookAuthor.Author.Name = "Updated Achyut Godbole";
                    }

                    // Update book's students
                    foreach (var bookStudent in bookToUpdate.BookStudents)
                    {
                        bookStudent.Student.Name = "Updated Manoj";
                    }

                    context.SaveChanges();
                }
            }
        }
        public void Delete()
        {
            using (var context = new SchoolLibraryContext())
            {
                var bookToDelete = context.Books.FirstOrDefault(b => b.Title == "Windows SDK Programming");

                if (bookToDelete != null)
                {
                    context.Books.Remove(bookToDelete);
                    context.SaveChanges();
                }
            }
        }
    }
}