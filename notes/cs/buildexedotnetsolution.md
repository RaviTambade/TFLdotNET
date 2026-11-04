# Building and Executing a .NET 10 Solution

## From C# Source Code to Running Software

Dear students, Today, let us understand something fundamental about software engineering. You have written C# code. You have created classes, methods, and projects. But have you ever wondered what actually happens when you type:

```
dotnet build
```

or

```
dotnet run
```

Does the computer directly execute your C# source code?

What role does MSBuild play? Who downloads external libraries? How does one project communicate with another? Where does the executable output come from?

Today, we will understand the internal journey of a .NET application. Remember: A programmer writes code. An engineer understands how that code becomes a running application.

## 1. Our Workshop: Create a Two-Project Solution

Imagine we are building a small application in a professional software development workshop.

Our requirement is simple:

- `MyApp` – Console application (executable entry point).
- `MyLibrary` – Class library (reusable business logic).

Why create two projects? Because professional applications separate responsibilities. 

Imagine you are developing a Student Management System.

Initially, you might write everything inside one project:
- Student information
- Student business rules
- Student calculations
- User interaction
- Application execution logic
It works. But as the application grows, maintaining everything in one place becomes difficult. In professional software development, we separate responsibilities.

```
                  MySolution.sln
                (.NET Solution)
                       |
             +---------+---------+
             |                   |
             v                   v
      +-------------+     +----------------+
      |   MyApp     |     |  MyLibrary     |
      +-------------+     +----------------+
      |             |     |                |
      | Console App |     | Class Library  |
      |             |     |                |
      | Program.cs  |     | Greeter.cs     |
      |             |     |                |
      +-------------+     +----------------+
             |                   |
             |                   |
             +------------------> 
                 Project Reference
                 (Dependency)
                       |
                       v
              Reusable Business
                   Logic

```

### Step 1: Create the solution

Open your terminal:

```
mkdir DotNetWorkshop
cd DotNetWorkshop

dotnet new sln -n MySolution

dotnet new console -n MyApp
dotnet new classlib -n MyLibrary
```

### Step 2: Add projects to solution

```
dotnet sln MySolution.sln add MyApp/MyApp.csproj

dotnet sln MySolution.sln add MyLibrary/MyLibrary.csproj
```

### Step 3: Establish project dependency

```
dotnet add MyApp/MyApp.csproj reference MyLibrary/MyLibrary.csproj
```

This creates a project reference inside `MyApp.csproj`.

```
<ItemGroup>
    <ProjectReference Include="..\MyLibrary\MyLibrary.csproj" />
</ItemGroup>
```

Now MyApp can use the public classes exposed by MyLibrary.

### Step 4: Write reusable business logic

Inside `MyLibrary/Greeter.cs`:

```
namespace MyLibrary;

public class Greeter
{
    public static void Hello()
    {
        Console.WriteLine(
            "Welcome to Transflower Learning!"
        );
    }
}
```

Inside `MyApp/Program.cs`:

```
using MyLibrary;

Greeter.Hello();

Console.WriteLine("Application completed.");
```

Expected output:

```
Welcome to Transflower Learning!
Application completed.
```

Congratulations! You have created a solution containing two cooperating projects.

But how does this code actually run?

Let's investigate.

## 2. Part One – Understanding the Build Process

Command: dotnet build

Think about a manufacturing workshop.

- Source code is the blueprint.
- NuGet packages are external components.
- Compiler is the manufacturing machine.
- MSBuild is the production manager.
- Compiled DLLs are the finished components.

When you execute:

```
dotnet build
```

the .NET build pipeline starts.

### Step 1: MSBuild takes control

The .NET CLI invokes MSBuild, the build engine responsible for coordinating the build process.

It examines:

```
MySolution.sln
    |
    |-- MyApp/MyApp.csproj
    |
    |-- MyLibrary/MyLibrary.csproj
```

MSBuild understands:

- Which projects belong to the solution.
- Which project depends on another.
- Which .NET SDK and target framework are configured.
- Which packages are required.
- Which compilation tasks must execute.

Mentor asks: Why should we compile MyLibrary before MyApp?

Student: Because MyApp depends on MyLibrary. The required library must be built before the consuming application can be compiled.

Exactly!

### Step 2: NuGet restores dependencies

Before compilation, the build process ensures that required dependencies are available.

```
dotnet restore
```

Suppose your project references an external package:

```
<ItemGroup>
    <PackageReference Include="Newtonsoft.Json"
                      Version="13.0.4" />
</ItemGroup>
```

NuGet performs dependency resolution.

```
MyApp.csproj
      |
      v
NuGet Restore
      |
      v
Check Package References
      |
      v
Download Required Packages
      |
      v
Global NuGet Package Cache
      |
      v
obj/project.assets.json
```

Important generated file:

```
obj/project.assets.json
```

It contains dependency resolution information used by the build system.

Mentor Insight: NuGet is not the compiler. Its responsibility is package management and dependency restoration.

### Step 3: C# compilation

Now the C# compiler (`csc`) takes your source code and converts it into Common Intermediate Language (CIL), commonly called IL.

```
Program.cs
Greeter.cs
    |
    v
C# Compiler (Roslyn)
    |
    v
Intermediate Language (IL)
    |
    v
MyApp.dll
MyLibrary.dll
```

Let's understand the difference.

| Source code | Compiled output |
| ----------- | --------------- |
| Program.cs  | MyApp.dll       |
| Greeter.cs  | MyLibrary.dll   |

The generated DLL contains compiled .NET code and metadata.

Important: IL is not native machine code. It is an intermediate representation that the .NET runtime can execute through JIT compilation or other deployment strategies.

### Step 4: Project reference resolution

Remember this command?

```
dotnet add MyApp/MyApp.csproj reference MyLibrary/MyLibrary.csproj
```

This tells the build system about the dependency relationship.

```
          MyApp
            |
            | depends on
            v
         MyLibrary
```

The build system handles the required project build ordering and dependency references.

This is the beginning of understanding layered architecture.

### Step 5: Understand the build output

After successful compilation, inspect:

```
MyApp/
  |
  +-- bin/
       |
       +-- Debug/
            |
            +-- net10.0/
                 |
                 +-- MyApp.dll
                 +-- MyLibrary.dll
                 +-- MyApp.deps.json
                 +-- MyApp.runtimeconfig.json
                 +-- MyApp.pdb
```

Let's understand these files.

| File                     | Responsibility            |
| ------------------------ | ------------------------- |
| MyApp.dll                | Compiled application code |
| MyLibrary.dll            | Compiled reusable library |
| MyApp.pdb                | Debugging symbols         |
| MyApp.deps.json          | Dependency information    |
| MyApp.runtimeconfig.json | Runtime configuration     |

Notice the directory:

```
bin/Debug/net10.0/
```

`net10.0` represents the target framework.

## 3. Part Two – Understanding Execution Process

Command: dotnet run

Build is completed.

Now imagine starting the machine we have manufactured.

Execute:

```
dotnet run --project MyApp/MyApp.csproj
```

What happens internally?

### Step 1: Build if required

`dotnet run` normally builds the application before executing it.

If the project is already up to date, incremental build processing avoids unnecessary compilation.

```
       dotnet run
            |
            v
      Build Required?
        /       \
      Yes        No
       |          |
       v          |
   Build Project  |
       |          |
       +----+-----+
            |
            v
       Execute App
```

### Step 2: .NET Runtime takes control

The execution process uses the .NET runtime, including CoreCLR for the normal CoreCLR execution model.

Conceptually:

```
dotnet MyApp.dll
```

The runtime performs several activities:

1. Loads the application assembly.
2. Resolves required dependencies.
3. Loads MyLibrary.dll when required.
4. Initializes the runtime environment.
5. Executes the application's entry point.

### Step 3: JIT compilation

Here comes an important concept.

Your C# compiler generated IL, not CPU-specific native instructions.

So how does the processor execute your application?

Meet the JIT Compiler – Just-In-Time Compiler.

```
      C# Source Code
             |
             v
       C# Compiler
             |
             v
       MyApp.dll
        (IL Code)
             |
             v
       .NET Runtime
          (CoreCLR)
             |
             v
       JIT Compiler
             |
             v
      Native Machine Code
             |
             v
          CPU
             |
             v
       Application Output
```

The JIT compiler translates IL methods into native machine instructions as they are needed.

This is one of the important mechanisms behind normal .NET application execution.

Mentor asks: Why doesn't the C# compiler directly generate native machine code for every platform?

Student: Because IL provides a portable intermediate representation. The runtime can generate appropriate machine instructions for the target environment.

Excellent!

## 4. Part Three – Build vs Run vs Publish

Students often confuse these three commands.

Let's understand them from a software engineering perspective.

| Command          | Responsibility                     | Result              |
| ---------------- | ---------------------------------- | ------------------- |
| `dotnet build`   | Compile the solution               | Build output        |
| `dotnet run`     | Build if needed and execute        | Running application |
| `dotnet publish` | Prepare application for deployment | Publish output      |

### Understanding Publish

Imagine your application works perfectly on your development laptop.

Now your client wants to run it on a Linux server or inside Docker.

You need deployment-ready output.

Execute:

```
dotnet publish MyApp/MyApp.csproj -c Release -o ./publish
```

This generates publish output in the specified directory.

```
Development Environment
          |
          v
     C# Source Code
          |
          v
      dotnet build
          |
          v
      dotnet publish
          |
          v
   Deployment Package
          |
     +----+------+
     |           |
     v           v
   Docker     Cloud Server
     |           |
     v           v
 Kubernetes   Production
```

Publish is particularly important for:

- Docker containers.
- Cloud deployments.
- ASP.NET Core applications.
- Microservices.
- Kubernetes workloads.
- Native AOT applications.
- Self-contained application deployment.

Remember, publishing does not automatically mean that your application is containerized or compiled using Native AOT. Those require additional configuration.

## 5. Part Four – Explore the Commands Yourself

Let's become comfortable with the .NET CLI.

| Command                   | Purpose                    |
| ------------------------- | -------------------------- |
| `dotnet --version`        | Check installed SDK        |
| `dotnet --list-sdks`      | List installed SDKs        |
| `dotnet sln list`         | List solution projects     |
| `dotnet restore`          | Restore dependencies       |
| `dotnet build`            | Compile solution           |
| `dotnet build -c Release` | Release build              |
| `dotnet run`              | Execute application        |
| `dotnet clean`            | Clean build outputs        |
| `dotnet publish`          | Generate deployment output |

Try these commands one by one.

```
dotnet --version

dotnet sln MySolution.sln list

dotnet restore

dotnet build

dotnet run --project MyApp/MyApp.csproj

dotnet clean

dotnet publish MyApp/MyApp.csproj -c Release
```

## 6. Transflower Practical Lab

Hands-on Assignment

Problem Statement: Build a Reusable Student Management Application

Your task is to extend the two-project solution.

Create the following structure:

```
StudentManagementSolution
    |
    |-- StudentApp
    |     |
    |     |-- Program.cs
    |
    |-- StudentLibrary
          |
          |-- Student.cs
          |-- StudentService.cs
```

Responsibilities:

StudentLibrary

- Create Student class.
- Store student ID, name and course.
- Implement StudentService.
- Implement AddStudent().
- Implement GetStudents().

StudentApp

- Create the application entry point.
- Instantiate StudentService.
- Add student records.
- Display student information.

### Student challenges

1. Build only the StudentLibrary project.
2. Build the complete solution.
3. Observe the generated DLL files.
4. Modify StudentLibrary and rebuild.
5. Observe incremental build behavior.
6. Execute StudentApp.
7. Publish the application in Release configuration.
8. Inspect the publish folder.

### Assessment questions

Knowledge checkpoint

0/5 answered

1\. Which component coordinates the .NET build process?

CLR

MSBuild

NuGet

JIT

2\. Which tool restores external NuGet dependencies?

Roslyn

CoreCLR

NuGet

PDB

3\. What does the C# compiler generate?

Only native machine code

IL and metadata in assemblies

Docker images

SQL scripts

4\. Which component converts IL methods into native code during normal JIT execution?

MSBuild

NuGet

JIT Compiler

Razor

5\. Which command prepares application output for deployment?

dotnet clean

dotnet publish

dotnet restore

dotnet sln list

Submit answers

## 7. Mentor Wisdom – From Programmer to Software Engineer

Students, remember this execution journey:

## The .NET Engineering Journey

1. Write C# Code

   Understand classes, methods, interfaces and business logic.
2. Organize Projects

   Separate application responsibilities into reusable libraries.
3. Restore Dependencies

   Understand NuGet and dependency resolution.
4. Build

   Understand MSBuild, Roslyn and IL generation.
5. Execute

   Understand CoreCLR, assembly loading and JIT compilation.
6. Publish

   Prepare software for production deployment.

One important thought to take home:

Don't become dependent on Visual Studio's Build and Run buttons. Learn what happens behind those buttons.

When you understand the build pipeline, runtime, dependencies and deployment process, you can troubleshoot real-world problems instead of blindly following instructions.

A developer knows how to write a program.

A software engineer understands how the program is built, executed, tested and deployed.

Transflower Learning – Learn the fundamentals. Understand the architecture. Build with confidence.

AI can generate code for you. But engineering judgment comes from understanding what happens behind that code.