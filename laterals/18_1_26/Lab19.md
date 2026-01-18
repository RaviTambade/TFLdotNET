# 🧪 Lab Assignment

## **Async Web API – HR Employee Controller**

Below is a **end-to-end Lab Assignment** on
👉 **Async ASP.NET Core Web API**
👉 **HR Controller (Employee API)**

This lab connects **async/await** with **real Web API usage**, exactly how students will see it in **industry projects**.

## 🎯 Lab Objective

To build an **asynchronous HR Web API** that:

* Uses `async` / `await` correctly
* Exposes REST endpoints for Employees
* Separates **Controller → Service → Repository**
* Simulates real **DB / I/O latency**



## 🧠 Concepts Covered

| Concept                  | Purpose                    |
| ------------------------ | -------------------------- |
| Async Controller Actions | Non-blocking HTTP requests |
| `Task<IActionResult>`    | Async API responses        |
| Repository Pattern       | Clean data access          |
| RESTful Endpoints        | Industry API design        |


## 🏗️ Solution Structure

```
HRAsyncWebApi
│
├── Program.cs
│
├── Controllers
│   └── EmployeesController.cs
│
├── Models
│   └── Employee.cs
│
├── Repositories
│   ├── IEmployeeRepository.cs
│   └── EmployeeRepository.cs
│
└── Services
    └── EmployeeService.cs
```

## 🧱 Step 1: Create ASP.NET Core Web API

```bash
dotnet new webapi -n HRAsyncWebApi
cd HRAsyncWebApi
```

## 🧱 Step 2: Employee Domain Model

📄 **Models/Employee.cs**

```csharp
namespace HRAsyncWebApi.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public double Salary { get; set; }
    }
}
```

## 🧠 Step 3: Repository Interface (Async)

📄 **Repositories/IEmployeeRepository.cs**

```csharp
using HRAsyncWebApi.Models;

namespace HRAsyncWebApi.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(int id);
        Task AddAsync(Employee employee);
    }
}
```

## 🧠 Step 4: Repository Implementation (Simulated DB)

📄 **Repositories/EmployeeRepository.cs**

```csharp
using HRAsyncWebApi.Models;

namespace HRAsyncWebApi.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private static readonly List<Employee> _employees = new()
        {
            new Employee { Id=101, Name="Amit", Department="IT", Salary=50000 },
            new Employee { Id=102, Name="Neha", Department="HR", Salary=40000 }
        };

        public async Task<List<Employee>> GetAllAsync()
        {
            await Task.Delay(1000); // simulate DB latency
            return _employees;
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            await Task.Delay(500);
            return _employees.FirstOrDefault(e => e.Id == id);
        }

        public async Task AddAsync(Employee employee)
        {
            await Task.Delay(500);
            _employees.Add(employee);
        }
    }
}
```

## 🧠 Step 5: Service Layer (Business Logic)

📄 **Services/EmployeeService.cs**

```csharp
using HRAsyncWebApi.Models;
using HRAsyncWebApi.Repositories;

namespace HRAsyncWebApi.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public Task<List<Employee>> GetEmployeesAsync()
            => _repository.GetAllAsync();

        public Task<Employee?> GetEmployeeAsync(int id)
            => _repository.GetByIdAsync(id);

        public Task AddEmployeeAsync(Employee employee)
            => _repository.AddAsync(employee);
    }
}
```

## 🧠 Step 6: Async HR Controller

📄 **Controllers/EmployeesController.cs**

```csharp
using HRAsyncWebApi.Models;
using HRAsyncWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HRAsyncWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeService _service;

        public EmployeesController(EmployeeService service)
        {
            _service = service;
        }

        // GET: api/employees
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _service.GetEmployeesAsync();
            return Ok(employees);
        }

        // GET: api/employees/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _service.GetEmployeeAsync(id);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        // POST: api/employees
        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            await _service.AddEmployeeAsync(employee);
            return CreatedAtAction(nameof(GetById),
                new { id = employee.Id }, employee);
        }
    }
}
```

## 🧱 Step 7: Register Services (DI)

📄 **Program.cs**

```csharp
using HRAsyncWebApi.Repositories;
using HRAsyncWebApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<EmployeeService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.Run();
```

## 🧪 API Testing (Swagger / Postman)

### 🔹 GET All Employees

```
GET /api/employees
```

### 🔹 GET Employee by Id

```
GET /api/employees/101
```

### 🔹 Add Employee

```
POST /api/employees
{
  "id": 103,
  "name": "Ravi",
  "department": "Finance",
  "salary": 60000
}
```

## 🧠 HR Analogy Mapping

| Web API Concept | HR Meaning                    |
| --------------- | ----------------------------- |
| Controller      | HR front desk                 |
| Service         | HR policy processing          |
| Repository      | Employee master database      |
| Async           | HR works on multiple requests |



## 🧪 Student Tasks (Hands-On)

### ✅ Task 1

Add async endpoint:

```
GET /api/employees/department/{dept}
```

### ✅ Task 2

Add async salary update API

### ✅ Task 3

Add validation (salary > 0)

### ✅ Task 4

Convert repository to **EF Core async**


## ❓ Interview Questions

1. Why should Web API controllers be async?
2. What happens if async is removed?
3. Difference between `Task<IActionResult>` and `IActionResult`?
4. Why async improves scalability, not speed?

---

## 🧩 Mentor Notes

This lab:

* Connects **async → Web API → Clean Architecture**
* Prepares students for:

  * EF Core async
  * Microservices
  * Cloud APIs
* Naturally leads to:

  * JWT Security
  * Logging
  * Exception middleware
