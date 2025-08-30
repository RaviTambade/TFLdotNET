
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

---

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
   └─ AccountEventListener.cs   # (we’ll add this)
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
                UnderBalance?.Invoke(
                    $"⚠️ Alert: Withdrawal denied for {HolderName}. " +
                    $"Balance would go below 1000!");
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



## 4️⃣ Listener: **BankingApp (Consumer)**

👉 File: `BankingApp/AccountEventListener.cs`

```csharp
using System;
using Banking;

namespace BankingApp
{
    public class AccountEventListener
    {
        public void HandleUnderBalance(string message)
        {
            Console.WriteLine(message);
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

            AccountEventListener listener = new AccountEventListener();

            // Subscribe listener to event
            acc.UnderBalance += listener.HandleUnderBalance;

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


### 🎯 Mentor Wrap-up

* **Delegate** = “Who should be called?”
* **Event** = “When something special happens, call them.”
* **Account** = Engine triggers the event.
* **Listener** = The driver who reacts.
* **Console App** = The car that shows what’s happening.
