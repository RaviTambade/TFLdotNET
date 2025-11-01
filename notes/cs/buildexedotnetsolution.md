 
# 👨‍🏫 **What Happens When You Build and Run a .NET Solution?**

You’ve created a solution with two projects:

* **MyApp** → Console application (the one you run)
* **MyLibrary** → Class library (provides reusable logic)

Now let’s step into what actually happens when you build and execute it.

---

## 🛠 **Part 1: The Build Process (dotnet build)**

Imagine you’re in a workshop constructing a machine. You have blueprints (source code), dependencies (tools), and a final machine (executable). The `dotnet build` process is that construction phase.

### ✅ **What happens step-by-step when you type:**

```bash
dotnet build
```

### 1️⃣ **MSBuild Starts the Pipeline**

The .NET CLI delegates the work to **MSBuild**, the official build engine.
It reads:

* `MySolution.sln` → to know all projects inside
* `MyApp.csproj` and `MyLibrary.csproj` → to understand dependencies and build rules

---

### 2️⃣ **Restoration of Dependencies (.NET restores NuGet packages)**

If required, it runs:

```bash
dotnet restore
```

This means:

* It checks `.csproj` files for `<PackageReference>`.
* Downloads necessary packages to your system cache.
* Generates a `obj/project.assets.json` file for dependency tracking.

*In our case, no external packages are used → so it’s quick.*

---

### 3️⃣ **Compilation (Turning C# Code into IL)**

Each C# file (`.cs`) is compiled into **Intermediate Language (IL)** by the C# compiler (`csc`).
Output:

* `MyLibrary.dll`
* `MyApp.dll`

These go to:

```
MyApp/bin/Debug/net9.0/
MyLibrary/bin/Debug/net9.0/
```

---

### 4️⃣ **Linking Project References**

Since you added:

```bash
dotnet add MyApp/MyApp.csproj reference MyLibrary/MyLibrary.csproj
```

MSBuild ensures:

* `MyApp.dll` **references** `MyLibrary.dll`
* It adds this line in `MyApp.csproj`:

  ```xml
  <ProjectReference Include="..\MyLibrary\MyLibrary.csproj" />
  ```

So now MyApp can call `Greeter.Hello()` from MyLibrary.

---

### 5️⃣ **Output Structure**

After a successful build, your compiled files sit like this:

```
MyApp
 └── bin/Debug/net9.0/
     ├── MyApp.dll          ← compiled app
     ├── MyLibrary.dll      ← library used by app
     ├── MyApp.pdb          ← debugging symbols
     └── [runtime.config files]
```

---

## ⚙ **Part 2: Execution Process (dotnet run)**

Now the magic begins.

When you run:

```bash
dotnet run --project MyApp/MyApp.csproj
```

Here’s what actually happens:

### 1️⃣ **dotnet run triggers a build (if needed)**

* If no changes → it skips.
* If you changed code → rebuilds **only updated projects**.

---

### 2️⃣ **Execution Starts**

The command behind the scenes is:

```bash
dotnet MyApp.dll
```

The **.NET runtime (CoreCLR)** takes over:

* Loads `MyApp.dll`
* Loads `MyLibrary.dll` because MyApp depends on it
* Finds the entry point → `static void Main(string[] args)`
* Starts execution

---

### 3️⃣ **Program Flow**

Inside `Main()`:

```csharp
var name = args.Length > 0 ? args[0] : "World";
Console.WriteLine(Greeter.Hello(name));
```

* If you passed `Ravi`, then `name = "Ravi"`
* Calls `Greeter.Hello("Ravi")`
* Greeter returns `"Hello, Ravi from MyLibrary!"`
* Console prints it.

✅ That’s how your program runs successfully.

---

## 🔁 **Build vs Run — Clear Difference**

| Command          | Purpose                                                  |
| ---------------- | -------------------------------------------------------- |
| `dotnet build`   | Compiles source code → generates `.dll` files            |
| `dotnet run`     | Builds (if needed) + executes the application            |
| `dotnet publish` | Produces final deployable output (self-contained or not) |

---

## 💡 **Mentor Wisdom: Why This Matters?**

Because when you understand:
✔ How your code becomes IL
✔ How projects reference each other
✔ How .dll files run on the runtime
…then you’re no longer *using* .NET — you *understand* .NET.

That’s what separates a student from a developer.
