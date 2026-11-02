# Playwright Browser Automation Testing using C# with xUnit / NUnit

You can integrate Microsoft Playwright for .NET directly into an existing C# testing project. You do not need Node.js or TypeScript for this approach.

If your existing automation framework already uses xUnit or NUnit, Playwright can be used alongside your existing test cases, assertions, fixtures, and test lifecycle.

We will use a practical example: automate a browser, open a login page, enter credentials, click Login, and validate the result.

## 1. Understand the architecture

```
       QA AUTOMATION ENGINEER
                 |
                 v
          C# TEST PROJECT
                 |
          +------+------+
          |             |
          v             v
        xUnit         NUnit
          |             |
          +------+------+
                 |
                 v
       Microsoft.Playwright
                 |
                 v
        Browser Automation
                 |
       +---------+---------+
       |         |         |
       v         v         v
   Chromium   Firefox    WebKit
       |         |         |
       +---------+---------+
                 |
                 v
          WEB APPLICATION
                 |
          +------+------+
          |             |
          v             v
       MVC UI       ASP.NET Core
                    Web API
                        |
                        v
                     MySQL
```

## 2. Understand the NuGet packages

| Package                   | Purpose                          |
| ------------------------- | -------------------------------- |
| Microsoft.Playwright      | Browser automation library       |
| xunit                     | Test framework                   |
| xunit.runner.visualstudio | Visual Studio test integration   |
| NUnit                     | Alternative test framework       |
| NUnit3TestAdapter         | NUnit test execution integration |
| Microsoft.NET.Test.Sdk    | .NET test execution              |

Important: Choose either xUnit or NUnit for your existing project. You do not need to install both.

## 3. Integrate Playwright into an existing xUnit project

Assume you already have an existing project:

```
Transflower.Automation/
│
├── Transflower.Automation.csproj
│
├── Tests/
│   ├── LoginTests.cs
│   ├── RegistrationTests.cs
│   └── DashboardTests.cs
│
└── TestData/
    └── users.json
```

### Step 1: Install Playwright NuGet package

Open the terminal in your existing project directory.

```
dotnet add package Microsoft.Playwright.Xunit
```

This provides Playwright integration with xUnit, including browser/page test fixtures.

### Step 2: Build the project

```
dotnet build
```

### Step 3: Install Chromium browser

For Windows PowerShell:

```
pwsh bin/Debug/net8.0/playwright.ps1 install chromium
```

Replace `net8.0` with your project's actual target framework directory, such as `net10.0`.

Playwright downloads the browser binary required by its installed version. You do not need to install Chrome separately.

### Step 4: Write your first browser automation test

Create `Tests/BrowserTests.cs`.

```
using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.Xunit;

namespace Transflower.Automation.Tests;

public class BrowserTests : PageTest
{
    [Fact]
    public async Task VerifyWebsiteTitle()
    {
        // Open website
        await Page.GotoAsync("https://example.com");

        // Validate page title
        await Expect(Page)
            .ToHaveTitleAsync(new Regex("Example Domain"));
    }
}
```

Understand the code:

| Code                 | Purpose                                  |
| -------------------- | ---------------------------------------- |
| `PageTest`           | Provides Playwright browser/page fixture |
| `[Fact]`             | Defines an xUnit test                    |
| `Page`               | Represents browser tab                   |
| `GotoAsync()`        | Navigates to website                     |
| `Expect()`           | Performs Playwright assertion            |
| `ToHaveTitleAsync()` | Validates browser page title             |

### Step 5: Execute the test

```
dotnet test
```

Your xUnit test runner will execute the Playwright browser test. To run with a visible browser:

```
dotnet test -- Playwright.LaunchOptions.Headless=false
```

## 4. Integrate Playwright into an existing NUnit project

Suppose you already have:

```
Transflower.NUnitTests/
│
├── Transflower.NUnitTests.csproj
│
├── Tests/
│   ├── LoginTests.cs
│   └── ProductTests.cs
│
└── TestData/
```

### Step 1: Install NUnit integration

```
dotnet add package Microsoft.Playwright.NUnit
```

### Step 2: Build and install Chromium

```
dotnet build

pwsh bin/Debug/net8.0/playwright.ps1 install chromium
```

### Step 3: Create a Playwright NUnit test

Create `Tests/BrowserTests.cs`.

```
using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace Transflower.NUnitTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class BrowserTests : PageTest
{
    [Test]
    public async Task VerifyWebsiteTitle()
    {
        await Page.GotoAsync("https://example.com");
        await Expect(Page).ToHaveTitleAsync(new Regex("Example Domain"));
    }
}
```

### Step 4: Execute NUnit tests

```
dotnet test
```

The NUnit test runner executes the browser automation test.

## 5. Practical Login Automation Test

Let's automate a login page developed using ASP.NET Core MVC.

Assume the login page contains:

```
<input id="username" />
<input id="password" />

<button id="loginBtn">Login</button>
<div id="message"></div>
```

### xUnit implementation

```
using Microsoft.Playwright;
using Microsoft.Playwright.Xunit;

namespace Transflower.Automation.Tests;

public class LoginTests : PageTest
{
    [Fact]
    public async Task ValidUserShouldLoginSuccessfully()
    {
        // Navigate to MVC login page
        await Page.GotoAsync("https://localhost:7001/Account/Login");

        // Enter credentials
        await Page.Locator("#username").FillAsync("admin");
        await Page.Locator("#password").FillAsync("admin123");

        // Click login
        await Page.Locator("#loginBtn").ClickAsync();

        // Verify successful login
        await Expect(Page).ToHaveURLAsync(new Regex(".*/Dashboard"));
    }
}
```

The same test logic can be used in NUnit by changing the test framework attributes and base class:

```
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

[TestFixture]
public class LoginTests : PageTest
{
    [Test]
    public async Task ValidUserShouldLoginSuccessfully()
    {
        await Page.GotoAsync("https://localhost:7001/Account/Login");
        await Page.Locator("#username").FillAsync("admin");
        await Page.Locator("#password").FillAsync("admin123");
        await Page.Locator("#loginBtn").ClickAsync();

        await Expect(Page).ToHaveURLAsync(new Regex(".*/Dashboard"));
    }
}
```

These examples assume that your application uses the specified element IDs and redirects successful logins to a Dashboard URL. Use test credentials, not production administrator credentials.

## 6. Common Playwright functions in C\#

| Playwright function   | Purpose               |
| --------------------- | --------------------- |
| `Page.GotoAsync()`    | Open website          |
| `Page.Locator()`      | Find HTML element     |
| `FillAsync()`         | Enter text            |
| `ClickAsync()`        | Click button          |
| `CheckAsync()`        | Select checkbox       |
| `SelectOptionAsync()` | Select dropdown value |
| `ScreenshotAsync()`   | Capture screenshot    |
| `ReloadAsync()`       | Reload browser        |
| `GoBackAsync()`       | Navigate backward     |
| `WaitForURLAsync()`   | Wait for URL change   |
| `Expect()`            | Perform assertions    |

## 7. Important: What if your existing test class already inherits another base class?

You do not have to inherit `PageTest`. You can use the core Playwright library and manage the browser lifecycle manually.

Install:

```
dotnet add package Microsoft.Playwright
```

Example:

```
using Microsoft.Playwright;
using Xunit;

public class CustomBrowserTests : IDisposable
{
    private readonly IPlaywright playwright;
    private readonly IBrowser browser;

    public CustomBrowserTests()
    {
        playwright = Playwright.CreateAsync().GetAwaiter().GetResult();

        browser = playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = true
            }).GetAwaiter().GetResult();
    }

    [Fact]
    public async Task OpenWebsite()
    {
        var page = await browser.NewPageAsync();

        await page.GotoAsync("https://example.com");
        Assert.Contains("Example Domain",await page.TitleAsync());
        await page.CloseAsync();
    }

    public void Dispose()
    {
        browser.Dispose();
        playwright.Dispose();
    }
}
```

This manual approach is useful when integrating Playwright into an existing custom automation framework. In production test suites, prefer asynchronous lifecycle management and proper isolation of browser contexts.

## 8. Recommended automation project architecture

```
Transflower.Automation
│
├── Tests
│   ├── LoginTests.cs
│   ├── RegistrationTests.cs
│   ├── DashboardTests.cs
│   └── ProductTests.cs
│
├── Pages
│   ├── LoginPage.cs
│   ├── RegistrationPage.cs
│   └── DashboardPage.cs
│
├── Fixtures
│   └── BrowserFixture.cs
│
├── TestData
│   └── TestUsers.json
│
├── Utilities
│   ├── ScreenshotHelper.cs
│   └── TestConfiguration.cs
│
└── Reports
```

This architecture follows the Page Object Model (POM), separating browser interactions from test scenarios.

## 9. Mentor's recommendation

For Transflower students learning SDET automation, I recommend this progression:

1. Existing xUnit/NUnit project – understand test lifecycle.
2. Playwright installation – browser automation setup.
3. Locators – identify HTML elements.
4. Browser actions – click, fill, select and navigate.
5. Assertions – validate expected behavior.
6. Page Object Model – organize automation code.
7. API testing – integrate REST API validation.
8. Database validation – verify persisted records.
9. CI/CD – execute tests automatically after Git commits.

One important distinction: xUnit and NUnit are test runners/frameworks. Playwright is the browser automation engine. They complement each other rather than replace each other.

For a practical classroom lab, use your existing ASP.NET Core MVC + Web API + MySQL login application and build the complete automation suite using C# + xUnit or NUnit.