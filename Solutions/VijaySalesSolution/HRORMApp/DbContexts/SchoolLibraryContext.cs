
using HRORMApp.Entities.SchoolLibrary;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRORMApp.DbContexts.SchoolLibrary
{

    /*
     *  Many to many relationship authors, books and students
     
        CREATE TABLE Authors (
                            AuthorId INT PRIMARY KEY IDENTITY,
                            Name NVARCHAR(100) NOT NULL
        );
        
       CREATE TABLE Students (
                            StudentId INT PRIMARY KEY IDENTITY,
                            Name NVARCHAR(100) NOT NULL
        );

        CREATE TABLE Books (
                            BookId INT PRIMARY KEY IDENTITY,
                            Title NVARCHAR(100) NOT NULL
        );

       CREATE TABLE BookAuthor (
                            BookAuthorId INT PRIMARY KEY IDENTITY,
                            BookId  INT NOT NULL,
                            AuthorId INT NOT NULL,
                            FOREIGN KEY (BookId) REFERENCES Books(BookId),
                            FOREIGN KEY (AuthorId) REFERENCES Authors(AuthorId)
        );

       CREATE TABLE Bookstudent (
                            BookStudentId INT PRIMARY KEY IDENTITY,
                            BookId INT NOT NULL,
                            StudentId INT NOT NULL,
                            FOREIGN KEY (StudentId) REFERENCES Students(StudentId)
                            FOREIGN KEY (BookId) REFERENCES Books(BookId)
        );

     */

    public class SchoolLibraryContext : DbContext
    {
        public string connectionString = @"Data Source=DESKTOP-H1K53PL\SQLEXPRESS;Initial Catalog=SchoolLibrary;Integrated Security=True;Connect Timeout=30;Encrypt=False";

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookAuthor>()
                .HasKey(ba => new { ba.BookId, ba.AuthorId });

            modelBuilder.Entity<BookStudent>()
                .HasKey(bs => new { bs.BookId, bs.StudentId });
        }
    }
}
