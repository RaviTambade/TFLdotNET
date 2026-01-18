# 🧪 Lab Assignment

## **Async Web API + JWT Security – HR Solution**

Below is a **complete, mentor-style, industry-ready Lab Assignment** that combines
👉 **Async ASP.NET Core Web API**
👉 **JWT Authentication & Authorization**
using the **HR Solution domain**.

This lab represents a **real production HR API** and is a **milestone lab** for employability.


## 🎯 Lab Objective

To build a **secure HR Employee Web API** that:

* Uses **async/await** end-to-end
* Secures APIs using **JWT Authentication**
* Implements **role-based authorization**
* Follows **Controller → Service → Repository** flow


## 🧠 Concepts Covered

| Concept           | Purpose                    |
| ----------------- | -------------------------- |
| Async Controllers | Non-blocking HTTP requests |
| JWT Token         | Secure authentication      |
| Claims & Roles    | Authorization              |
| `[Authorize]`     | Protect APIs               |
| Clean layering    | Maintainability            |


## 🏗️ Solution Structure

```
HRJwtAsyncApi
│
├── Program.cs
│
├── Controllers
│   ├── AuthController.cs
│   └── EmployeesController.cs
│
├── Models
│   ├── Employee.cs
│   └── LoginRequest.cs
│
├── Repositories
│   ├── IEmployeeRepository.cs
│   └── EmployeeRepository.cs
│
└── Services
    ├── EmployeeService.cs
    └── JwtTokenService.cs
```

## 🧱 Step 1: Create Web API Project

```bash
dotnet new webapi -n HRJwtAsyncApi
cd HRJwtAsyncApi
```

Add NuGet package:

```bash
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
```

## 🧱 Step 2: Employee Domain Model

📄 **Models/Employee.cs**

```csharp
namespace HRJwtAsyncApi.Models
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

📄 **Models/LoginRequest.cs**

```csharp
namespace HRJwtAsyncApi.Models
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Role { get; set; }
    }
}
```

## 🧠 Step 3: Repository Layer (Async)

📄 **Repositories/IEmployeeRepository.cs**

```csharp
using HRJwtAsyncApi.Models;

namespace HRJwtAsyncApi.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllAsync();
    }
}
```

📄 **Repositories/EmployeeRepository.cs**

```csharp
using HRJwtAsyncApi.Models;

namespace HRJwtAsyncApi.Repositories
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
            await Task.Delay(1000); // simulate DB
            return _employees;
        }
    }
}
```

## 🧠 Step 4: Service Layer

📄 **Services/EmployeeService.cs**

```csharp
using HRJwtAsyncApi.Models;
using HRJwtAsyncApi.Repositories;

namespace HRJwtAsyncApi.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _repo;

        public EmployeeService(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public Task<List<Employee>> GetEmployeesAsync()
            => _repo.GetAllAsync();
    }
}
```

## 🧠 Step 5: JWT Token Service

📄 **Services/JwtTokenService.cs**

```csharp
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HRJwtAsyncApi.Services
{
    public class JwtTokenService
    {
        private readonly string _key = "THIS_IS_SUPER_SECRET_KEY";

        public string GenerateToken(string username, string role)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "HRSystem",
                audience: "HRUsers",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
```

## 🧠 Step 6: Authentication Controller

📄 **Controllers/AuthController.cs**

```csharp
using HRJwtAsyncApi.Models;
using HRJwtAsyncApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HRJwtAsyncApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenService _tokenService;

        public AuthController(JwtTokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            // Simulated authentication
            var token = _tokenService.GenerateToken(request.Username, request.Role);
            return Ok(new { Token = token });
        }
    }
}
```

## 🧠 Step 7: Secured HR Controller (Async)

📄 **Controllers/EmployeesController.cs**

```csharp
using HRJwtAsyncApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRJwtAsyncApi.Controllers
{
    [ApiController]
    [Route("api/employees")]
    [Authorize]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeService _service;

        public EmployeesController(EmployeeService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "HR")]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _service.GetEmployeesAsync();
            return Ok(employees);
        }
    }
}
```

## 🧱 Step 8: Configure JWT Authentication

📄 **Program.cs**

```csharp
using HRJwtAsyncApi.Repositories;
using HRJwtAsyncApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddSingleton<JwtTokenService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "HRSystem",
        ValidAudience = "HRUsers",
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("THIS_IS_SUPER_SECRET_KEY"))
    };
});

builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
```

## 🧪 API Testing Flow (Swagger / Postman)

### 🔹 1. Login (Get Token)

```
POST /api/auth/login
{
  "username": "admin",
  "role": "HR"
}
```

### 🔹 2. Call Secured API

```
GET /api/employees
Authorization: Bearer <JWT_TOKEN>
```


## 🧠 HR Analogy Mapping

| Security Concept | HR Meaning                   |
| ---------------- | ---------------------------- |
| JWT Token        | Employee ID card             |
| Claims           | Designation & access         |
| Authorization    | Permission check             |
| Async            | Multiple HR requests handled |



## 🧪 Student Tasks (Mandatory)

### ✅ Task 1

Add **Manager role** and restrict salary visibility

### ✅ Task 2

Add token expiry handling

### ✅ Task 3

Add `[AllowAnonymous]` public endpoint

### ✅ Task 4

Move JWT key to `appsettings.json`


## ❓ Interview Questions

1. Why JWT is stateless?
2. Difference between Authentication & Authorization?
3. Why async is critical in secured APIs?
4. What happens if token is tampered?


## 🧩 Mentor Notes 

This lab:

* Is **real interview-level project**
* Covers:

  * Async
  * Security
  * Clean architecture
* Perfect foundation for:

  * Microservices
  * API Gateway
  * Cloud deployment



### 🔜 Next Advanced Labs

✅ Refresh Token Implementation
✅ Role-based Policies
✅ Global Exception Middleware
✅ Clean Architecture HR API (Final Capstone)

If you want this converted into:
📄 **Lab manual PDF**
🧪 **Evaluation rubric**
🎯 **Interview assignment pack**

Just say 👍
