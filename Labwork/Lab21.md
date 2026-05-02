Below is your **FINAL CAPSTONE LAB** — designed exactly for **industry readiness**, **placement interviews**, and **mentorship-driven learning**, aligned with everything you’ve been building so far in the **HR Solution series**.

This is the **culmination lab** that ties together **Clean Architecture + SOLID + Async + Repository + JWT + LINQ + DI**.

---

# 🏆 FINAL CAPSTONE LAB

## **Clean Architecture HR Management API**

> **Level:** Advanced / Industry-Ready
> **Type:** End-to-End Backend Capstone
> **Outcome:** “I can design real-world APIs, not just write controllers”

---

## 🎯 Capstone Goal

Design and implement a **production-grade HR Management API** using:

✅ **Clean Architecture**
✅ **Async / Await**
✅ **JWT Authentication & Role-based Authorization**
✅ **Repository Pattern**
✅ **SOLID Principles**
✅ **LINQ & DTO Mapping**

---

## 🧠 What This Lab Proves

After completing this lab, a student can confidently say:

> “I understand **how real enterprise APIs are structured**, not just how to write code.”

---

## 🏛️ Clean Architecture Overview (HR Context)

```
┌─────────────────────────────┐
│        Presentation         │  → Controllers, DTOs
├─────────────────────────────┤
│        Application          │  → UseCases, Interfaces
├─────────────────────────────┤
│           Domain            │  → Entities, Rules
├─────────────────────────────┤
│        Infrastructure       │  → DB, Repositories, JWT
└─────────────────────────────┘
```

👉 **Dependencies always point INWARD**

---

## 🗂️ Solution Structure (Must Follow)

```
HRManagementSolution
│
├── HR.Domain
│   ├── Entities
│   │   └── Employee.cs
│   └── Interfaces
│       └── IEmployeeRepository.cs
│
├── HR.Application
│   ├── DTOs
│   │   └── EmployeeDto.cs
│   ├── Interfaces
│   │   └── IEmployeeService.cs
│   └── Services
│       └── EmployeeService.cs
│
├── HR.Infrastructure
│   ├── Repositories
│   │   └── EmployeeRepository.cs
│   └── Security
│       └── JwtTokenService.cs
│
└── HR.API
    ├── Controllers
    │   ├── AuthController.cs
    │   └── EmployeesController.cs
    └── Program.cs
```

---

## 🧱 LAYER 1: Domain (Pure Business)

📁 **HR.Domain**

### 🔹 Entity: Employee

```csharp
namespace HR.Domain.Entities
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

### 🔹 Repository Contract

```csharp
namespace HR.Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(int id);
        Task AddAsync(Employee employee);
    }
}
```

✔ No EF
✔ No Web
✔ No JWT

---

## 🧱 LAYER 2: Application (Use Cases)

📁 **HR.Application**

### 🔹 DTO (API-safe model)

```csharp
namespace HR.Application.DTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
    }
}
```

### 🔹 Service Interface

```csharp
namespace HR.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDto>> GetEmployeesAsync();
        Task AddEmployeeAsync(EmployeeDto dto);
    }
}
```

### 🔹 Service Implementation (LINQ + Async)

```csharp
using HR.Application.DTOs;
using HR.Application.Interfaces;
using HR.Domain.Entities;
using HR.Domain.Interfaces;

namespace HR.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<EmployeeDto>> GetEmployeesAsync()
        {
            var employees = await _repository.GetAllAsync();

            return employees.Select(e => new EmployeeDto
            {
                Id = e.Id,
                Name = e.Name,
                Department = e.Department
            }).ToList();
        }

        public async Task AddEmployeeAsync(EmployeeDto dto)
        {
            var employee = new Employee
            {
                Name = dto.Name,
                Department = dto.Department,
                Salary = 30000
            };

            await _repository.AddAsync(employee);
        }
    }
}
```

✔ LINQ
✔ Business rules
✔ No Web dependency

---

## 🧱 LAYER 3: Infrastructure (Implementation)

📁 **HR.Infrastructure**

### 🔹 Repository Implementation

```csharp
using HR.Domain.Entities;
using HR.Domain.Interfaces;

namespace HR.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private static readonly List<Employee> _employees = new();

        public async Task<List<Employee>> GetAllAsync()
        {
            await Task.Delay(500);
            return _employees;
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            await Task.Delay(200);
            return _employees.FirstOrDefault(e => e.Id == id);
        }

        public async Task AddAsync(Employee employee)
        {
            await Task.Delay(200);
            employee.Id = _employees.Count + 1;
            _employees.Add(employee);
        }
    }
}
```

### 🔹 JWT Token Service

```csharp
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HR.Infrastructure.Security
{
    public class JwtTokenService
    {
        public string GenerateToken(string username, string role)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("HR_SECRET_KEY"));

            var creds = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "HRSystem",
                audience: "HRUsers",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
```

---

## 🧱 LAYER 4: API (Presentation)

📁 **HR.API**

### 🔹 Auth Controller

```csharp
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly JwtTokenService _jwt;

    public AuthController(JwtTokenService jwt)
    {
        _jwt = jwt;
    }

    [HttpPost("login")]
    public IActionResult Login(string username, string role)
    {
        var token = _jwt.GenerateToken(username, role);
        return Ok(token);
    }
}
```

### 🔹 Employee Controller (Secured)

```csharp
[ApiController]
[Route("api/employees")]
[Authorize(Roles = "HR")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _service;

    public EmployeesController(IEmployeeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
        => Ok(await _service.GetEmployeesAsync());

    [HttpPost]
    public async Task<IActionResult> Post(EmployeeDto dto)
    {
        await _service.AddEmployeeAsync(dto);
        return Ok();
    }
}
```

---

## ⚙️ Dependency Injection (Program.cs)

```csharp
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddSingleton<JwtTokenService>();
```

---

## 🧪 Mandatory Student Tasks

### ✅ Level 1 (Core)

✔ Implement Clean Architecture strictly
✔ Async everywhere
✔ JWT secured endpoints

### ✅ Level 2 (Advanced)

✔ Add Manager role
✔ Add salary visibility policy
✔ Add validation

### ✅ Level 3 (Expert)

✔ Global exception middleware
✔ Logging
✔ Move secrets to config

---

## 🎯 Interview Mapping

| Interview Question   | This Lab Covers |
| -------------------- | --------------- |
| Clean Architecture   | ✅               |
| SOLID                | ✅               |
| Async API design     | ✅               |
| JWT Security         | ✅               |
| Real-world structure | ✅               |

---

## 🧠 Mentor Closing Note (Very Important)

This **Final Capstone** is:

> ❌ Not a tutorial
> ✅ A **proof of capability**

If a student **can explain this project clearly**, they are **industry-ready backend developers**.

