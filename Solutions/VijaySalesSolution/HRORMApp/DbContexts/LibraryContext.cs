using HRORMApp.Entities.Library;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRORMApp.DbContexts
{

    /*
     
        CREATE TABLE Authors (
                            AuthorId INT PRIMARY KEY IDENTITY,
                            Name NVARCHAR(100) NOT NULL
        );

        CREATE TABLE Books (
                            BookId INT PRIMARY KEY IDENTITY,
                            Title NVARCHAR(100) NOT NULL,
                            AuthorId INT NOT NULL,
                            FOREIGN KEY (AuthorId) REFERENCES Authors(AuthorId)
        );

     */

    public class LibraryContext : DbContext
    {
        public string connectionString = @"Data Source=DESKTOP-H1K53PL\SQLEXPRESS;Initial Catalog=AssessmentDB;Integrated Security=True;Connect Timeout=30;Encrypt=False";

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
