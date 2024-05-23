using HRORMApp.Entities;
using HRORMApp.DbContexts;
using System.Net.Mime;
using HRORMApp.Repositories;
using HRORMApp.Repositories.Library;
using HRORMApp.DbContexts.SchoolLibrary;
using HRORMApp.Repositories.SchoolLibrary;

/*using ( var context=new HRContext())
{
    /* 
     //Insert new employee
     var newEmp = new Employee
     {
         FirstName = "Mahesh",
         LastName = "Panhale",
         Contact = "9887634567",
         Email = "mahesh.p@gamil.com",
         Id = 901
     };
     context.Employees.Add(newEmp);
     context.SaveChanges();

     var employees=context.Employees.ToList();
     foreach( var employee in employees)
     {
         Console.WriteLine(employee.FirstName + "  "+ employee.LastName + "  "+ employee.Contact);
     }
    */


/*
//update existing employee
var empToUpdate = context.Employees.FirstOrDefault(emp => emp.Id == 901);
if(empToUpdate != null)
{
    empToUpdate.FirstName = "Suresh";
    empToUpdate.LastName = "Mane";
    context.SaveChanges();
}
*/

//Delete existing employee
/*
    var empToUpdate = context.Employees.FirstOrDefault(emp => emp.Id == 901);
    if (empToUpdate != null)
    { 
        context.Employees.Remove(empToUpdate);
        context.SaveChanges();
    }
*/

//Show all Employees
/*
    EmployeeRepository repo=new EmployeeRepository(context);
    var employees =repo.GetAll();   
    foreach (var employee in employees)
    {
        Console.WriteLine(employee.FirstName + "  " + employee.LastName + "  " + employee.Contact);
    }
}
*/


/*

using (var context = new LibraryContext())
{
    LibraryRepository repo=new LibraryRepository(context);
    repo.Initialize();  
    repo.ShowAll();
}

*/

using (var context=new SchoolLibraryContext())
{
    SchoolLibraryRepository repo = new SchoolLibraryRepository(context);
    //repo.Insert();
    repo.ShowBookStudents();
    Console.WriteLine("***************************************************************************************");
    repo.ShowBookAuthors();
}