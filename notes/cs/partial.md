## 🏦 Partial Classes – Teamwork in Banking Code

"Imagine you are building a **Bank Account Management system**.
Two developers are working on the same `BankAccount` class:
1. **Developer A** focuses on the **core data and properties** like account number, balance, and customer name.
2. **Developer B** focuses on **business logic** like deposit, withdraw, and applying interest.

Rather than one developer writing a huge class, C# allows us to **split the class into multiple files** using the **`partial` keyword**.

At compile-time, the compiler **merges all parts** into a single class. This way, both developers can work **simultaneously without conflicts**.


### Banking Example in C#
**BankAccount.Data.cs** (developer A — properties/data)

```csharp
public partial class BankAccount
{
    public string AccountNumber { get; set; }
    public string CustomerName { get; set; }
    public decimal Balance { get; set; }
}
```

**BankAccount.Logic.cs** (developer B — business logic)

```csharp
public partial class BankAccount
{
    public void Deposit(decimal amount)
    {
        Balance += amount;
        Console.WriteLine($"{CustomerName} deposited {amount}. New Balance: {Balance}");
    }

    public void Withdraw(decimal amount)
    {
        if (Balance >= amount)
        {
            Balance -= amount;
            Console.WriteLine($"{CustomerName} withdrew {amount}. New Balance: {Balance}");
        }
        else
        {
            Console.WriteLine("Insufficient funds!");
        }
    }

    public void ApplyInterest(decimal rate)
    {
        Balance += Balance * rate;
        Console.WriteLine($"Interest applied to {CustomerName}'s account. New Balance: {Balance}");
    }
}
```

### Using the Partial Class

```csharp
class Program
{
    static void Main()
    {
        BankAccount account = new BankAccount
        {
            AccountNumber = "ACC123",
            CustomerName = "Ravi Tambade",
            Balance = 1000
        };

        account.Deposit(500);          // Works seamlessly
        account.Withdraw(200);         // Works seamlessly
        account.ApplyInterest(0.05m);  // Works seamlessly
    }
}
```

### 💡 Mentor Notes

1. **Why use partial classes?**

   * When multiple developers work on **different concerns** of the same class.
   * Helps organize **design vs logic**, **data vs behavior**, or **auto-generated vs custom code**.

2. **Compilation:**

   * All `.cs` files with the same partial class are **merged into one class** at compile-time.

3. **Banking analogy:**

   * Think of `BankAccount` as a **team effort**:

     * One person sets up the **account data**
     * Another writes the **banking operations**
   * Together, they form a **complete functional account** for the bank.

 
### The Invisible Contract: Interfaces in C\#

Imagine you’re signing a contract with someone — it clearly defines what each side promises to do. That’s exactly what **Interfaces** do in programming.

### What Is an Interface?
An **Interface** is like an invisible handshake or a contract between:

* **Providers** — who promise to deliver certain services or behaviors.
* **Consumers** — who rely on those promises without worrying about how they’re fulfilled.

### Why Are Interfaces Important?

Interfaces let us write software that’s:

* **Loosely Coupled** — Consumers and Providers don’t tightly depend on each other’s internal details.
* **Highly Cohesive** — Each piece focuses on doing its job well, nothing extra.

In other words, interfaces are the blueprint for building flexible, maintainable, and scalable software systems.