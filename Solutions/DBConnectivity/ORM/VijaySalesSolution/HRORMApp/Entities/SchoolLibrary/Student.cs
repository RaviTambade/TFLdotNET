using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRORMApp.Entities.SchoolLibrary
{
    public  class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public List<BookStudent> BookStudents { get; set; }
    }
}
