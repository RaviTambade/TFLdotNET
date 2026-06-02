 # 👨‍🏫 **Building and Executing .NET Core Solution**

You’ve created a solution with two projects:

* **MyApp** → Console application (the executable entry point)
* **MyLibrary** → Class library (contains reusable business logic)

Now let’s go deeper and understand what actually happens when you build and execute it in **.NET 10**.

This is where students start becoming real engineers.



## 🛠 **Part 1: The Build Process (`dotnet build`)**

Imagine you're inside a professional workshop.

Your source code is the blueprint.
Your dependencies are the tools.
Your final executable is the machine.

The `dotnet build` command is the construction phase.

## ✅ What happens when you type:

```bash
dotnet build
```

## 1️⃣ MSBuild Starts the Pipeline

The .NET CLI hands control to **MSBuild**, the official build engine.

It reads:

* `MySolution.sln` → to understand all projects inside the solution
* `MyApp.csproj` and `MyLibrary.csproj` → to understand dependencies, SDK targets, framework versions, and build instructions

In .NET 10, build optimization is faster and smarter, especially for incremental builds.

## 2️⃣ Restore Dependencies (NuGet Package Resolution)

If required, .NET automatically performs:

```bash
dotnet restore
```

This means:

* It checks `.csproj` files for `<PackageReference>`
* Downloads required NuGet packages
* Stores them in your global package cache
* Generates dependency tracking files inside the `obj/` folder

For example:

```text
obj/project.assets.json
```

This helps .NET manage package versions and transitive dependencies.

If your project has no external packages, this step is extremely fast.

## 3️⃣ Compilation (C# → IL → Ready for Runtime)

Your `.cs` files are compiled by the C# compiler (`csc`) into:

## CIL → Common Intermediate Language

This is platform-independent code.

Output generated:

* `MyLibrary.dll`
* `MyApp.dll`

In .NET 10, support for improved JIT + AOT compilation makes execution faster and startup smoother.

By default, output goes here:

```text
MyApp/bin/Debug/net10.0/
MyLibrary/bin/Debug/net10.0/
```

Notice:

Not `net8.0`
Not `net9.0`

Now it is:

## `net10.0`

That’s your target framework.

## 4️⃣ Linking Project References

Because you added:

```bash
dotnet add MyApp/MyApp.csproj reference MyLibrary/MyLibrary.csproj
```

MSBuild ensures:

* `MyApp.dll` references `MyLibrary.dll`
* Proper dependency order is maintained during build

It automatically adds this inside `MyApp.csproj`:

```xml
<ProjectReference Include="..\MyLibrary\MyLibrary.csproj" />
```

This allows:

```csharp
Greeter.Hello()
```

to work across projects.

This is the beginning of layered architecture.

## 5️⃣ Final Output Structure

After successful build:

```text
MyApp
 └── bin/Debug/net10.0/
     ├── MyApp.dll
     ├── MyLibrary.dll
     ├── MyApp.pdb
     ├── MyApp.runtimeconfig.json
     ├── MyApp.deps.json
     └── other runtime files
```

### What these files mean:

* `.dll` → compiled application code
* `.pdb` → debugging symbols
* `.runtimeconfig.json` → runtime settings
* `.deps.json` → dependency resolution details

This is professional output—not just “code running.”

# ⚙ **Part 2: Execution Process (`dotnet run`)**

Now the machine starts.

When you run:

```bash
dotnet run --project MyApp/MyApp.csproj
```

the real execution begins.

## 1️⃣ `dotnet run` Triggers Build (If Needed)

Before execution:

* If no code changed → skips rebuild
* If code changed → rebuilds only affected projects

This is called **incremental build optimization**

.NET 10 improves this significantly.

Faster feedback = better developer productivity.

## 2️⃣ Runtime Execution Starts

Behind the scenes, this becomes:

```bash
dotnet MyApp.dll
```

Now the **.NET Runtime (CoreCLR)** takes over.

It:

* Loads `MyApp.dll`
* Loads `MyLibrary.dll` because MyApp depends on it
* Resolves dependencies
* Initializes runtime configuration
* Finds the application entry point

In modern .NET:

This may be:

```csharp
static void Main()
```

or

## Top-Level Statements

which are the default in .NET 10 console apps.

Cleaner code.

Less boilerplate.

Same power.

## 3️⃣ Program Flow Begins

Example:

```csharp
var name = args.Length > 0 ? args[0] : "World";

Console.WriteLine(Greeter.Hello(name));
```

Execution flow:

* If you pass `Ravi`, then `name = "Ravi"`
* Calls:

```csharp
Greeter.Hello("Ravi")
```

* `Greeter` returns:

```text
Hello, Ravi from .NET 10 MyLibrary!
```

* Console displays output

Simple from outside.

Powerful underneath.



# 🔁 Build vs Run vs Publish

| Command          | Purpose                                                                         |
| ---------------- | ------------------------------------------------------------------------------- |
| `dotnet build`   | Compiles source code and generates `.dll` files                                 |
| `dotnet run`     | Builds (if needed) + executes the application                                   |
| `dotnet publish` | Produces deployment-ready output for servers, Docker, cloud, or standalone apps |

In .NET 10, `publish` becomes even more important for:

* Containers
* Cloud deployment
* Native AOT apps
* Microservices
* Kubernetes
* Serverless systems

# 💡 Mentor Wisdom: Why This Matters

Because once you understand:
- ✔ How C# becomes IL
- ✔ How MSBuild manages projects
- ✔ How NuGet resolves dependencies
- ✔ How DLLs interact
- ✔ How CoreCLR executes your code
- ✔ How deployment actually works

…you stop being someone who “uses .NET”

and become someone who truly understands .NET.

That is the difference between:

### Writing code and Engineering software

That difference builds careers.