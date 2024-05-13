using AuthLib.Entities;
using AuthLib.Repositories;

/*
Console.WriteLine("Enter registered Email");
string email=Console.ReadLine();

Console.WriteLine("Enter password");
string word = Console.ReadLine();

bool status = false;

CredentialRepository repo = new CredentialRepository();
//repo.ShowAllCredentials();

status =repo.Validate(email, word);
if (status)
{
    Console.WriteLine("Welcome " + email + " to Transflower");
}
else
{
    Console.WriteLine("Invalid User , Please try again");
}

*/


//Repository Fetching data
HRRepository hrRepo =new HRRepository();
List<Employee> allEmployees=hrRepo.GetAllEmployee();

//Presentation Logic
foreach (Employee employee in allEmployees)
{
    Console.WriteLine(employee.Id + " "+ employee.FirstName+ " "+ employee.LastName+ "  "+ employee.Contact);
}
