

### 💳 Interfaces – A Bank’s Contract

"Imagine a bank with different types of accounts: **Savings Account**, **Current Account**, and maybe even **Loan Account**.

Every account **must support certain actions**: deposit money, withdraw money, and check balance.

But **how** these actions work can be different for each account type:

* Savings accounts might apply interest on deposit.
* Current accounts might allow overdrafts.
* Loan accounts might not allow withdrawals at all.

The **bank doesn’t care** about the internal details — it just knows: *any account must follow the contract*."

In C#, this contract is called an **interface**.


### How This Looks in Banking C#

```csharp
// The contract – every bank account must implement these actions
public interface IAccount
{
    void Deposit(decimal amount);
    void Withdraw(decimal amount);
    decimal CheckBalance();
}

// Savings Account implementing the contract
public class SavingsAccount : IAccount
{
    private decimal _balance = 0;
    private decimal _interestRate = 0.05m;

    public void Deposit(decimal amount)
    {
        _balance += amount + (amount * _interestRate);
        Console.WriteLine($"SavingsAccount: Deposited {amount} + interest. Balance: {_balance}");
    }

    public void Withdraw(decimal amount)
    {
        if (_balance >= amount)
        {
            _balance -= amount;
            Console.WriteLine($"SavingsAccount: Withdrew {amount}. Balance: {_balance}");
        }
        else
        {
            Console.WriteLine("SavingsAccount: Insufficient funds!");
        }
    }

    public decimal CheckBalance() => _balance;
}

// Current Account implementing the same contract
public class CurrentAccount : IAccount
{
    private decimal _balance = 0;
    private decimal _overdraftLimit = 1000;

    public void Deposit(decimal amount)
    {
        _balance += amount;
        Console.WriteLine($"CurrentAccount: Deposited {amount}. Balance: {_balance}");
    }

    public void Withdraw(decimal amount)
    {
        if (_balance + _overdraftLimit >= amount)
        {
            _balance -= amount;
            Console.WriteLine($"CurrentAccount: Withdrew {amount}. Balance: {_balance}");
        }
        else
        {
            Console.WriteLine("CurrentAccount: Exceeds overdraft limit!");
        }
    }

    public decimal CheckBalance() => _balance;
}
```


### Using the Interface

```csharp
class Program
{
    static void Main()
    {
        // The bank system can work with any account type through IAccount
        IAccount account = new SavingsAccount();
        account.Deposit(1000);
        account.Withdraw(200);

        account = new CurrentAccount();
        account.Deposit(500);
        account.Withdraw(1200); // Uses overdraft
    }
}
```


### 💡 Mentor Notes

1. **Interface = Contract**: The bank ensures that every account type implements the required actions.
2. **Decoupling**: The bank system interacts with `IAccount` and **doesn’t care** about the specific account type.
3. **Flexibility**: You can add new account types in the future (like `LoanAccount`) **without changing the existing code**.
4. **Polymorphism + Interface**: The same code (`Deposit`, `Withdraw`) works differently depending on the account type.

 

### Final Thoughts for the Apprentice

Interfaces are the **contract** that bind your software parts together cleanly and predictably. They enable the elegant dance of collaboration between classes — letting you swap, improve, or extend implementations without breaking everything else.

## 🏦 Explicit Interface Implementation – Handling Name Conflicts

"Imagine you are in a bank working with **Transactions**.

* Every transaction can have **Order Details** (like which product or service is purchased).
* Every transaction also has **Customer Details** (like who performed the transaction).

Now, suppose both **Order Details** and **Customer Details** require a method called `ShowDetails()`.

💡 Problem: How does the `Transaction` class handle **two methods with the same name**?

The answer is **Explicit Interface Implementation** — we fully qualify which method belongs to which interface. This way, there’s **no ambiguity**.



### Banking Example in C#

```csharp
// Interface for Order Details
public interface IOrderDetails
{
    void ShowDetails();
}

// Interface for Customer Details
public interface ICustomerDetails
{
    void ShowDetails();
}

// Transaction class implements both interfaces explicitly
public class Transaction : IOrderDetails, ICustomerDetails
{
    // Implementation specific to order details
    void IOrderDetails.ShowDetails()
    {
        Console.WriteLine("Showing Order Details: Transaction #12345, Product: Savings Account Subscription");
    }

    // Implementation specific to customer details
    void ICustomerDetails.ShowDetails()
    {
        Console.WriteLine("Showing Customer Details: Customer Name: Ravi Tambade, Account: 987654321");
    }
}
```

### Using Explicit Interface Implementation

```csharp
class Program
{
    static void Main()
    {
        Transaction txn = new Transaction();

        // Accessing Order Details
        IOrderDetails order = txn;
        order.ShowDetails(); 
        // Output: Showing Order Details: Transaction #12345, Product: Savings Account Subscription

        // Accessing Customer Details
        ICustomerDetails customer = txn;
        customer.ShowDetails();
        // Output: Showing Customer Details: Customer Name: Ravi Tambade, Account: 987654321
    }
}
```

### 💡 Mentor Notes

1. **Why explicit implementation?**

   * When multiple interfaces have the **same method signature**, explicit implementation avoids conflicts.

2. **Access restriction:**

   * Explicitly implemented methods **cannot be called directly** via the class instance (`txn.ShowDetails()` will not compile).
   * You must **cast to the interface type** to access the method.

3. **Banking analogy:**

   * Think of it as having **two separate managers** in the bank:

     * One handles **order processing**
     * Another handles **customer verification**
   * Even though both have a method called `ShowDetails()`, they work **independently**.