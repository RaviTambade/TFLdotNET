# ASP.NET Core Web API – Step-by-Step Unit Testing and API Testing Lab

Transflower Practical Testing Lab

Objective: Teach students how to test an ASP.NET Core Web API using two different approaches:

1. Unit Testing: Test individual methods and business logic independently.
2. API Testing: Test actual HTTP endpoints, request/response, validation and application behavior.

We will build a simple Student Management Web API using ASP.NET Core, C#, xUnit and ASP.NET Core integration testing.

## 1. Understand the testing architecture

First, understand what we are testing.

```
             STUDENT MANAGEMENT WEB APP
             ==========================

                HTTP Client
              (Postman / Browser)
                     |
                     v
             +---------------+
             |  API Layer    |
             |               |
             | Students      |
             | Controller    |
             +-------+-------+
                     |
                     v
             +---------------+
             | Business Logic|
             |               |
             | StudentService|
             +-------+-------+
                     |
                     v
             +---------------+
             | Data Layer    |
             |               |
             | Repository    |
             +-------+-------+
                     |
                     v
             +---------------+
             | MySQL Database|
             +---------------+


                 TESTING LAYERS

          +------------------------+
          |       Unit Tests       |
          | Test StudentService    |
          | Test business rules    |
          +------------------------+

          +------------------------+
          |       API Tests        |
          | HTTP GET / POST / PUT  |
          | HTTP Status / JSON     |
          +------------------------+
```

## 2. Create ASP.NET Core Web API project

Open Visual Studio or VS Code terminal.

```
# Create solution
dotnet new sln -n StudentManagement

# Create Web API
dotnet new webapi -n StudentAPI

# Create xUnit test project
dotnet new xunit -n StudentAPI.Tests

# Add projects to solution
dotnet sln add StudentAPI/StudentAPI.csproj

dotnet sln add StudentAPI.Tests/StudentAPI.Tests.csproj

# Add project reference
dotnet add StudentAPI.Tests/StudentAPI.Tests.csproj reference StudentAPI/StudentAPI.csproj
```

Expected folder structure:

```
StudentManagement/
|
+-- StudentAPI/
|   |
|   +-- Controllers/
|   |      StudentsController.cs
|   |
|   +-- Models/
|   |      Student.cs
|   |
|   +-- Services/
|   |      StudentService.cs
|   |
|   +-- Program.cs
|   +-- StudentAPI.csproj
|
+-- StudentAPI.Tests/
       |
       +-- StudentServiceTests.cs
       +-- StudentsApiTests.cs
       +-- StudentAPI.Tests.csproj
```

## 3. Create Student Model

File: `Models/Student.cs`

```
namespace StudentAPI.Models;

public class Student
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public string Email { get; set; } = "";

    public string Course { get; set; } = "";
}
```

## 4. Create Business Logic – StudentService

File: `Services/StudentService.cs`

```
using StudentAPI.Models;

namespace StudentAPI.Services;

public class StudentService
{
    private readonly List<Student> students = new()
    {
        new Student
        {
            Id = 1,
            Name = "Amit",
            Email = "amit@test.com",
            Course = ".NET"
        },
        new Student
        {
            Id = 2,
            Name = "Sneha",
            Email = "sneha@test.com",
            Course = "React"
        }
    };

    public List<Student> GetAll()
    {
        return students;
    }

    public Student? GetById(int id)
    {
        return students.FirstOrDefault(s => s.Id == id);
    }

    public Student Add(Student student)
    {
        student.Id = students.Count + 1;

        students.Add(student);

        return student;
    }

    public bool Delete(int id)
    {
        var student = GetById(id);

        if (student == null)
            return false;

        students.Remove(student);

        return true;
    }
}
```

This first version uses an in-memory collection, so students can focus on testing before introducing MySQL.

## 5. Create Students API Controller

File: `Controllers/StudentsController.cs`

```
using Microsoft.AspNetCore.Mvc;
using StudentAPI.Models;
using StudentAPI.Services;

namespace StudentAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly StudentService service;

    public StudentsController(StudentService service)
    {
        this.service = service;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(service.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var student = service.GetById(id);

        if (student == null)
            return NotFound();

        return Ok(student);
    }

    [HttpPost]
    public IActionResult Add(Student student)
    {
        var result = service.Add(student);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Id },
            result
        );
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (!service.Delete(id))
            return NotFound();

        return NoContent();
    }
}
```

Register the service in `Program.cs`:

```
using StudentAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<StudentService>();

var app = builder.Build();

app.MapControllers();

app.Run();

// Expose Program for integration testing
public partial class Program { }
```

## 6. Run the Web API

```
dotnet run --project StudentAPI
```

Test the following endpoints using Postman or browser:

| HTTP Method | Endpoint           | Expected result |
| ----------- | ------------------ | --------------- |
| GET         | `/api/students`    | 200 OK          |
| GET         | `/api/students/1`  | 200 OK          |
| GET         | `/api/students/99` | 404 Not Found   |
| POST        | `/api/students`    | 201 Created     |
| DELETE      | `/api/students/1`  | 204 No Content  |
| DELETE      | `/api/students/99` | 404 Not Found   |

## 7. Unit Testing – Test StudentService

Now we will test business logic independently, without running the Web API or connecting to a database.

Install / verify xUnit packages

The `dotnet new xunit` command normally installs the required xUnit dependencies.

File: `StudentAPI.Tests/StudentServiceTests.cs`

```
using StudentAPI.Models;
using StudentAPI.Services;

namespace StudentAPI.Tests;

public class StudentServiceTests
{
    [Fact]
    public void GetAll_ShouldReturnStudents()
    {
        // Arrange
        var service = new StudentService();

        // Act
        var result = service.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void GetById_ShouldReturnStudent()
    {
        // Arrange
        var service = new StudentService();

        // Act
        var result = service.GetById(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Amit", result.Name);
    }

    [Fact]
    public void GetById_ShouldReturnNullForInvalidId()
    {
        // Arrange
        var service = new StudentService();

        // Act
        var result = service.GetById(99);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void Add_ShouldAddNewStudent()
    {
        // Arrange
        var service = new StudentService();

        var student = new Student
        {
            Name = "Rahul",
            Email = "rahul@test.com",
            Course = ".NET"
        };

        // Act
        var result = service.Add(student);

        // Assert
        Assert.Equal("Rahul", result.Name);
        Assert.Equal(3, result.Id);
        Assert.Equal(3, service.GetAll().Count);
    }

    [Fact]
    public void Delete_ShouldRemoveStudent()
    {
        // Arrange
        var service = new StudentService();

        // Act
        var result = service.Delete(1);

        // Assert
        Assert.True(result);
        Assert.Null(service.GetById(1));
    }
}
```

Understand the AAA testing pattern

Every unit test follows three steps:

```
       UNIT TEST STRUCTURE
       ===================

       +------------------+
       |     ARRANGE      |
       |                  |
       | Create objects   |
       | Prepare data     |
       +--------+---------+
                |
                v
       +------------------+
       |       ACT        |
       |                  |
       | Execute method   |
       | Call function    |
       +--------+---------+
                |
                v
       +------------------+
       |      ASSERT      |
       |                  |
       | Verify result    |
       | Check expected   |
       +------------------+
```

Run unit tests:

```
dotnet test StudentAPI.Tests
```

Expected result:

```
Test Run Successful.

Total tests: 5
     Passed: 5
     Failed: 0
```

Unit testing completed

## 8. API Testing – Test HTTP Endpoints

Now we move from individual methods to actual HTTP requests.

We will use:

- `Microsoft.AspNetCore.Mvc.Testing`
- `WebApplicationFactory`
- `HttpClient`
- xUnit

Install the integration testing package:

```
dotnet add StudentAPI.Tests package Microsoft.AspNetCore.Mvc.Testing --version 10.0.0
```

This version assumes your application targets .NET 10. For another target framework, use the matching package major version.

### API testing architecture

```
           STUDENT API TEST
                  |
                  v
          +---------------+
          |  xUnit Test   |
          +-------+-------+
                  |
                  v
          +---------------+
          |  HttpClient   |
          +-------+-------+
                  |
                  | HTTP GET / POST
                  v
          +---------------+
          | Test Server   |
          |               |
          | WebApplication|
          | Factory       |
          +-------+-------+
                  |
                  v
          +---------------+
          | ASP.NET Core  |
          | Middleware    |
          +-------+-------+
                  |
                  v
          +---------------+
          | Controller    |
          +-------+-------+
                  |
                  v
          +---------------+
          | StudentService|
          +---------------+
```

Unlike unit tests, these tests exercise the ASP.NET Core routing, model binding, controller and HTTP response pipeline.

### Create StudentsApiTests.cs

File: `StudentAPI.Tests/StudentsApiTests.cs`

```
using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using StudentAPI.Models;
using Xunit;

namespace StudentAPI.Tests;

public class StudentsApiTests :
    IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public StudentsApiTests(
        WebApplicationFactory<Program> factory)
    {
        client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAll_ShouldReturnSuccess()
    {
        // Act
        var response = await client.GetAsync(
            "/api/students");

        // Assert
        Assert.Equal(
            HttpStatusCode.OK,
            response.StatusCode);
    }

    [Fact]
    public async Task GetById_ShouldReturnStudent()
    {
        // Act
        var response = await client.GetAsync(
            "/api/students/1");

        var student =
            await response.Content.ReadFromJsonAsync<Student>();

        // Assert
        Assert.Equal(HttpStatusCode.OK,
            response.StatusCode);

        Assert.Equal("Amit", student?.Name);
    }

    [Fact]
    public async Task GetInvalidStudent_ShouldReturn404()
    {
        // Act
        var response = await client.GetAsync(
            "/api/students/99");

        // Assert
        Assert.Equal(
            HttpStatusCode.NotFound,
            response.StatusCode);
    }

    [Fact]
    public async Task CreateStudent_ShouldReturn201()
    {
        // Arrange
        var student = new Student
        {
            Name = "Rahul",
            Email = "rahul@test.com",
            Course = ".NET"
        };

        // Act
        var response = await client.PostAsJsonAsync(
            "/api/students", student);

        // Assert
        Assert.Equal(
            HttpStatusCode.Created,
            response.StatusCode);

        Assert.NotNull(response.Headers.Location);
    }

    [Fact]
    public async Task CreateGetAndDeleteStudent()
    {
        // Arrange
        var student = new Student
        {
            Name = "TestStudent",
            Email = "teststudent@test.com",
            Course = "React"
        };

        // Create
        var createResponse = await client.PostAsJsonAsync(
            "/api/students", student);

        Assert.Equal(HttpStatusCode.Created,
            createResponse.StatusCode);

        var created = await createResponse.Content
            .ReadFromJsonAsync<Student>();

        Assert.NotNull(created);

        // Read
        var getResponse = await client.GetAsync(
            $"/api/students/{created.Id}");

        Assert.Equal(HttpStatusCode.OK,
            getResponse.StatusCode);

        // Delete
        var deleteResponse = await client.DeleteAsync(
            $"/api/students/{created.Id}");

        Assert.Equal(HttpStatusCode.NoContent,
            deleteResponse.StatusCode);
    }
}
```

Run API tests:

```
dotnet test StudentAPI.Tests
```

The tests execute the application through an in-memory test server. You do not need to manually start the Web API in another terminal.

## 9. Unit Testing vs API Testing

| Feature            | Unit Testing      | API Testing           |
| ------------------ | ----------------- | --------------------- |
| Testing target     | Individual method | HTTP endpoint         |
| Tool               | xUnit             | xUnit + HttpClient    |
| Application server | Not required      | Test server           |
| Controller         | Usually excluded  | Included              |
| Business logic     | Directly tested   | Tested through API    |
| HTTP status        | Not applicable    | Verified              |
| JSON response      | Not applicable    | Verified              |
| Database           | Usually isolated  | Can use test database |

## 10. Manual API Testing using Postman

Students should also learn to test APIs manually before automating them.

[How to Send JSON Object with POST Request](https://images.openai.com/static-rsc-4/L-1FPddTHa_mNjNXvApgWdQg23KOerbxq_8OzwZm2khXiGnlRAaDPZUC67kMiMs8zQ1xtzzqpm00Sb9qwUnd0zhFwq5ft1WzMX1NE2O1Uv6iZWNJ8jobsG9XXTTxZlC636QvF_tI92j-hFGvjhRA1kJIfreyK6zwl-i043_wpPc?purpose=inline)

[apidog.com](https://apidog.com/articles/send-json-object-with-post-request/?utm_source=chatgpt.com)

Example POST request:

URL

```
POST http://localhost:5000/api/students
```

Request Body – JSON

```
{
    "name": "Priya",
    "email": "priya@test.com",
    "course": ".NET"
}
```

Expected response:

```
{
    "id": 3,
    "name": "Priya",
    "email": "priya@test.com",
    "course": ".NET"
}
```

Expected status: `201 Created`.

## 11. Test Scenarios for Students

Ask students to implement these additional test cases.

| No. | Test scenario                     | Expected result                              |
| --- | --------------------------------- | -------------------------------------------- |
| 1   | Get all students                  | 200 OK                                       |
| 2   | Get student by valid ID           | 200 OK                                       |
| 3   | Get invalid student ID            | 404                                          |
| 4   | Create a student                  | 201 Created                                  |
| 5   | Delete existing student           | 204                                          |
| 6   | Delete invalid student            | 404                                          |
| 7   | Create student with empty name    | Validation error (400)                       |
| 8   | Create student with invalid email | Validation error (400)                       |
| 9   | Verify response JSON properties   | Correct properties                           |
| 10  | Verify duplicate email            | Conflict (409), if uniqueness is implemented |

Note: Scenarios 7–10 require additional validation and business rules in the application before they can pass.

## 12. Integrate Testing into CI/CD

```
             DEVELOPER
                 |
                 v
            GIT COMMIT
                 |
                 v
             GITHUB
                 |
                 v
          GITHUB ACTIONS
                 |
                 v
        +------------------+
        | Build ASP.NET API|
        +--------+---------+
                 |
                 v
        +------------------+
        | Unit Testing     |
        | StudentService   |
        +--------+---------+
                 |
                 v
        +------------------+
        | API Testing      |
        | HttpClient       |
        | WebApplication   |
        | Factory           |
        +--------+---------+
                 |
                 v
        +------------------+
        | Test Results     |
        +--------+---------+
                 |
           +-----+-----+
           |           |
           v           v
         PASS         FAIL
           |           |
           v           v
       Docker Build   Stop Build
           |
           v
       Cloud Deploy
```

## 13. Final Learning Assignment

Student Practical – Student Management API Testing

Suggested assignment for Transflower TAP students

Lab completion

# 0%

Create ASP.NET Core Web API project

Create Student model and StudentService

Implement GET, POST and DELETE endpoints

Write five xUnit unit tests

Write five API integration tests

Execute manual tests using Postman

Generate test execution reports

Integrate dotnet test into GitHub Actions

Mentor's final message

> Unit testing checks whether individual building blocks work correctly.
>
> API testing checks whether those building blocks work together through actual application endpoints.
>
> Automation testing makes these checks repeatable.
>
> CI/CD ensures that every code change is tested before reaching production.

One important next step: Replace the in-memory `StudentService` with a Repository + Entity Framework Core + MySQL implementation. Then introduce a separate test database for integration testing, so students learn how to test real database interactions without affecting production data.