.

# 👨‍🏫 **.Setting Up Your First .NET Core Solution with CLI**

Imagine you are entering a workshop—not a classroom. No IDEs, no drag-and-drop. Just you, your terminal, and your willingness to build like a real developer. I’m your mentor, not to do things for you, but to **show you how real professionals structure solutions using the .NET CLI**.

Ready? Open your terminal. Take a deep breath. Let’s build something clean, modular, and professional.


## ✅ **Step 1: Do you even have the tools?**

Before building anything, a craftsman checks his tools.

```bash
dotnet --version
```

If it prints a number like `9.0.100` — perfect. If not, install .NET SDK first.


## ✅ **Step 2: Create Your Workshop (Solution Folder)**

```bash
mkdir MySolution
cd MySolution
```

This folder becomes your workspace — like your carpentry bench.


## ✅ **Step 3: Create the Solution File (.sln)**

```bash
dotnet new sln -n MySolution
```

A `.sln` file is like your workshop directory — it doesn’t do any work itself, but it knows where all your tools and projects are.


## ✅ **Step 4: Create the Console Application (The Front Door)**

```bash
dotnet new console -n MyApp
```

This generates a console app that will **interact with users**.



## ✅ **Step 5: Create a Class Library (Your Toolbox)**

```bash
dotnet new classlib -n MyLibrary
```

This project contains **reusable logic**, separate from the app. Professionals don’t hardcode everything in Program.cs — they structure code.

---

## ✅ **Step 6: Register Both Projects into the Solution**

```bash
dotnet sln add MyApp/MyApp.csproj
dotnet sln add MyLibrary/MyLibrary.csproj
```

Now your solution knows both projects exist.

---

## ✅ **Step 7: Make MyApp Use MyLibrary**

```bash
dotnet add MyApp/MyApp.csproj reference MyLibrary/MyLibrary.csproj
```

This is like saying, *“Hey MyApp, if you need help, you can use tools from MyLibrary.”*

---

## ✅ **Step 8: Add Actual Code**

📁 **MyLibrary/Class1.cs**

```csharp
namespace MyLibrary;
public class Greeter
{
    public static string Hello(string name) => $"Hello, {name} from MyLibrary!";
}
```

📁 **MyApp/Program.cs**

```csharp
using System;
using MyLibrary;

class Program
{
    static void Main(string[] args)
    {
        var name = args.Length > 0 ? args[0] : "World";
        Console.WriteLine(Greeter.Hello(name));
    }
}
```

---

## ✅ **Step 9: Build (Compiling Your Work)**

```bash
dotnet build
```

This checks if your code is valid and compiles everything.

---

## ✅ **Step 10: Run the Console App**

```bash
dotnet run --project MyApp/MyApp.csproj
```

Or with an argument:

```bash
dotnet run --project MyApp/MyApp.csproj -- Ravi
```

Output:

```
Hello, Ravi from MyLibrary!
```

Yes! That’s your first two-project solution working perfectly.

---

## 🧠 **Quick Command Recap (Memory Booster)**

```bash
mkdir MySolution && cd MySolution
dotnet new sln -n MySolution
dotnet new console -n MyApp
dotnet new classlib -n MyLibrary
dotnet sln add MyApp/MyApp.csproj MyLibrary/MyLibrary.csproj
dotnet add MyApp/MyApp.csproj reference MyLibrary/MyLibrary.csproj
# write code...
dotnet build
dotnet run --project MyApp/MyApp.csproj
```


## 💡 **Mentor Tips & Troubleshooting**

| Problem                                  | Why it Happens               | Solution                           |
| ---------------------------------------- | ---------------------------- | ---------------------------------- |
| `dotnet run` error                       | You didn’t specify a project | Use `--project` or go inside MyApp |
| No output / compilation failed           | Reference missing            | Add reference between projects     |
| Visual Studio Code doesn’t load solution | You opened just the folder   | Open root folder (with `.sln`)     |



## 🌟 **Now What?**

You have just built a **real-world-style solution** using only the CLI like a professional.

From here you can:

* Add more console apps or libraries
* Connect APIs, databases, or UI projects
* Turn this into a full enterprise architecture

I’m here to mentor you further.
