using HRORMApp.Entities.SchoolLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRORMApp.Entities.SchoolLibrary
{

    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public List<BookAuthor> BookAuthors { get; set; }
        public List<BookStudent> BookStudents { get; set; }
    }
}
