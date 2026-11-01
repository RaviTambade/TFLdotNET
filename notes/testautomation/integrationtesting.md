# ASP.NET Core MVC + Web API + Playwright + TypeScript

Let's restructure the previous Node.js login automation lab using the Microsoft .NET ecosystem.

We will retain Playwright + TypeScript for automation, but replace the Node.js + Express backend with ASP.NET Core Web API and use ASP.NET Core MVC for the login user interface.

This is a practical enterprise-style testing architecture for Transflower students.

## 1. Understand the  architecture ASP.NET Core + Playwright + TypeScript

 ```
                 TEST AUTOMATION LAYER
          +--------------------------------+
          |                                |
          |     Playwright + TypeScript    |
          |                                |
          |      Automation Framework      |
          +---------------+----------------+
                          |
                 +--------+--------+
                 |                 |
                 v                 v
        +----------------+  +----------------+
        |  UI TESTING    |  |  API TESTING   |
        +----------------+  +----------------+
                 |                 |
                 v                 v
        +----------------+  +----------------+
        |    Chromium    |  | HTTP Requests  |
        |    Browser     |  |                |
        +----------------+  +----------------+
                 |                 |
                 v                 v
        +----------------+  +----------------+
        |   MVC LOGIN    |  |   ASP.NET CORE |
        |     PAGE       |  |    WEB API     |
        |                |  |                |
        | Razor Views    |  | API Endpoints  |
        | HTML + CSS     |  | JSON Requests  |
        +--------+-------+  +--------+-------+
                 |                 |
                 +--------+--------+
                          |
                          v
          +--------------------------------+
          |       ASP.NET CORE APP         |
          |                                |
          |  +--------------------------+  |
          |  |     MVC Controllers     |   |
          |  +--------------------------+  |
          |  |      API Controllers    |   |
          |  +--------------------------+  |
          |  |    Business Services    |   |
          |  +--------------------------+  |
          |  |   Authentication Logic  |   |
          |  +--------------------------+  |
          +---------------+----------------+
                          |
                          v
          +--------------------------------+
          |          DATA LAYER            |
          |                                |
          |       Entity Framework Core    |
          |                                |
          |       Repository / Queries     |
          +---------------+----------------+
                          |
                 +--------+--------+
                 |                 |
                 v                 v
          +------------+    +------------+
          |   MySQL    |    | SQL Server |
          +------------+    +------------+
                 |                 |
                 +--------+--------+
                          |
                          v
             USER DATA AND AUTH RECORDS
```

### Understand the two testing paths

1. UI Testing – Browser-based

UI Automation

```
Playwright + TypeScript
          |
          v
     Chromium
          |
          v
     MVC Login Page
          |
          v
     Username / Password
          |
          v
     Login Button
          |
          v
     Web API Request
          |
          v
     Verify Dashboard
```

2. API Testing – HTTP-based

API Automation

```
Playwright + TypeScript
          |
          v
     APIRequestContext
          |
          v
     HTTP POST Request
          |
          v
     ASP.NET Core Web API
          |
          v
     Business Services
          |
          v
     Database
          |
          v
     HTTP Response
          |
          v
     Assert Status + JSON
```

### Responsibilities of each layer

| Layer              | Responsibility       |
| ------------------ | -------------------- |
| Playwright         | Automation execution |
| TypeScript         | Test scripting       |
| Chromium           | Browser rendering    |
| MVC                | User interface       |
| Web API            | HTTP endpoints       |
| Business Services  | Business rules       |
| EF Core            | Database access      |
| MySQL / SQL Server | Persistent storage   |


UI Testing validates the application from the user's perspective.

API Testing validates the application from the service consumer's perspective.

Both approaches together provide better confidence in the quality of an ASP.NET Core application.


## 2. Technology stack

| Layer         | Technology               |
| ------------- | ------------------------ |
| Frontend      | ASP.NET Core MVC + Razor |
| Backend       | ASP.NET Core Web API     |
| Language      | C#                       |
| Runtime       | .NET                     |
| Database      | MySQL or SQL Server      |
| Automation    | Playwright               |
| Test language | TypeScript               |
| Browser       | Chromium                 |
| Reporting     | Playwright HTML Reporter |

## 3. Create ASP.NET Core solution

Open Windows Terminal or Developer PowerShell.

Check your .NET SDK:

```
dotnet --version
```

Create a solution:

```
mkdir TransflowerLoginLab
cd TransflowerLoginLab

dotnet new sln -n TransflowerLoginLab
```

Create the MVC project:

```
dotnet new mvc -n LoginWeb
```

Create the Web API project:

```
dotnet new webapi -n LoginAPI
```

Add both projects to the solution:

```
dotnet sln add LoginWeb/LoginWeb.csproj

dotnet sln add LoginAPI/LoginAPI.csproj
```

Your solution structure:

```
TransflowerLoginLab/
│
├── TransflowerLoginLab.sln
│
├── LoginWeb/
│   ├── Controllers/
│   ├── Views/
│   ├── Models/
│   ├── wwwroot/
│   └── Program.cs
│
├── LoginAPI/
│   ├── Controllers/
│   ├── Models/
│   ├── Services/
│   └── Program.cs
│
└── Automation/
```

## 4. Develop ASP.NET Core Web API

First, build the backend login API.

Create `LoginAPI/Models/LoginRequest.cs`.

```

namespace LoginAPI.Models;

public class LoginRequest
{
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
}

```

Create `LoginAPI/Controllers/AuthController.cs`.

```

using Microsoft.AspNetCore.Mvc;
using LoginAPI.Models;

namespace LoginAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Username) ||
            string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest(new
            {
                success = false,
                message = "Username and password are required"
            });
        }

        // Demo credentials
        if (request.Username == "student" &&
            request.Password == "Pass123")
        {
            return Ok(new
            {
                success = true,
                message = "Login successful",
                username = request.Username
            });
        }

        return Unauthorized(new
        {
            success = false,
            message = "Invalid username or password"
        });
    }
}

```

Open `LoginAPI/Program.cs` and ensure controller services and routing are configured:

```

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

```

Run API:

```
cd LoginAPI

dotnet run --urls http://localhost:5001
```

Your endpoint:

```
POST http://localhost:5001/api/auth/login
```

Sample JSON request:

```
{
  "username": "student",
  "password": "Pass123"
}
```

Expected response:

```
{
  "success": true,
  "message": "Login successful",
  "username": "student"
}
```

Test this endpoint using Swagger or Postman before moving to the MVC application.

## 5. Develop ASP.NET Core MVC Login UI

The MVC application will display the login form and communicate with the Web API.

Create `LoginWeb/Models/LoginViewModel.cs`.

```

using System.ComponentModel.DataAnnotations;

namespace LoginWeb.Models;

public class LoginViewModel
{
    [Required]
    public string Username { get; set; } = "";

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = "";
}

```

Create `LoginWeb/Controllers/AccountController.cs`.

```

using Microsoft.AspNetCore.Mvc;
using LoginWeb.Models;

namespace LoginWeb.Controllers;

public class AccountController : Controller
{
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Dashboard()
    {
        return View();
    }
}

```

Create the Razor view:

`LoginWeb/Views/Account/Login.cshtml`

```

@model LoginWeb.Models.LoginViewModel

<!DOCTYPE html>
<html>
<head>
    <title>Transflower Login</title>
    <style>
        body {
            font-family: Arial;
            background: #eeeeee;
        }

        .login-container {
            width: 350px;
            margin: 100px auto;
            padding: 25px;
            background: white;
            border-radius: 10px;
        }

        input, button {
            width: 100%;
            padding: 12px;
            margin: 10px 0;
            box-sizing: border-box;
        }

        button {
            background: darkred;
            color: white;
            border: none;
            cursor: pointer;
        }

        #message {
            color: red;
        }
    </style>
</head>

<body>

<div class="login-container">

    <h2>Transflower Login</h2>

    <form id="loginForm">
        <input asp-for="Username" id="username" placeholder="Username" />
        <input asp-for="Password" id="password" placeholder="Password" />
        <button type="submit" id="loginButton">Login</button>
    </form>

    <p id="message"></p>
</div>

<script>
    document.getElementById("loginForm")
        .addEventListener("submit", async function(event) {

        event.preventDefault();

        const username =document.getElementById("username").value;

        const password =document.getElementById("password").value;

        const response = await fetch("http://localhost:5001/api/auth/login",
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    username,
                    password
                })
            }
        );

        const result = await response.json();

        if (response.ok) 
        {window.location.href = "/Account/Dashboard";}
        else 
        {document.getElementById("message").textContent =result.message;}
    });
</script>

</body>
</html>

```

Create `LoginWeb/Views/Account/Dashboard.cshtml`.

```

<!DOCTYPE html>
<html>
<head>
    <title>Student Dashboard</title>
</head>
<body>
    <h1>Welcome to Transflower Dashboard</h1>
    <p>Login Successful!</p>
</body>
</html>

```

Configure MVC in `LoginWeb/Program.cs`:

```
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute(name: "default",pattern: "{controller=Account}/{action=Login}/{id?}");
app.Run();
```

### Important: Enable cross-origin requests

Because MVC and API are running on different ports, the browser treats them as different origins.

In `LoginAPI/Program.cs`, before `builder.Build()`, add:

```
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLoginWeb", policy =>
    {
        policy.WithOrigins("http://localhost:5002")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
```

After building the application, before mapping controllers:

```
app.UseCors("AllowLoginWeb");
```

Run the MVC application in another terminal:

```
cd LoginWeb

dotnet run --urls http://localhost:5002
```

Open:

`http://localhost:5002/Account/Login`

Demo credentials:

```
Username: student
Password: Pass123
```

Authentication limitation

The dashboard route currently has no authorization protection. Also, the API's CORS policy only permits the MVC origin; it does not authenticate callers. Production systems should use ASP.NET Core authentication, secure cookies or tokens, and `[Authorize]` protection.


## 6. Create Playwright + TypeScript automation project

Keep automation independent of the ASP.NET Core projects.

Open another terminal at the solution root:

```
mkdir Automation
cd Automation

npm init -y

npm install -D typescript
npm install -D @types/node
npm install -D @playwright/test

npx tsc --init

npx playwright install chromium
```

Create this structure:

```
Automation/
│
├── tests/
│   ├── login-ui.spec.ts
│   └── login-api.spec.ts
│
├── playwright.config.ts
├── package.json
└── tsconfig.json
```

## 7. Configure Playwright

Create `Automation/playwright.config.ts`.

```

import { defineConfig, devices } from "@playwright/test";

export default defineConfig({

    testDir: "./tests",
    fullyParallel: true,
    reporter: "html",
    use: {
        baseURL: "http://localhost:5002",
        trace: "on-first-retry",
        screenshot: "only-on-failure"
    },

    projects: [
        {
            name: "chromium",
            use: { ...devices["Desktop Chrome"]}
        }
    ],

    webServer: [
        {
            command: "dotnet run --urls http://localhost:5001",
            url: "http://localhost:5001/swagger/index.html",
            reuseExistingServer: !process.env.CI,
            timeout: 60000
        },
        {
            command: "dotnet run --urls http://localhost:5002",
            url: "http://localhost:5002/Account/Login",
            reuseExistingServer: !process.env.CI,
            timeout: 60000
        }
    ]
});

```

The `webServer` configuration starts both ASP.NET Core applications automatically when tests execute.

## 8. UI testing using Playwright + TypeScript

Create `Automation/tests/login-ui.spec.ts`.

```

import { test, expect } from "@playwright/test";

test.describe("ASP.NET Core MVC Login UI", () => {

    test("Verify login page title", async ({ page }) => {
        await page.goto("/Account/Login");
        await expect(page).toHaveTitle("Transflower Login");

    });

    test("Verify login form elements", async ({ page }) => {

        await page.goto("/Account/Login");
        await expect(page.locator("#username")).toBeVisible();
        await expect(page.locator("#password")).toBeVisible();

        await expect(page.locator("#loginButton")).toBeVisible();
    });

    test("Login with valid credentials", async ({ page }) => {

        await page.goto("/Account/Login");
        await page.locator("#username").fill("student");
        await page.locator("#password").fill("Pass123");
        await page.locator("#loginButton").click();
        await expect(page).toHaveURL(/Dashboard/);
        await expect( page.locator("h1")).toHaveText("Welcome to Transflower Dashboard");

    });

    test("Login with invalid credentials", async ({ page }) => {
        await page.goto("/Account/Login");
        await page.locator("#username").fill("student");
        await page.locator("#password").fill("wrong123");
        await page.locator("#loginButton").click();
        await expect(page.locator("#message")).toHaveText("Invalid username or password");
    });

});

```

## 9. API testing using Playwright + TypeScript

Create `Automation/tests/login-api.spec.ts`.

Notice that API testing does not open Chromium. Playwright directly sends HTTP requests to ASP.NET Core Web API.

```

import { test, expect } from "@playwright/test";

const API_URL = "http://localhost:5001";

test.describe("ASP.NET Core Login API", () => {

    test("Valid login returns HTTP 200", async ({ request }) => {

        const response = await request.post(
            `${API_URL}/api/auth/login`,
            {
                data: {
                    username: "student",
                    password: "Pass123"
                }
            }
        );

        expect(response.status()).toBe(200);
        const result = await response.json();
        expect(result.success).toBe(true);
        expect(result.message).toBe("Login successful");

    });

    test("Invalid login returns HTTP 401", async ({ request }) => {

        const response = await request.post(
            `${API_URL}/api/auth/login`,
            {
                data: {
                    username: "student",
                    password: "wrong123"
                }
            }
        );

        expect(response.status()).toBe(401);
        const result = await response.json();
        expect(result.success).toBe(false);
        expect(result.message).toBe("Invalid username or password" );
    });

    test("Missing credentials returns HTTP 400", async ({ request }) => {

        const response = await request.post(
            `${API_URL}/api/auth/login`,
            {
                data: {
                    username: "",
                    password: ""
                }
            }
        );
        expect(response.status()).toBe(400);
    });

});

```

## 10. Execute UI and API tests

From the `Automation` directory:

Run all tests:

```
npx playwright test
```

Run UI tests only:

```
npx playwright test tests/login-ui.spec.ts
```

Run API tests only:

```
npx playwright test tests/login-api.spec.ts
```

Run Chromium in headed mode:

```
npx playwright test --headed
```

Run Playwright HTML report:

```
npx playwright show-report
```

One configuration detail: use a health endpoint instead of the Swagger URL for reliable API startup detection. Add this to `LoginAPI/Program.cs` before `app.Run()`:

```
app.MapGet("/health", () => Results.Ok(new {  status = "Healthy"}));
```

Then update the API `webServer.url` in Playwright configuration to:

```
url: "http://localhost:5001/health",
```

## 11. Understand the complete execution flow

```
              DEVELOPER
                  |
                  v
           GIT REPOSITORY
                  |
                  v
          ASP.NET CORE SOLUTION
                  |
          +-------+-------+
          |               |
          v               v
      LoginWeb         LoginAPI
       MVC             Web API
          |               |
          v               |
      Razor View          |
          |               |
          v               |
      Login Form          |
          |               |
          +-------+-------+
                  |
                  v
          Authentication API
                  |
                  v
             Database
                  |
                  v
          HTTP Response
                  |
       +----------+----------+
       |                     |
       v                     v
   UI AUTOMATION         API AUTOMATION
   Playwright             Playwright
   TypeScript             TypeScript
       |                     |
       v                     v
   Chromium              HTTP Request
       |                     |
       +----------+----------+
                  |
                  v
           TEST ASSERTIONS
                  |
                  v
             TEST REPORT
                  |
                  v
              CI/CD
```

## 12. Compare the previous and new architecture

| Layer         | Previous lab            | New lab              |
| ------------- | ----------------------- | -------------------- |
| UI            | HTML                    | ASP.NET Core MVC     |
| Backend       | Node.js                 | ASP.NET Core Web API |
| Language      | JavaScript / TypeScript | C#                   |
| API           | Express                 | API Controller       |
| Runtime       | Node.js                 | .NET                 |
| UI automation | Playwright              | Playwright           |
| Test language | TypeScript              | TypeScript           |
| Browser       | Chromium                | Chromium             |

## 13. Suggested progression for Transflower SDET students

## SDET practical learning roadmap

1. Lab 1 – Manual login testing

   Understand UI, form validation and login requirements.
2. Lab 2 – ASP.NET Core Web API

   Build authentication endpoint and test using Postman.
3. Lab 3 – MVC integration

   Connect Razor login page to Web API.
4. Lab 4 – Playwright UI automation

   Browser navigation, locators, actions and assertions.
5. Lab 5 – Playwright API automation

   HTTP methods, JSON, status codes and response validation.
6. Lab 6 – Page Object Model

   Build reusable LoginPage and DashboardPage classes.
7. Lab 7 – Database testing

   Integrate MySQL or SQL Server and validate persisted user data.
8. Lab 8 – CI/CD

   GitHub Actions, automated execution, reports and deployment gates.

Mentor's key takeaway: The application is developed using C# and ASP.NET Core, while the independent automation framework is developed using TypeScript and Playwright. This is a valuable real-world skill combination for an SDET working in a .NET enterprise environment.