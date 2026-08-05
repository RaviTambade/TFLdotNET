# 🎓 Building a Professional .NET 10 Solution Using the CLI

## * Think Like a Software Engineer, Build Like an Architect*

> **"Good morning, future software engineers! Today, I don't want you to think like someone using an IDE. I want you to think like an engineer working on a production server, a Linux machine, or inside a Docker container. There may be no Visual Studio, no drag-and-drop, and no mouse. There is only your terminal, your knowledge, and your ability to build software from first principles. That is why every .NET developer should master the .NET CLI."**



# 🌍 Why Learn the .NET CLI?

Many beginners ask me,

> **"Sir, why should I learn CLI when Visual Studio can create everything with a few clicks?"**

My answer is always the same.

> **"Because professionals don't depend on tools. Tools depend on professionals."**

Visual Studio is an excellent productivity tool.

But the **.NET CLI** is the foundation behind it.

Whether you're using:

* Visual Studio
* Visual Studio Code
* JetBrains Rider
* Linux
* macOS
* Windows
* Docker
* GitHub Actions
* Azure DevOps

they all rely on the **.NET CLI**.

# 🏗 Think Like a Construction Engineer

Imagine you're constructing a modern apartment. Would you start by painting the walls? Of course not.
First, you create:

* Land
* Blueprint
* Foundation
* Rooms
* Electrical Wiring
* Plumbing
* Furniture

Software architecture follows exactly the same principle.

```text
        Apartment Construction

              Land
                │
                ▼
           Blueprint
                │
                ▼
          Foundation
                │
                ▼
             Rooms
                │
                ▼
         Electrical Wiring
                │
                ▼
          Interior Design
```

Similarly...

```text
         .NET Solution

        Solution (.sln)
              │
              ▼
      Console/Web Project
              │
              ▼
       Business Library
              │
              ▼
         Data Library
              │
              ▼
      Infrastructure Layer
```

Professional software is built layer by layer.


# 🛠 Step 1 – Check Your Tools

Before a carpenter starts work, he checks his tools. Before a software engineer starts, he checks the SDK.

```bash
dotnet --version
```

Expected output

```text
10.0.xxx
```

```text
Terminal
     │
dotnet --version
     │
     ▼
.NET SDK Installed
     │
Ready to Build
```

Never assume. Always verify your environment.

 # 📂 Step 2 – Create Your Workshop

```bash
mkdir MySolution
cd MySolution
```

Think of this folder as your engineering workshop. Everything you build belongs here.

```text
MySolution/

Empty Workspace
↓
Ready for Construction
```

A clean workspace leads to a clean architecture.


# 🗺 Step 3 – Create the Solution

```bash
dotnet new sln -n MySolution
```

This creates

```text
MySolution.sln
```

Many students think the solution contains code. It doesn't. It contains **organization**. Think of it as the architect's blueprint.

```text
             MySolution.sln

          Project Manager
        ┌────────┬─────────┐
        │        │         │
        ▼        ▼         ▼

     MyApp   MyLibrary   Future Projects
```

The solution knows where every project lives.


# 🚪 Step 4 – Create the Entry Point

```bash
dotnet new console -n MyApp
```

Every application needs an entry point.

```text
User
↓
Program.cs
↓
Application Starts
```

In .NET 10, we use **Top-Level Statements**. No need to write

```csharp
static void Main()
```

The compiler generates it automatically. Cleaner code. Less ceremony.


# 📚 Step 5 – Create a Class Library

```bash
dotnet new classlib -n MyLibrary
```

This is where professionals separate business logic. Instead of writing everything inside Program.cs,we organize our code.

```text
MyLibrary
↓
Business Rules
↓
Reusable Components
↓
Shared Logic
```

This library can later be used by:

* Console App
* Web API
* MVC Application
* Blazor
* Unit Tests

One library.Many applications.

  

# 🧩 Step 6 – Add Projects to the Solution

```bash
dotnet sln add MyApp/MyApp.csproj
dotnet sln add MyLibrary/MyLibrary.csproj
```

Now the blueprint knows every building.

```text
MySolution.sln
      │
      ├────────► MyApp
      │
      └────────► MyLibrary
```

Without this step, the projects exist... but the solution doesn't know about them.

# 🔗 Step 7 – Create Project References

```bash
dotnet add MyApp/MyApp.csproj reference MyLibrary/MyLibrary.csproj
```

This is one of the most important architectural steps. Think of it as connecting departments.

```text
Console App
      │
Uses
      ▼
Business Library
```

Without this reference, MyApp cannot see MyLibrary.

# Dependency Relationship

```text
          MySolution

      +----------------+
      |    MyApp       |
      +----------------+
              │
              │ Reference
              ▼
      +----------------+
      |  MyLibrary     |
      +----------------+
```

Notice something. The library knows nothing about the application. This is good architecture.


# 🏗 Step 8 – Add Business Logic

Inside MyLibrary

```csharp
namespace MyLibrary;

public class Greeter
{
    public static string Hello(string name)
        => $"Hello, {name} from .NET 10!";
}
```

Business logic belongs here.

Inside Program.cs

```csharp
using MyLibrary;

var name = args.Length > 0
    ? args[0]
    : "World";

Console.WriteLine(Greeter.Hello(name));
```

Notice Program.cs doesn't know

**how**

the greeting is created. It simply uses it. That is Separation of Concerns.



# Application Architecture

```text
Program.cs
      │
Calls
      ▼
Greeter
      │
Creates Message
      ▼
Console
```

Each component has one responsibility.

# 🔨 Step 9 – Build the Solution

```bash
dotnet build
```

Many beginners immediately run. Professionals build first. Why? Because Build answers an important question.

> **"Can every project compile successfully?"**

```text
Source Code
      │
Build
      │
Compiler
      │
Errors?
      │
No
      ▼
Executable
```

Never skip the build step.

# ▶ Step 10 – Run the Application

```bash
dotnet run --project MyApp/MyApp.csproj
```

Or

```bash
dotnet run --project MyApp/MyApp.csproj -- Ravi
```

Output

```text
Hello, Ravi from .NET 10!
```

Congratulations.You have built a professional multi-project solution.


# Complete Folder Structure

```text
MySolution
│
├── MySolution.sln
│
├── MyApp
│   ├── Program.cs
│   ├── MyApp.csproj
│   └── bin
│
└── MyLibrary
    ├── Greeter.cs
    ├── MyLibrary.csproj
    └── bin
```

Everything is organized. Everything has a purpose.


# CLI Workflow

```text
Create Folder
      │
      ▼
Create Solution
      │
      ▼
Create Projects
      │
      ▼
Add Projects
      │
      ▼
Add References
      │
      ▼
Write Code
      │
      ▼
Build
      │
      ▼
Run
```

This is the workflow followed by professional developers.


# Why Professionals Love the CLI

The same commands work on

```text
Windows
Linux
macOS
Docker
GitHub Actions
Azure DevOps
AWS CodeBuild
Google Cloud Build
```
 
Learn once. Build everywhere.


# Growing into Enterprise Architecture

Today's solution contains

```text
MyApp
↓
MyLibrary
```

Tomorrow it can grow into

```text
                   MySolution
                         │

     ┌─────────────┬──────────────┬──────────────┐
     ▼             ▼              ▼
 WebAPI      Business Layer   Data Layer
     │             │              │
     └─────────────┼──────────────┘
                   ▼
            Shared Library
                   │
                   ▼
             Infrastructure
                   │
                   ▼
      SQL Server / MySQL / MongoDB
                   │
                   ▼
 RabbitMQ • Redis • Docker • Kubernetes
```

The CLI scales with your architecture.

# Useful CLI Commands Every .NET Developer Should Know

| Command                | Purpose                   |
| ---------------------- | ------------------------- |
| `dotnet --version`     | Check SDK version         |
| `dotnet new`           | Create new project        |
| `dotnet new sln`       | Create solution           |
| `dotnet sln add`       | Add project to solution   |
| `dotnet add reference` | Create project reference  |
| `dotnet add package`   | Install NuGet package     |
| `dotnet restore`       | Restore dependencies      |
| `dotnet build`         | Compile solution          |
| `dotnet test`          | Execute unit tests        |
| `dotnet run`           | Run application           |
| `dotnet publish`       | Create deployment package |
| `dotnet clean`         | Remove build artifacts    |

# Common Mistakes

| Problem           | Reason             | Solution                                     |
| ----------------- | ------------------ | -------------------------------------------- |
| Project not found | Wrong folder       | Use `--project` or navigate into the project |
| Namespace error   | Missing reference  | `dotnet add reference`                       |
| Package missing   | NuGet not restored | `dotnet restore`                             |
| Build failed      | Compilation errors | Read the build output carefully              |
| Wrong SDK         | Old .NET version   | Install the latest .NET SDK                  |

# 🎯 Mentor's Architecture Perspective

As a beginner, the CLI looks like a collection of commands.

```bash
dotnet new
dotnet build
dotnet run
```

As an experienced software engineer, the CLI becomes:

```text
Project Generator
Solution Manager
Build System
Dependency Manager
Package Manager
Test Runner
Deployment Tool
Cross-Platform Automation Engine
```

Every modern .NET tool—from Visual Studio to Docker containers and CI/CD pipelines—uses these same capabilities under the hood.



# Mentor's Golden Wisdom

> **"A professional developer is recognized not by how quickly they click buttons, but by how well they understand the architecture they are building. The .NET CLI teaches discipline, organization, and repeatable engineering practices. Once you master the CLI, you are no longer tied to a particular IDE—you can build, test, and deploy your applications anywhere the .NET SDK runs."**


# 🏁 Final Takeaway

```text
             Developer
                 │
                 ▼
          .NET CLI Commands
                 │
      ┌──────────┼──────────┐
      ▼          ▼          ▼
  Create     Build      Test
      │          │          │
      └──────────┼──────────┘
                 ▼
         Multi-Project Solution
                 │
                 ▼
     Modular Architecture
                 │
                 ▼

 Enterprise-Ready Applications
```

> **"As a Transflower mentor, I encourage every student to spend time in the terminal. The command line is where you'll truly understand how .NET solutions are created, connected, compiled, tested, and deployed. Visual Studio may boost your productivity, but the .NET CLI builds your confidence as an engineer."**