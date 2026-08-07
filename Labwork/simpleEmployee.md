 
# C# OOP Assignment — Employee Pay Calculator

## Objective

Create a C# class named `Employee` that stores employee information and calculates the employee's salary.

The assignment should help you practice:

* Class and Object
* Properties (`get` / `set`)
* Constructor
* Methods
* Calculations
* Encapsulation
* Conditional logic



## Problem Statement

Create an `Employee` class for a payroll application.

Each employee has the following information:

| Property    | Data Type | Description                    |
| ----------- | --------- | ------------------------------ |
| EmployeeId  | `int`     | Unique employee ID             |
| FirstName   | `string`  | Employee first name            |
| LastName    | `string`  | Employee last name             |
| Email       | `string`  | Employee email address         |
| BasicSalary | `decimal` | Monthly basic salary           |
| HRA         | `decimal` | House Rent Allowance           |
| DailyWages  | `decimal` | Salary earned per working day  |
| WorkingDays | `int`     | Number of days employee worked |



## 1. Create the Employee Class

Create a class:

```csharp
public class Employee
{
    // Properties

    // Constructor

    // Methods
}
```

Add properties using getter and setter:

```csharp
public int EmployeeId { get; set; }

public string FirstName { get; set; }

public string LastName { get; set; }

public string Email { get; set; }

public decimal BasicSalary { get; set; }

public decimal HRA { get; set; }

public decimal DailyWages { get; set; }

public int WorkingDays { get; set; }
```


# 2. Create Constructor

Create a constructor that initializes employee information.

Example:

```csharp
public Employee(
    int employeeId,
    string firstName,
    string lastName,
    string email,
    decimal basicSalary,
    decimal hra,
    decimal dailyWages,
    int workingDays)
{
    EmployeeId = employeeId;
    FirstName = firstName;
    LastName = lastName;
    Email = email;
    BasicSalary = basicSalary;
    HRA = hra;
    DailyWages = dailyWages;
    WorkingDays = workingDays;
}
```



# 3. Compute Basic Pay

Create a method:

```csharp
public decimal ComputeBasicPay()
{
    // Calculate basic pay
}
```

Formula:

```text
Basic Pay = Daily Wages × Working Days
```

For example:

```text
Daily Wages = ₹1,000
Working Days = 22

Basic Pay = 1000 × 22
          = ₹22,000
```



# 4. Compute HRA

Create a method:

```csharp
public decimal ComputeHRA()
{
    // Calculate HRA
}
```

For this assignment, assume:

```text
HRA = 20% of Basic Pay
```

Formula:

```text
HRA = Basic Pay × 20 / 100
```



# 5. Compute Gross Pay

Create:

```csharp
public decimal ComputeGrossPay()
{
    // Calculate gross pay
}
```

Formula:

```text
Gross Pay = Basic Pay + HRA
```


# 6. Compute Pay

Create the main method:

```csharp
public decimal ComputePay()
{
    // Calculate total salary
}
```

For this assignment:

```text
Pay = Basic Pay + HRA
```

You may internally call:

```csharp
ComputeBasicPay();
ComputeHRA();
```

instead of duplicating the calculation.


# 7. Get Full Name

Create a method:

```csharp
public string GetFullName()
{
    // Return FirstName + LastName
}
```

Expected result:

```text
Ravi Tambade
```



# 8. Display Employee Details

Create:

```csharp
public void DisplayEmployeeDetails()
{
    // Display employee information
}
```

Expected output:

```text
-----------------------------------
Employee Details
-----------------------------------
Employee ID    : 101
Name           : Ravi Tambade
Email          : ravi@example.com
Daily Wages    : 1000
Working Days   : 22
Basic Salary   : 22000
HRA            : 4400
Gross Pay      : 26400
-----------------------------------
```



# 9. Main Program

Create an object of `Employee` in `Program.cs`.

Example:

```csharp
Employee employee = new Employee(
    101,
    "Ravi",
    "Tambade",
    "ravi@example.com",
    22000,
    4400,
    1000,
    22);
```

Call:

```csharp
Console.WriteLine(employee.GetFullName());

Console.WriteLine(
    $"Basic Pay : {employee.ComputeBasicPay()}");

Console.WriteLine(
    $"HRA : {employee.ComputeHRA()}");

Console.WriteLine(
    $"Gross Pay : {employee.ComputeGrossPay()}");

Console.WriteLine(
    $"Total Pay : {employee.ComputePay()}");

employee.DisplayEmployeeDetails();
```



# Additional Methods — Student Challenge

After completing the basic assignment, add the following methods.

### 1. Compute Annual Pay

```csharp
public decimal ComputeAnnualPay()
{
    // Monthly Pay × 12
}
```

Formula:

```text
Annual Pay = Monthly Pay × 12
```


### 2. Compute Tax

Assume:

```text
Tax = 10% of Gross Pay
```

Create:

```csharp
public decimal ComputeTax()
{
}
```

---

### 3. Compute Net Pay

```csharp
public decimal ComputeNetPay()
{
}
```

Formula:

```text
Net Pay = Gross Pay - Tax
```

---

### 4. Check Full-Time Employee

Create:

```csharp
public bool IsFullTimeEmployee()
{
}
```

Rule:

```text
Working Days >= 20
```

Return:

```text
true
```

or

```text
false
```



# Final Challenge

Modify the program to accept employee information from the user:

```text
Enter Employee ID    : 101
Enter First Name     : Ravi
Enter Last Name      : Tambade
Enter Email          : ravi@example.com
Enter Daily Wages    : 1000
Enter Working Days   : 22
```

Then display:

```text
===================================
        EMPLOYEE PAY SLIP
===================================

Employee ID    : 101
Employee Name  : Ravi Tambade
Email          : ravi@example.com

Daily Wages    : ₹1000
Working Days   : 22

Basic Pay      : ₹22000
HRA            : ₹4400
Gross Pay      : ₹26400
Tax            : ₹2640
Net Pay        : ₹23760

Annual Pay     : ₹285120

Full Time      : Yes

===================================
```

##  Mentor Challenge

Do **not** put all calculations inside `Main()`.

`Main()` should only create the object and ask the object to perform its responsibilities:

```text
Program
   │
   ▼
Employee Object
   │
   ├── GetFullName()
   ├── ComputeBasicPay()
   ├── ComputeHRA()
   ├── ComputeGrossPay()
   ├── ComputeTax()
   ├── ComputeNetPay()
   ├── ComputeAnnualPay()
   └── IsFullTimeEmployee()
```

### Learning Goal

The key lesson is:

> **An object should contain both its data and the behavior that operates on that data.**

This is the first step toward understanding **Object-Oriented Programming and encapsulation**.