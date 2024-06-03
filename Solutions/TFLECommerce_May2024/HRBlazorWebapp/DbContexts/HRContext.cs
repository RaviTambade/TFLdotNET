using System;
using System.Collections.Generic;
 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transflower.Entities;
using Microsoft.EntityFrameworkCore;


namespace Transflower.DbContexts
{
    //step 2: Extend UserDefined Class from DBContext
    public class HRContext:DbContext
    {
        //Step 3: Define DbSet of Employee as property
        public DbSet<Employee> Employees { get; set; }

       public string connectionString = @"Data Source=DESKTOP-H1K53PL\SQLEXPRESS;Initial Catalog=AssessmentDB;Integrated Security=True;Connect Timeout=30;Encrypt=False";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
