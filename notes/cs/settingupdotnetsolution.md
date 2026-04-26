# 👨‍🏫 **Setting Up Your First .NET 10 Solution with CLI**

Imagine you are entering a workshop—not a classroom. No IDE dependency, no drag-and-drop shortcuts. Just you, your terminal, and your mindset to build like a real software engineer.

I’m your mentor—not to do things for you, but to show you how professionals structure clean, scalable solutions using the **.NET 10 CLI**.

Ready?

Open your terminal.
Take a deep breath.
Let’s build something modular, maintainable, and industry-ready.



## ✅ **Step 1: Check Your Tools First**

Before building anything, a craftsman checks his tools.

```bash
dotnet --version
```

If it prints something like:

```bash
10.0.xxx
```

Perfect—you’re ready for .NET 10.

If not, install the latest **.NET 10 SDK** first.



## ✅ **Step 2: Create Your Workshop (Solution Folder)**

```bash
mkdir MySolution
cd MySolution
```

This folder becomes your workspace—your engineering desk where everything will be organized.



## ✅ **Step 3: Create the Solution File (.sln)**

```bash
dotnet new sln -n MySolution
```

A `.sln` file is like your project map.

It doesn’t contain business logic itself, but it manages all projects inside your ecosystem.

Think of it as the architect’s blueprint.



## ✅ **Step 4: Create the Console Application (The Entry Point)**

```bash
dotnet new console -n MyApp
```

This creates the application users interact with.

In .NET 10, console apps are even cleaner with modern top-level statements and improved runtime performance.



## ✅ **Step 5: Create a Class Library (Reusable Business Logic)**

```bash
dotnet new classlib -n MyLibrary
```

This project holds reusable business logic.

Professionals separate concerns.

They don’t dump everything inside `Program.cs`.

They build maintainable architecture.



## ✅ **Step 6: Add Both Projects to the Solution**

```bash
dotnet sln add MyApp/MyApp.csproj
dotnet sln add MyLibrary/MyLibrary.csproj
```

Now your solution officially knows both projects exist.

Your workshop is organized.



## ✅ **Step 7: Connect MyApp to MyLibrary**

```bash
dotnet add MyApp/MyApp.csproj reference MyLibrary/MyLibrary.csproj
```

This means:

**MyApp can now use services from MyLibrary**

Exactly how enterprise applications work.

UI talks to Business Logic.

Business Logic talks to Data Layer.

Clean separation.



## ✅ **Step 8: Add Real Code**

### 📁 MyLibrary/Class1.cs

```csharp
namespace MyLibrary;

public class Greeter
{
    public static string Hello(string name)
        => $"Hello, {name} from .NET 10 MyLibrary!";
}
```



### 📁 MyApp/Program.cs

```csharp
using MyLibrary;

var name = args.Length > 0 ? args[0] : "World";

Console.WriteLine(Greeter.Hello(name));
```

Notice:

No traditional `Main()` method required.

Modern .NET uses **top-level statements** by default.

Cleaner. Faster. Better.



## ✅ **Step 9: Build the Solution**

```bash
dotnet build
```

This compiles everything and checks for errors.

A professional always builds before running.

Never assume.

Always verify.



## ✅ **Step 10: Run the Application**

```bash
dotnet run --project MyApp/MyApp.csproj
```

Or pass an argument:

```bash
dotnet run --project MyApp/MyApp.csproj -- Ravi
```

Output:

```text
Hello, Ravi from .NET 10 MyLibrary!
```

Congratulations.

That is your first properly structured multi-project .NET 10 solution.

Not a toy project.

A professional foundation.



## 🧠 **Quick Command Recap (Memory Booster)**

```bash
mkdir MySolution && cd MySolution

dotnet new sln -n MySolution

dotnet new console -n MyApp

dotnet new classlib -n MyLibrary

dotnet sln add MyApp/MyApp.csproj
dotnet sln add MyLibrary/MyLibrary.csproj

dotnet add MyApp/MyApp.csproj reference MyLibrary/MyLibrary.csproj

# write your code

dotnet build

dotnet run --project MyApp/MyApp.csproj
```



## 💡 **Mentor Tips & Troubleshooting**

| Problem                         | Why It Happens               | Solution                                   |
| ------------------------------- | ---------------------------- | ------------------------------------------ |
| `dotnet run` error              | Project not specified        | Use `--project` or go inside MyApp         |
| Build failed                    | Missing project reference    | Add reference using `dotnet add reference` |
| VS Code doesn’t detect solution | Wrong folder opened          | Open the root folder containing `.sln`     |
| NuGet restore issue             | Package dependencies missing | Run `dotnet restore`                       |



## 🌟 **What Comes Next?**

Now you can grow this into real enterprise architecture:

* Add Web API projects using ASP.NET Core
* Connect MySQL or SQL Server databases
* Add Entity Framework Core
* Build layered architecture
* Integrate RabbitMQ, Redis, gRPC
* Deploy using Docker and Kubernetes
* Make it cloud-ready for Microsoft Azure, Amazon Web Services, and Google Cloud

This is how students become developers.

This is how developers become engineers.

And this is where mentorship truly begins.
