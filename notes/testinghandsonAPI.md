# Hands-on Lab: API Testing Existing ASP.NET Core Web API Using .NET CLI



Practical SDET Lab

No Postman required



Objective: Learn how to test an already-developed ASP.NET Core Web API using only the .NET CLI, C#, xUnit and `HttpClient`.

We will not create a new Web API. We will use your existing application and create a separate automated API testing project.

## 1. Understand the architecture

```
       EXISTING ASP.NET CORE WEB API
       =============================

       +---------------------------+
       | Existing Web API Project  |
       |                           |
       | Controllers               |
       | Services                  |
       | Models                    |
       | Repository                |
       | Database                  |
       +-------------+-------------+
                     |
                     | HTTP Request
                     |
       +-------------v-------------+
       |       API TEST PROJECT    |
       |                           |
       | xUnit                     |
       | HttpClient                |
       | Assertions                |
       | Test Cases                |
       +-------------+-------------+
                     |
                     v
             Test Execution
                     |
             +-------+-------+
             |               |
             v               v
           PASS            FAIL
             |               |
             v               v
         Test Report    Error Details
```

## 2. Identify your existing Web API project

Open the existing solution in VS Code or Visual Studio.

For example:

```
TFLAssessment/
|
+-- TFLAssessment.API/
|     |
|     +-- Controllers/
|     +-- Models/
|     +-- Services/
|     +-- Program.cs
|     +-- TFLAssessment.API.csproj
|
+-- TFLAssessment.sln
```

Open terminal in the solution directory.

Check installed .NET SDK:

```
dotnet --version
dotnet --list-sdks
```

Check your API project:

```
dotnet build TFLAssessment.API
```

If the build succeeds, your existing API is ready for testing.

## 3. Start the existing Web API

```
dotnet run --project TFLAssessment.API
```

You may see output similar to:

```
Now listening on: https://localhost:7001
Now listening on: http://localhost:5001

Application started.
```

Use the actual URL and port displayed in your terminal.

Check the Swagger interface if enabled:

```
https://localhost:7001/swagger
```

For this lab, assume the API base URL is:

```
https://localhost:7001
```

## 4. Create a separate API testing project

Open a second terminal.

From the solution directory:

```
dotnet new xunit -n TFLAssessment.ApiTests

dotnet sln add TFLAssessment.ApiTests/TFLAssessment.ApiTests.csproj
```

Your solution now looks like:

```
TFLAssessment/
|
+-- TFLAssessment.API/
|     +-- Controllers/
|     +-- Models/
|     +-- Program.cs
|
+-- TFLAssessment.ApiTests/
      +-- UnitTest1.cs
      +-- TFLAssessment.ApiTests.csproj
```

Notice that the API testing project is separate from the existing API project.

## 5. Create your first API test

Rename `UnitTest1.cs` to `StudentsApiTests.cs`.

Write the following code:

File: `TFLAssessment.ApiTests/StudentsApiTests.cs`

```
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace TFLAssessment.ApiTests;

public class StudentsApiTests
{
    private readonly HttpClient client;

    public StudentsApiTests()
    {
        var baseUrl =
            Environment.GetEnvironmentVariable("API_BASE_URL")
            ?? "http://localhost:5001";

        client = new HttpClient
        {
            BaseAddress = new Uri(baseUrl)
        };
    }

    [Fact]
    public async Task GetStudents_ShouldReturn200()
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
    public async Task GetInvalidStudent_ShouldReturn404()
    {
        // Act
        var response = await client.GetAsync(
            "/api/students/99999");

        // Assert
        Assert.Equal(
            HttpStatusCode.NotFound,
            response.StatusCode);
    }

    [Fact]
    public async Task CreateStudent_ShouldReturn201()
    {
        // Arrange
        var student = new
        {
            name = "Test Student",
            email = $"student-{Guid.NewGuid():N}@test.com",
            course = ".NET"
        };

        // Act
        var response = await client.PostAsJsonAsync(
            "/api/students", student);

        // Assert
        Assert.Equal(
            HttpStatusCode.Created,
            response.StatusCode);
    }

    [Fact]
    public async Task DeleteInvalidStudent_ShouldReturn404()
    {
        // Act
        var response = await client.DeleteAsync(
            "/api/students/99999");

        // Assert
        Assert.Equal(
            HttpStatusCode.NotFound,
            response.StatusCode);
    }
}
```

Important: These are example endpoints based on our Student API lab. If your existing API has different routes, replace `/api/students` with the actual routes from your controllers or Swagger.

## 6. Execute API tests using .NET CLI

Keep your existing Web API running in the first terminal.

Open the second terminal.

```
# Restore dependencies
dotnet restore

# Build testing project
dotnet build TFLAssessment.ApiTests

# Execute all API tests
dotnet test TFLAssessment.ApiTests
```

Expected output:

```
Test Run Successful.

Passed!  - Failed: 0
           Passed: 4
           Skipped: 0
           Total: 4
```

API tests executed

## 7. Important .NET CLI testing commands

| Command                                                 | Purpose                                            |
| ------------------------------------------------------- | -------------------------------------------------- |
| `dotnet test`                                           | Execute all tests                                  |
| `dotnet test --list-tests`                              | List available tests                               |
| `dotnet test --filter "FullyQualifiedName~GetStudents"` | Run selected test                                  |
| `dotnet test --logger "console;verbosity=detailed"`     | Detailed execution output                          |
| `dotnet test --no-build`                                | Run without rebuilding                             |
| `dotnet test --collect:"XPlat Code Coverage"`           | Collect code coverage (requires collector package) |
| `dotnet test -v normal`                                 | Normal verbosity                                   |

Example:

```
dotnet test TFLAssessment.ApiTests \
  --filter "FullyQualifiedName~CreateStudent"
```

On Windows CMD, you can also execute the command on one line.

## 8. API test scenarios for existing Web APIs

Suppose your existing TFLAssessment application exposes these endpoints:

| HTTP   | API endpoint       | Test scenario           |
| ------ | ------------------ | ----------------------- |
| GET    | `/api/questions`   | Retrieve all questions  |
| GET    | `/api/questions/1` | Retrieve question by ID |
| POST   | `/api/questions`   | Create question         |
| PUT    | `/api/questions/1` | Update question         |
| DELETE | `/api/questions/1` | Delete question         |
| GET    | `/api/students`    | Retrieve students       |
| GET    | `/api/assessments` | Retrieve assessments    |

Students should verify the following:

- Correct HTTP status codes.
- Correct response JSON.
- Required fields are present.
- Invalid IDs are handled correctly.
- Invalid request data is rejected.
- Authentication and authorization work correctly.
- Database changes are correctly persisted.

## 9. Configure API URL without changing test code

Instead of hardcoding the URL, pass it through an environment variable.

Windows PowerShell:

```
$env:API_BASE_URL="http://localhost:5001"

dotnet test TFLAssessment.ApiTests
```

Windows CMD:

```
set API_BASE_URL=http://localhost:5001

dotnet test TFLAssessment.ApiTests
```

For HTTPS local development, trust the ASP.NET Core development certificate:

```
dotnet dev-certs https --trust
```

Then configure the corresponding HTTPS URL.

## 10. Test an existing API with real database connectivity

The previous example tests your running API. If the existing API uses MySQL, the HTTP request can travel through the complete application:

```
       xUnit API Test
             |
             v
         HttpClient
             |
             | HTTP POST
             v
       ASP.NET Core API
             |
             v
         Controller
             |
             v
       Business Service
             |
             v
         Repository
             |
             v
         MySQL DB
             |
             v
       HTTP Response
             |
             v
        xUnit Assert
```

For these tests:

1. Start the API.
2. Make sure the test database is available.
3. Execute the HTTP requests.
4. Verify the response.
5. Verify database persistence where required.
6. Clean up test records.

Use a separate test database, not production data.

## 11. Challenge: Students should write additional API tests

SDET Practical Challenge – TFLAssessment API

Challenge progress

0/10

Test GET /api/questions returns 200

Test GET /api/questions/999 returns 404

Test POST /api/questions with valid JSON

Test POST /api/questions with missing description

Test PUT /api/questions/1 updates question

Test DELETE /api/questions/1 deletes question

Test unauthorized request returns 401

Test user without permission receives 403

Verify created question exists in MySQL

Verify deleted question no longer exists

## 12. Final testing workflow using CLI

```
        EXISTING WEB API
               |
               v
        dotnet run
               |
               v
        API RUNNING
               |
               v
       SECOND TERMINAL
               |
               v
       dotnet test
               |
               v
       xUnit Test Runner
               |
        +------+------+
        |             |
        v             v
     API Tests    Assertions
        |             |
        +------+------+
               |
               v
          Test Results
               |
               v
          CI/CD Pipeline
```

My recommendation for your TAP students: Teach them this sequence:

1. Manual API testing using Swagger.
2. Manual API testing using Postman.
3. Automated API testing using C# and HttpClient.
4. Integration testing using WebApplicationFactory.
5. Database integration testing using MySQL.
6. Execute all tests through GitHub Actions.

The important distinction: our current lab tests an already-running API over HTTP. `WebApplicationFactory` is the next step when students want to launch the application inside the test process without manually starting the server.