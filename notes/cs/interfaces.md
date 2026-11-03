## C# Interfaces – Designing Contracts Between Objects

### 1. Start with a Banking Story

Dear students, Imagine a bank has different types of accounts:

- Savings Account
- Current Account
- Loan Account

Every account may have different business rules.

For example:

| Account | Deposit | Withdraw | Special Rule |
|---|---|---|---|
| Savings | Allowed | Allowed | Interest calculation |
| Current | Allowed | Allowed | Overdraft facility |
| Loan | Allowed | Depends on loan rules | Loan repayment |

Now imagine the bank wants to build a common banking application. Should the banking application understand the internal implementation of every account?

No!

It should simply know that every supported account provides certain operations. This is where **interfaces** become useful.

### 2. What Is an Interface?

An interface defines a contract that implementing classes must fulfill.

```csharp
public interface IAccount
{
    void Deposit(decimal amount);
    void Withdraw(decimal amount);
    decimal CheckBalance();
}
```

Understand the contract:

```text
             IAccount
           (Contract)
                |
        +-------+-------+
        |               |
        v               v
 SavingsAccount   CurrentAccount
        |               |
        v               v
  Own implementation  Own implementation
```

The interface defines what operations are available. The implementing class defines how those operations work.

**Mentor:** Does an interface tell us exactly how a withdrawal should happen?

**Student:** No, sir. It only defines the operation that the class must implement.

### 3. Implementing the Interface – Savings Account

```csharp
public class SavingsAccount : IAccount
{
    private decimal balance = 0;
    private decimal interestRate = 0.05m;

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Invalid amount");

        decimal interest = amount * interestRate;
        balance += amount + interest;

        Console.WriteLine( $"Savings deposit: {amount}, Balance: {balance}");
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Invalid amount");

        if (balance >= amount)
        {
            balance -= amount;
            Console.WriteLine($"Savings withdrawal: {amount}, Balance: {balance}");
        }
        else
        {
            Console.WriteLine("Insufficient funds!");
        }
    }

    public decimal CheckBalance()
    {
        return balance;
    }
}
```

Notice:

- SavingsAccount implements `IAccount`.
- It provides all three required operations.
- It maintains its own balance.
- It applies its own business rules.

### 4. Implementing the Interface – Current Account

```csharp
public class CurrentAccount : IAccount
{
    private decimal balance = 0;
    private decimal overdraftLimit = 1000;

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Invalid amount");

        balance += amount;

        Console.WriteLine($"Current deposit: {amount}, Balance: {balance}");
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Invalid amount");

        if (balance + overdraftLimit >= amount)
        {
            balance -= amount;
            Console.WriteLine($"Current withdrawal: {amount}, Balance: {balance}");
        }
        else
        {
            Console.WriteLine("Overdraft limit exceeded!");
        }
    }

    public decimal CheckBalance()
    {
        return balance;
    }
}
```

Both classes follow the same contract. But their implementations are different.

### 5. Interface-Based Programming

Now let's write the banking application.

```csharp
public class Program
{
    public static void Main()
    {
        IAccount account;

        account = new SavingsAccount();

        account.Deposit(1000);
        account.Withdraw(200);

        Console.WriteLine(account.CheckBalance());

        account = new CurrentAccount();

        account.Deposit(500);
        account.Withdraw(1200);

        Console.WriteLine(account.CheckBalance());
    }
}
```

What is happening?

```text
             Banking Application
                      |
                      v
                  IAccount
                  /      \
                 /        \
                v          v
       SavingsAccount  CurrentAccount
                |          |
                v          v
          Savings Rules  Current Rules
```

The application communicates through `IAccount`. It doesn't need to know the internal implementation of the account.

### 6. Polymorphism Through Interfaces

Consider:

```csharp
IAccount account;

account = new SavingsAccount();
account.Deposit(1000);

account = new CurrentAccount();
account.Deposit(500);
```

The same reference type can refer to different implementations. This is runtime polymorphism.

```text
            IAccount reference
                   |
             +-----+-----+
             |           |
             v           v
       SavingsAccount CurrentAccount
             |           |
             v           v
        Savings logic  Current logic
```

The interface provides a common abstraction. The runtime object determines which implementation executes.

### 7. Why Interfaces Matter in Enterprise Applications

Imagine our Transflower insurance application. We need multiple payment options:

- UPI
- Credit Card
- Net Banking
- Wallet

Instead of creating separate payment logic inside every controller, we can define a contract.

```csharp
public interface IPayment
{
    bool Pay(decimal amount);
}
```

Implementations:

```csharp
public class UpiPayment : IPayment
{
    public bool Pay(decimal amount)
    {
        Console.WriteLine($"UPI payment: {amount}");
        return true;
    }
}

public class CardPayment : IPayment
{
    public bool Pay(decimal amount)
    {
        Console.WriteLine($"Card payment: {amount}");
        return true;
    }
}
```

Now we can create a payment service.

```csharp
public class PaymentService
{
    private readonly IPayment payment;

    public PaymentService(IPayment payment)
    {
        this.payment = payment;
    }

    public void ProcessPayment(decimal amount)
    {
        payment.Pay(amount);
    }
}
```

Usage:

```csharp
IPayment payment = new UpiPayment();
PaymentService service = new PaymentService(payment);
service.ProcessPayment(2500);
```

Later, we can supply `CardPayment` without rewriting the PaymentService. This is an example of loose coupling.

### 8. Understanding Dependency Inversion

Compare two designs.

**Tightly coupled design**

```text
PaymentService
      |
      v
  UpiPayment
```

The service directly depends on one implementation.

**Abstraction-based design**

```text
           PaymentService
                  |
                  v
               IPayment
                  |
          +-------+-------+
          |               |
          v               v
      UpiPayment      CardPayment
```

The high-level service depends on an abstraction. This makes it easier to replace implementations and write unit tests.

### 9. Explicit Interface Implementation

Now let's explore an interesting C# feature. Imagine a Transaction object implements two interfaces. Both interfaces contain a method with the same name:

```csharp
public interface IOrderDetails
{
    void ShowDetails();
}

public interface ICustomerDetails
{
    void ShowDetails();
}
```

What happens when a class implements both interfaces? We may want different behavior for each contract. This is where explicit interface implementation helps.

```csharp
public class Transaction : IOrderDetails, ICustomerDetails
{
    void IOrderDetails.ShowDetails()
    {
        Console.WriteLine("Showing Order Details");
    }

    void ICustomerDetails.ShowDetails()
    {
        Console.WriteLine("Showing Customer Details");
    }
}
```

Notice the syntax:

```csharp
void IOrderDetails.ShowDetails()
```

and

```csharp
void ICustomerDetails.ShowDetails()
```

We explicitly specify which interface owns the implementation.

### 10. Calling Explicit Interface Methods

```csharp
public class Program
{
    public static void Main()
    {
        Transaction transaction = new Transaction();

        IOrderDetails order = transaction;
        ICustomerDetails customer = transaction;

        order.ShowDetails();
        customer.ShowDetails();
    }
}
```

Output:

```text
Showing Order Details
Showing Customer Details
```

But this will not compile:

```csharp
transaction.ShowDetails();
```

Why?

Because explicitly implemented members are accessed through their interface references.

Visualize:

```text
                Transaction
                     |
              Implements
                     |
           +---------+---------+
           |                   |
           v                   v
      IOrderDetails      ICustomerDetails
           |                   |
           v                   v
      ShowDetails()       ShowDetails()
           |                   |
           v                   v
       Order logic        Customer logic
```

### 11. When Should We Use Explicit Implementation?

Use it when:

- Multiple interfaces have members with identical signatures.
- A class needs different behavior for each interface.
- You want to expose an operation only through the relevant interface contract.
- You want to avoid ambiguity in the public API of a class.

It is not mandatory whenever two interfaces contain the same method name. If the same implementation satisfies both contracts, a normal public implementation can be used.

### 12. Interface vs Abstract Class

Students often ask:

"Sir, what is the difference between an interface and an abstract class?"

Let's compare.

| Interface | Abstract Class |
|---|---|
| Defines a contract | Defines a common base abstraction |
| A class can implement multiple interfaces | A class can inherit only one class |
| No instance constructors | Can have constructors |
| Can define abstract members and supported default implementations | Can contain abstract and concrete members |
| Useful for capabilities and collaboration contracts | Useful for shared state and behavior |

Example:

```csharp
public interface IAccount
{
    void Deposit(decimal amount);
}
```

```csharp
public abstract class BankAccount
{
    public string AccountNumber { get; protected set; }

    public decimal Balance { get; protected set; }

    public abstract void Withdraw(decimal amount);

    public void DisplayBalance()
    {
        Console.WriteLine(Balance);
    }
}
```

A class can combine both:

```csharp
public class SavingsAccount : BankAccount, IAccount
{
    public void Deposit(decimal amount)
    {
        Balance += amount;
    }

    public override void Withdraw(decimal amount)
    {
        if (Balance >= amount)
            Balance -= amount;
    }
}
```

Remember:

- Interface: What capability or contract must be supported?
- Abstract class: What common foundation and behavior can derived classes share?

### 13. ASP.NET Core Dependency Injection Connection

In ASP.NET Core, interfaces are commonly used with Dependency Injection.

For example:

```csharp
public interface IStudentService
{
    string GetStudentName(int id);
}
```

Implementation:

```csharp
public class StudentService : IStudentService
{
    public string GetStudentName(int id)
    {
        return $"Student {id}";
    }
}
```

Register the dependency:

```csharp
builder.Services.AddScoped<
    IStudentService,
    StudentService>();
```

Inject it into a controller:

```csharp
public class StudentController : Controller
{
    private readonly IStudentService service;

    public StudentController(IStudentService service)
    {
        this.service = service;
    }

    public IActionResult Index()
    {
        string name = service.GetStudentName(101);

        return Content(name);
    }
}
```

Understand the architecture:

```text
             HTTP Request
                  |
                  v
          StudentController
                  |
                  v
           IStudentService
                  |
          Dependency Injection
                  |
                  v
           StudentService
                  |
                  v
             Business Logic
```

The controller depends on the interface, not directly on the concrete service. This is one of the most common uses of interfaces in ASP.NET Core applications.

### 14. Practical Lab – Banking Application

Create a console application:

```bash
dotnet new console -n BankingInterfaces
cd BankingInterfaces
```

Implement:

**IAccount**
- Deposit()
- Withdraw()
- CheckBalance()

**SavingsAccount**
- Apply interest rules.
- Prevent withdrawal beyond balance.

**CurrentAccount**
- Support overdraft.
- Enforce overdraft limit.

**LoanAccount**
- Support loan repayment.
- Do not provide an unrestricted withdrawal operation.

**BankingService**
- Accept an `IAccount`.
- Perform account operations through the interface.

Extension challenge:

Create two interfaces:

```csharp
public interface IAccountStatement
{
    void ShowDetails();
}

public interface ICustomerStatement
{
    void ShowDetails();
}
```

Implement both explicitly in a `BankTransaction` class.

### 15. Important Banking Design Discussion

A contract must represent valid business capabilities. For example, forcing a LoanAccount to support unrestricted withdrawals may be a poor design. Instead, consider separating capabilities:

```csharp
public interface IDepositable
{
    void Deposit(decimal amount);
}

public interface IWithdrawable
{
    void Withdraw(decimal amount);
}

public interface IBalanceInquiry
{
    decimal CheckBalance();
}
```

Now each account implements only the capabilities that make sense for its business rules. This is a practical application of the Interface Segregation Principle.

### 16. Student Assessment

1. What is an interface?
2. Why do we use the `I` prefix in names like `IAccount`?
3. What does it mean for a class to implement an interface?
4. Can a class implement multiple interfaces?
5. What is runtime polymorphism?
6. How do interfaces help achieve loose coupling?
7. What is explicit interface implementation?
8. Can an explicitly implemented method be called directly using a class reference?
9. What is the difference between an interface and an abstract class?
10. Why should an interface represent a meaningful business capability?


### Ravi Sir's Closing Thought

Dear students, Imagine building a banking application with 50 different account types. Would you write 50 completely different banking applications? No! You define a contract and allow each account to provide its own implementation.

That is the power of interfaces.

```text
Contract
   |
   v
Multiple Implementations
   |
   v
Polymorphism
   |
   v
Loose Coupling
   |
   v
Extensible Software
```

**Remember: A well-designed interface makes software components independent in implementation, but connected through a meaningful contract.**

— Ravi Sir, Transflower Mentor