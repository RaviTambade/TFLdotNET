
# 🧑‍🏫 **Journey Into OOP with a Bank Account**

Tejas, imagine you are standing in front of a classroom, and you want your students to *feel* OOP, not just memorize definitions.
So let's begin with a simple story…


# 🌟 **Chapter 1 — The Bank of India**

In the city of **India**, everything is digital. Even the bank!

One day, the Bank Manager calls you:

> **“Tejas, I want to create thousands of bank accounts inside our software.
> But all accounts must follow the same structure.
> Can you help?”**

You smile and say:

> **“Yes sir, that’s what Object-Oriented Programming is for.”**


# 🌱 **Chapter 2 — Understanding a Class**

You explain to the manager:

> **“A *class* is like a blueprint or design of an object.”**

Just like the bank has a **form** for opening accounts:

* It asks for name
* Account number
* Balance
* Account type

That form is **not** a real account.
It simply describes *how* an account should look.

👉 That "form" in C# is a **class**.

### ✔ Example:

```csharp
class Account
{
    // Data members
    private string accountHolderName;
    private string accountNumber;
    private double balance;
}
```


# 🧱 **Chapter 3 — Creating Objects (Real Accounts)**

The manager asks:

> “Blueprint is fine… but where are the real accounts?”

You reply:

> “Objects are the real accounts created from the blueprint (class).”

You can create many objects:

```csharp
Account a1 = new Account();
Account a2 = new Account();
Account a3 = new Account();
```

Each object stores:

* its own name
* its own balance
* its own account number

Just like real bank accounts!


# 🔒 **Chapter 4 — Encapsulation (The Vault Room)**

Banks lock important information inside vaults.

In OOP:

* Sensitive data should be protected.
* Nobody should directly access your vault (balance).
* They should interact *only via secure methods*.

That’s why we use:

### ✔ `private` — information hidden

### ✔ `public` — safe door to access information


# 🎛 **Chapter 5 — Getters and Setters (Secure Windows at the Counter)**

When a customer asks for their balance, they don’t enter the vault.
The bank employee checks and gives them the balance through a counter window.

In code, that window is the **getter/setter**.

```csharp
public string AccountHolderName
{
    get { return accountHolderName; }
    set { accountHolderName = value; }
}
```

This way:

* data is hidden
* access is controlled
* validation can be added

The students understand it instantly.


# 🚪 **Chapter 6 — Methods (Actions the Account Can Perform)**

The manager says:

> “Every account must do things — deposit, withdraw, show balance.”

You reply:

> “Yes sir, these are **methods** — the behavior of an object.”

```csharp
public void Deposit(double amount)
{
    balance += amount;
}

public void Withdraw(double amount)
{
    if(amount <= balance)
        balance -= amount;
    else
        Console.WriteLine("Insufficient balance!");
}

public void Display()
{
    Console.WriteLine($"Name: {accountHolderName}, Balance: {balance}");
}
```


# 🍼 **Chapter 7 — Constructor (Birth of the Account)**

When a real customer opens a new account, the bank gives:

* a fresh account number
* zero balance
* initial setup

In C#, when an object is created, a **constructor** is called.

```csharp
public Account(string name, string accNo)
{
    accountHolderName = name;
    accountNumber = accNo;
    balance = 0;
}
```

This ensures every account starts with proper values.


# ⚰️ **Chapter 8 — Destructor (Closing the Account)**

In real life, when an account is closed, files are removed.

C# destructor is rarely used, but conceptually:

```csharp
~Account()
{
    // Cleanup code
}
```

This is like a final cleanup when the object is destroyed.


# 🏦 **Full Class – Mentor-Style C# Account Example**

```csharp
using System;

class Account
{
    // Encapsulated data
    private string accountHolderName;
    private string accountNumber;
    private double balance;

    // Constructor
    public Account(string name, string accNo)
    {
        accountHolderName = name;
        accountNumber = accNo;
        balance = 0;
    }

    // Getter - Setter
    public string AccountHolderName
    {
        get { return accountHolderName; }
        set { accountHolderName = value; }
    }

    public double Balance
    {
        get { return balance; }
    }

    // Methods
    public void Deposit(double amount)
    {
        balance += amount;
    }

    public void Withdraw(double amount)
    {
        if (amount <= balance)
            balance -= amount;
        else
            Console.WriteLine("Insufficient balance!");
    }

    public void Display()
    {
        Console.WriteLine($"Account Holder: {accountHolderName}");
        Console.WriteLine($"Account Number: {accountNumber}");
        Console.WriteLine($"Balance: {balance}");
    }

    // Destructor
    ~Account()
    {
        // Cleanup
    }
}
```


# 🎬 **Chapter 9 — Using the Account Class (Creating Objects)**

```csharp
class Program
{
    static void Main(string[] args)
    {
        Account a1 = new Account("Tejas Tambade", "ACC1001");

        a1.Deposit(5000);
        a1.Withdraw(1200);

        a1.Display();
    }
}
```



# 🎉 Final Words to Students

Tell them:

> “Class is your design, object is your real working thing.
> Keep your data private.
> Talk to objects only through methods.
> And use constructors to give your objects a meaningful start.”
