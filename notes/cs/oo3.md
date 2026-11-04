# Clean Architecture in C#

**Previous session: SOLID Principles in C#**

Today's question: We have learned how to write maintainable classes using SOLID principles. But how do we organize hundreds of classes into a maintainable enterprise application?

## 1. Begin with a real-world story

Imagine Transflower is building a Student Management System. The system should support:

- Student registration
- Course enrollment
- Fee payment
- Attendance tracking
- Assessment and results
- Notifications to students

Initially, a developer creates one ASP.NET Core project and puts everything inside it.

```
StudentManagement
│
├── Controllers
├── Models
├── Services
├── Database
├── Email
├── Payment
└── Reports
```

As the application grows, developers face problems:

- Changing the database affects business logic.
- Replacing email services requires multiple code changes.
- Testing business rules requires database connectivity.
- Controllers become overloaded.
- Developers struggle to understand dependencies.

**Mentor asks**: Can SOLID principles help us organize an entire application?

**Student**: They help us design individual classes. But we need an architectural structure to organize those classes.

**Mentor**: Exactly! That is where Clean Architecture comes into the picture.

## 2. What is Clean Architecture?

Clean Architecture is an approach to organizing software so that business rules remain independent of external technologies. Its central principle is: Dependencies should point towards the core business logic, not away from it.

```
+-------------------------------------------------------------+
|                    EXTERNAL WORLD                           |
|                                                             |
|   Web UI       Mobile App       Database       External API |
|      |             |               |                |       |
|      +-------------+---------------+----------------+       |
|                            |                                |
|  +-------------------------------------------------------+  |
|  |              FRAMEWORKS & DRIVERS                     |  |
|  |                                                       |  |
|  |  ASP.NET Core     MySQL       Entity Framework        |  |
|  |                                                       |  |
|  |  +-------------------------------------------------+  |  |
|  |  |          INTERFACE ADAPTERS                     |  |  |
|  |  |                                                 |  |  |
|  |  |  Controllers   Presenters   Repositories        |  |  |
|  |  |                                                 |  |  |
|  |  |  +-------------------------------------------+  |  |  |
|  |  |  |             APPLICATION                   |  |  |  |
|  |  |  |                                           |  |  |  |
|  |  |  |  Use Cases                                |  |  |  |
|  |  |  |  Register Student                         |  |  |  |
|  |  |  |  Enroll Student                           |  |  |  |
|  |  |  |  Calculate Result                         |  |  |  |
|  |  |  |                                           |  |  |  |
|  |  |  |  +-------------------------------------+  |  |  |  |
|  |  |  |  |              DOMAIN                 |  |  |  |  |
|  |  |  |  |                                     |  |  |  |  |
|  |  |  |  |  Entities                           |  |  |  |  |
|  |  |  |  |  Business Rules                     |  |  |  |  |
|  |  |  |  |  Student, Course, Enrollment        |  |  |  |  |
|  |  |  |  +-------------------------------------+  |  |  |  |
|  |  |  +-------------------------------------------+  |  |  |
|  |  +-------------------------------------------------+  |  |
|  +-------------------------------------------------------+  |
+-------------------------------------------------------------+

           DEPENDENCY DIRECTION:  OUTSIDE ---> INSIDE
```

Think about an onion.

- Outer layers contain implementation details.
- Inner layers contain important business rules.
- The core should not depend on databases, web frameworks or external services.

## 3. Understand the four architectural layers

| Layer          | Responsibility                      | Example                        |
| -------------- | ----------------------------------- | ------------------------------ |
| Domain         | Core business concepts and rules    | Student, Course, Enrollment    |
| Application    | Use cases and application workflows | RegisterStudent, EnrollStudent |
| Infrastructure | External technology implementations | MySQL, Email, File Storage     |
| Presentation   | Interaction with users or clients   | ASP.NET Core Web API           |

### Architecture diagram

```
             CLIENT / BROWSER
                    |
                    v
       +---------------------------+
       |      PRESENTATION        |
       |                           |
       |  ASP.NET Core Controllers |
       |  Request / Response DTOs  |
       +---------------------------+
                    |
                    v
       +---------------------------+
       |       APPLICATION         |
       |                           |
       |  Use Cases                |
       |  Services                 |
       |  Interfaces               |
       |  DTOs                     |
       +---------------------------+
                    |
                    v
       +---------------------------+
       |          DOMAIN           |
       |                           |
       |  Student                  |
       |  Course                   |
       |  Enrollment               |
       |  Business Rules           |
       +---------------------------+

       INFRASTRUCTURE
       +---------------------------+
       | MySQL Repository          |
       | Email Notification        |
       | Payment Gateway           |
       | External Services         |
       +---------------------------+

       Infrastructure implements
       interfaces defined inward.
```

Important: Infrastructure is not supposed to be called directly by the Domain layer. The Application layer defines the contracts it needs; Infrastructure implements them.

## 4. Understand dependency direction

Let's consider a simple example. Suppose we want to save a student. A naive implementation:

```
public class StudentService
{
    private readonly MySqlConnection connection;

    public StudentService()
    {
        connection = new MySqlConnection("server=localhost;database=students;");
    }

    public void SaveStudent(Student student)
    {
        // Save student into MySQL
    }
}
```

What is the problem? `StudentService` directly depends on MySQL. Tomorrow, if we replace MySQL with SQL Server, we may need to modify the service. Let's redesign it using abstraction.

```
public interface IStudentRepository
{
    void Add(Student student);
}
```

Application service:

```
public class StudentService
{
    private readonly IStudentRepository repository;

    public StudentService(IStudentRepository repository)
    {
        this.repository = repository;
    }

    public void Register(Student student)
    {
        repository.Add(student);
    }
}
```

Infrastructure implementation:

```
public class MySqlStudentRepository : IStudentRepository
{
    public void Add(Student student)
    {
        // MySQL insert operation
    }
}
```

Now the application depends on an abstraction rather than a database implementation.

**Mentor asks**: Who creates the actual MySQL repository?

**Student**: The dependency injection container creates it and supplies it to StudentService.

**Mentor**: Correct! We are applying the Dependency Inversion Principle from our previous session.

## 5. Organize the ASP.NET Core solution

Let's create a solution with four projects.

```
StudentManagement.sln
│
├── StudentManagement.Domain
│   ├── Entities
│   │   ├── Student.cs
│   │   └── Course.cs
│   └── Interfaces
│
├── StudentManagement.Application
│   ├── Interfaces
│   │   └── IStudentRepository.cs
│   ├── DTOs
│   └── Services
│       └── StudentService.cs
│
├── StudentManagement.Infrastructure
│   ├── Persistence
│   │   └── MySqlStudentRepository.cs
│   └── DependencyInjection.cs
│
└── StudentManagement.API
    ├── Controllers
    │   └── StudentsController.cs
    └── Program.cs
```

### Project reference relationships

```
StudentManagement.API
       |
       +------> Application
       |
       +------> Infrastructure
                    |
                    +------> Application

Application
       |
       +------> Domain

Infrastructure
       |
       +------> Domain (if required)

Domain
       |
       +------> No project dependencies
```

Notice that Domain is independent.

Infrastructure depends on Application to implement its contracts. The API is the composition root: it wires the implementation to the abstraction.

## 6. Build the Domain layer

Create `Student.cs`.

```
namespace StudentManagement.Domain.Entities;

public class Student
{
    public int Id { get; private set; }

    public string Name { get; private set; }

    public string Email { get; private set; }

    public Student(string name, string email)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.");

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required.");

        Name = name;
        Email = email;
    }
}
```

**Mentor explains**:

The Domain layer should contain business concepts and business rules. It should not know about:

- HTTP requests
- ASP.NET Core controllers
- MySQL connections
- Entity Framework Core
- Email providers

## 7. Build the Application layer

Create `IStudentRepository.cs`.

```
using StudentManagement.Domain.Entities;

namespace StudentManagement.Application.Interfaces;

public interface IStudentRepository
{
    Task AddAsync(Student student);
    Task<Student?> GetByIdAsync(int id);
}
```

Create `StudentService.cs`.

```
using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Application.Services;

public class StudentService
{
    private readonly IStudentRepository repository;

    public StudentService(IStudentRepository repository)
    {
        this.repository = repository;
    }

    public async Task RegisterAsync(string name, string email)
    {
        var student = new Student(name, email);

        await repository.AddAsync(student);
    }

    public Task<Student?> GetStudentAsync(int id)
    {
        return repository.GetByIdAsync(id);
    }
}
```

Here, Application coordinates the use case: registering a student.

## 8. Build the Infrastructure layer

For this introductory lab, we will use an in-memory repository. Later, we can replace it with MySQL or Entity Framework Core.

```
using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Infrastructure.Persistence;

public class InMemoryStudentRepository : IStudentRepository
{
    private readonly List<Student> students = new();

    public Task AddAsync(Student student)
    {
        students.Add(student);
        return Task.CompletedTask;
    }

    public Task<Student?> GetByIdAsync(int id)
    {
        return Task.FromResult(students.FirstOrDefault(s => s.Id == id));
    }
}
```

For a production application, the repository would also need a proper identity-generation and persistence strategy. This simple example is intended to demonstrate architectural boundaries.

## 9. Connect everything using Dependency Injection

In the API project's `Program.cs`:

```
using StudentManagement.Application.Interfaces;
using StudentManagement.Application.Services;
using StudentManagement.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IStudentRepository,InMemoryStudentRepository>();
builder.Services.AddScoped<StudentService>();

var app = builder.Build();

app.MapControllers();
app.Run();
```

The DI container now knows which implementation to provide whenever `IStudentRepository` is requested.

## 10. Create the Presentation layer

Create `StudentsController.cs`.

```
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.Services;

namespace StudentManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly StudentService service;

    public StudentsController(StudentService service)
    {
        this.service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterStudentRequest request)
    {
        await service.RegisterAsync(request.Name,request.Email);
        return Ok("Student registered successfully.");
    }
}

public record RegisterStudentRequest(string Name,string Email);
```

The controller handles HTTP communication. It does not contain database logic or student registration rules.

## 11. Understand the complete execution flow

```
        POST /api/students
                 |
                 v
       StudentsController
                 |
                 v
          StudentService
                 |
                 v
         Student Entity
          Validates data
                 |
                 v
        IStudentRepository
                 |
                 v
     InMemoryStudentRepository
                 |
                 v
            Data Saved
```

Later, we can replace `InMemoryStudentRepository` with `MySqlStudentRepository` without changing the Domain or Application business rules.

## 12. Build and run the solution

Create the solution and projects:

```
dotnet new sln -n StudentManagement

dotnet new classlib -n StudentManagement.Domain
dotnet new classlib -n StudentManagement.Application
dotnet new classlib -n StudentManagement.Infrastructure
dotnet new webapi -n StudentManagement.API
```

Add projects to the solution:

```
dotnet sln add StudentManagement.Domain
dotnet sln add StudentManagement.Application
dotnet sln add StudentManagement.Infrastructure
dotnet sln add StudentManagement.API
```

Add project references:

```
dotnet add StudentManagement.Application reference StudentManagement.Domain
dotnet add StudentManagement.Infrastructure reference StudentManagement.Application
dotnet add StudentManagement.Infrastructure reference StudentManagement.Domain
dotnet add StudentManagement.API reference StudentManagement.Application
dotnet add StudentManagement.API reference StudentManagement.Infrastructure
```

Build:

```
dotnet build
```

Run:

```
dotnet run --project StudentManagement.API
```

Test the API using Postman:

```
POST /api/students
Content-Type: application/json
```

```
{
  "name": "Amit",
  "email": "amit@example.com"
}
```

Expected response:

```
Student registered successfully.
```

## 13. Mentor's comparison: Traditional vs Clean Architecture

| Traditional tightly coupled design        | Clean Architecture                |
| ----------------------------------------- | --------------------------------- |
| Business logic mixed with database code   | Business logic separated          |
| Service directly creates database objects | Service depends on interfaces     |
| Difficult to replace infrastructure       | Infrastructure can be replaced    |
| Testing requires external dependencies    | Business logic can be unit tested |
| Changes spread across layers              | Changes are more localized        |

## 14. Practical assignment – Transflower Student Management API

Hands-on Lab : **Build a Student Management API using Clean Architecture.**

Requirements:

1. Register a student.
2. Retrieve a student by ID.
3. Retrieve all students.
4. Update student details.
5. Enroll a student in a course.
6. Prevent duplicate enrollment.
7. Keep business rules inside the appropriate layer.
8. Use dependency injection for repository implementations.

**Extension challenge**: Replace the in-memory repository with MySQL without changing the Domain layer or the student registration use case.

### Is Clean Architecture always necessary ?

```
       SMALL APPLICATION
              |
              v
      Simple Architecture
              |
              v
       Build Features
              |
              v
      Avoid Over-Engineering


       ENTERPRISE APPLICATION
              |
              v
       Clean Architecture
              |
              v
       Clear Boundaries
              |
              v
       Independent Testing
              |
              v
       Sustainable Evolution

```

**Mentor's message**:

"Architecture is not about following rules blindly. Architecture is about making decisions that reduce the cost of future change."

This is an important distinction for students: **Don't create four projects just to claim that you are following Clean Architecture. Understand the dependency boundaries first.**

## 15. Closing thought from the Mentor

Students, remember:

- SOLID helps us design good classes.
- Clean Architecture helps us organize those classes.
- Dependency Injection helps us connect implementations.
- Testing helps us validate our design.
- Good architecture helps software evolve without unnecessary disruption.

A good developer writes working code. An experienced engineer designs software that can continue to evolve.

— Ravi Tambade, Chief Mentor, Transflower Learning
