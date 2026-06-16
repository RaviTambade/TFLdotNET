namespace MaxNewYorkInsurance.Models;

public class Employee
{
    public int EmployeeId { get; set; }

    public string EmployeeCode { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string FullName => $"{FirstName} {LastName}";

    public string Email { get; set; } = string.Empty;

    public string MobileNumber { get; set; } = string.Empty;

    public string Department { get; set; } = string.Empty;
    // e.g. Sales, Claims, Accounts, Renewal, Customer Support

    public string Designation { get; set; } = string.Empty;
    // e.g. Executive, Manager, Officer

    public DateTime DateOfJoining { get; set; }

    public decimal Salary { get; set; }

    public bool IsActive { get; set; }

    public string Address { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"{EmployeeCode} - {FullName} ({Department})";
    }
}


/*

Employee employee = new Employee
{
    EmployeeId = 101,
    EmployeeCode = "EMP1001",
    FirstName = "Anita",
    LastName = "Patil",
    Email = "anita.patil@example.com",
    MobileNumber = "9876543210",
    Department = "Claims",
    Designation = "Claims Officer",
    DateOfJoining = new DateTime(2024, 7, 1),
    Salary = 55000m,
    Address = "Pune, Maharashtra",
    IsActive = true
};*/

