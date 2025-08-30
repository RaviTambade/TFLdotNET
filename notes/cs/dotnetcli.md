
# 👨‍🏫 "The Craftsman’s Toolbox – Understanding .NET CLI"

Imagine you are a craftsman.
Before you build a house, you need tools: a hammer, saw, drill, measuring tape.
In the same way, as a **.NET developer**, your toolbox is the **`dotnet CLI`**.
It allows you to **create, build, test, package, and run applications** without depending on heavy IDEs like Visual Studio.

## 1️⃣ Getting Started – Your First Tool

👉 Command:

```bash
dotnet --version
```

* This checks if your toolbox is installed and ready.
* Like asking: *“Which version of hammer do I have?”*

👉 Command:

```bash
dotnet --list-sdks
```

* Shows which SDKs are available.
* Similar to checking if you have different screwdrivers for different tasks.

## 2️⃣ Creating a Solution – The Foundation

👉 Command:

```bash
dotnet new sln -n MyShop
```

* Think of this as **laying the foundation of your house**.
* The `.sln` (solution) is the container for multiple projects.

👉 Add a project:

```bash
dotnet new console -n MyShop.App
dotnet sln add MyShop.App/MyShop.App.csproj
```

* Created a **room** (project) and added it into the **house** (solution).


## 3️⃣ Building – Assembling the Structure

👉 Command:

```bash
dotnet build
```

* This **compiles your code** into DLLs.
* Think of it as assembling raw bricks into walls.
* Output is stored in the **bin/Debug/netX.0** folder.

## 4️⃣ Running – Testing Your Creation

👉 Command:

```bash
dotnet run --project MyShop.App
```

* This **runs your code** directly.
* Like switching on the lights in your house to see if wiring works.

## 5️⃣ Testing – Quality Check

👉 Add a test project:

```bash
dotnet new xunit -n MyShop.Tests
dotnet sln add MyShop.Tests/MyShop.Tests.csproj
dotnet add MyShop.Tests/MyShop.Tests.csproj reference MyShop.App/MyShop.App.csproj
```

👉 Run tests:

```bash
dotnet test
```

* This is like the **inspector checking your house**:

  * Doors open/close properly?
  * Plumbing works?

## 6️⃣ Packaging – Making It Ready to Ship

👉 Command:

```bash
dotnet pack -c Release
```

* This packages your project into a **NuGet package (.nupkg)**.
* Like wrapping your product into a **box ready for delivery**.


## 7️⃣ Publishing – Deploying the House

👉 Command:

```bash
dotnet publish -c Release -o ./publish
```

* Creates a folder with everything needed to run your app outside your machine.
* Like handing over the **keys of the house** to someone else.

## 🛠️ Summary Flow for Students

1. `dotnet new sln -n MyShop` → create solution
2. `dotnet new console -n MyShop.App` → create project
3. `dotnet sln add ...` → add project to solution
4. `dotnet build` → compile
5. `dotnet run` → execute
6. `dotnet new xunit` + `dotnet test` → testing
7. `dotnet pack` → package as NuGet
8. `dotnet publish` → deploy-ready files


# 👨‍🏫 BankingSolution – Wiring Delegates & Events

*"Think of this as building a **Banking Engine** (the class library) and a **Banking Car** (the console app). The car can’t move without the engine, and the engine is useless if nobody drives it."*


## 1️⃣ Create the Solution & Projects

```bash
# Step 1: Create solution
dotnet new sln -n BankingSolution
cd BankingSolution

# Step 2: Create provider (class library) – the Engine
dotnet new classlib -o Banking

# Step 3: Create consumer (console app) – the Car
dotnet new console -o BankingApp

# Step 4: Add both projects to solution
dotnet sln add Banking/Banking.csproj
dotnet sln add BankingApp/BankingApp.csproj

# Step 5: Mount engine into car (add reference)
cd BankingApp
dotnet add reference ../Banking/Banking.csproj
cd ..
```


## 2️⃣ Folder Structure (Expected)

```
BankingSolution/
├─ BankingSolution.sln
├─ Banking/                 # DLL project (provider)
│  ├─ Banking.csproj
│  └─ Account.cs            # (we’ll add this)
└─ BankingApp/              # EXE project (consumer)
   ├─ BankingApp.csproj
   ├─ Program.cs
    
```


## 3️⃣ Engine: The **Banking Class Library**

👉 File: `Banking/Account.cs`

```csharp
using System;

namespace Banking
{
    public class Account
    {
        public int AccountNumber { get; set; }
        public string HolderName { get; set; }
        public double Balance { get; private set; }

        // Delegate definition
        public delegate void UnderBalanceHandler(string message);

        // Event based on delegate
        public event UnderBalanceHandler? UnderBalance;

        public Account(int accNo, string holder, double balance)
        {
            AccountNumber = accNo;
            HolderName = holder;
            Balance = balance;
        }

        public void Withdraw(double amount)
        {
            if (Balance - amount < 1000)   // Business rule: min balance
            {
                // Fire the event
                
            }
            else
            {
                Balance -= amount;
                Console.WriteLine($"✅ {HolderName} withdrew {amount}. New Balance: {Balance}");
            }
        }
    }
}
```




## 5️⃣ Driver: **Program.cs**

👉 File: `BankingApp/Program.cs`

```csharp
using System;
using Banking;

namespace BankingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Account acc = new Account(101, "Ravi", 5000);

            Console.WriteLine("🚀 Banking Demo Started");
            
            acc.Withdraw(2000);  // ✅ allowed
            acc.Withdraw(2500);  // ❌ will fire event
        }
    }
}
```

 

## 6️⃣ Build & Run

```bash
dotnet build
dotnet run --project BankingApp
```

## 7️⃣ Expected Output

```
🚀 Banking Demo Started
✅ Ravi withdrew 2000. New Balance: 3000
⚠️ Alert: Withdrawal denied for Ravi. Balance would go below 1000!
```

# 🚗 The Three Vehicles for .NET Development

### 1️⃣ **.NET CLI** – *The Bicycle*

* Pure **command-line interface** (terminal/PowerShell).
* You create, build, run, test, and publish using commands like `dotnet new`, `dotnet build`, `dotnet run`, etc.
* **Pros**: Lightweight, cross-platform (Windows, Linux, macOS), works in automation/CI-CD, no heavy installation.
* **Cons**: Manual, no UI help, you must remember commands.

👉 Best for:

* Learning basics (under the hood).
* Developers working on Linux/macOS.
* CI/CD automation.
* Minimalists who like full control.

---

### 2️⃣ **Visual Studio Code (VS Code)** – *The Motorbike*

* Lightweight **code editor**, not a full IDE.
* Uses **extensions** (C#, Debugger for .NET, NuGet, Git, etc.) to support .NET.
* Relies on the **.NET CLI under the hood** → when you build/run/debug, VS Code just calls the same CLI commands.
* **Pros**: Free, open source, cross-platform, customizable, fast.
* **Cons**: Needs extensions for everything, not as “out-of-the-box” as Visual Studio IDE.

👉 Best for:

* Students, open-source contributors.
* Developers working cross-platform.
* Those who want flexibility and lightweight environment.

### 3️⃣ **Visual Studio IDE (Windows only)** – *The Luxury Car*

* Full-featured **Integrated Development Environment** (IDE).
* Rich UI, wizards, drag-and-drop, project templates, graphical designers.
* Internally, it **wraps .NET CLI commands** with menus and toolbars (e.g., Build → Build Solution runs `dotnet build`).
* **Pros**: Enterprise-grade, integrated tools (IntelliSense, Debugger, Test Explorer, Designer, Git integration, NuGet, Azure).
* **Cons**: Heavy installation, Windows-only (though VS for Mac existed, it was retired in 2024).

👉 Best for:

* Enterprise developers.
* Large-scale applications (ASP.NET Core, WPF, WinForms, Xamarin).
* Teams working with Azure/SQL Server deeply integrated.


# ⚖️ Comparison at a Glance

| Feature        | **.NET CLI** (Bicycle) | **VS Code** (Motorbike)  | **Visual Studio IDE** (Luxury Car) |
| -------------- | ---------------------- | ------------------------ | ---------------------------------- |
| Platform       | Cross-platform         | Cross-platform           | Windows only                       |
| Type           | Command-line tool      | Editor + Extensions      | Full IDE                           |
| Setup Required | SDK only               | Editor + Extensions      | Large install (Workloads)          |
| Ease of Use    | Hard (manual commands) | Medium (extensions help) | Easy (menu-driven, wizards)        |
| Features       | Build/run/test only    | Debug, Git, Extensions   | Enterprise features, GUI designers |
| Performance    | Fastest                | Lightweight              | Heavy but powerful                 |
| Best For       | Learning, CI/CD, Linux | Students, cross-platform | Enterprise, Windows devs           |



# 🎯 Mentor Wrap-up

* **.NET CLI** is the **foundation** — you must know it.
* **VS Code** is the **practical, portable choice** for modern devs.
* **Visual Studio IDE** is the **enterprise powerhouse** when you need everything integrated in one place.

 

# 👨‍🏫 CLI vs Visual Studio – *Manual Tools vs Power Tools*

Imagine you are a **carpenter**.

* With a **hand saw and hammer** (CLI), you cut wood, nail pieces, and slowly build a chair.
* With a **power machine** (Visual Studio IDE), you press buttons, and the machine does the cutting, drilling, and polishing automatically.

Both give you a **chair** at the end — but the experience and *control* are different.

 
## 🖥️ Visual Studio IDE (Menu-Driven UI)

When we use **Visual Studio**:

* **File → New → Project** → creates project (internally runs `dotnet new ...`)
* **Solution Explorer → Add Project** → (internally runs `dotnet sln add ...`)
* **Right-click → Add Reference** → (internally runs `dotnet add reference ...`)
* **Build → Build Solution** → (internally runs `dotnet build`)
* **Debug → Start** → (internally runs `dotnet run`)
* **Test Explorer → Run Tests** → (internally runs `dotnet test`)
* **Right-click → Publish** → (internally runs `dotnet publish`)

So, Visual Studio is like a **menu-driven wrapper** over the **same dotnet CLI commands**.

## 🛠️ Why Teach CLI First?

1. **Transparency** – Students see what really happens under the hood.
2. **Portability** – They can work on Linux, macOS, or CI/CD pipelines (no Visual Studio).
3. **Automation** – CLI fits naturally into DevOps pipelines (Jenkins, GitHub Actions, Azure DevOps).
4. **Confidence** – If IDE crashes or isn’t available, they can still build/run/test everything.

 

## 🎯 Mentor’s Tip

* *“Learn to drive with a manual car first (CLI). Once you master it, automatic cars (Visual Studio) will feel effortless. But if you only learn automatic, you’ll be stuck when the system doesn’t support it.”*
 
