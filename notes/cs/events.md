# **Events in C# – Like Real-Life Notifications**


### 🕰️ “Let me take you to a real-world moment before we dive into code…”

Imagine this:

You're working at your desk. Focused. Calm.

Suddenly, **your phone beeps** –
📱 “You’ve received an SMS: *Balance below ₹5,000*.”
Then, another one follows:
📱 “Bank Alert: Your account has been blocked due to low balance.”

You didn’t go and check your account manually — **the bank notified you** when something important happened.
This, my friends, is **exactly how Events work in C#**.


## 💡 What Is an Event?

In simple terms:

> **An Event is a notification sent by one object (Publisher) to one or more interested objects (Subscribers) when something important happens.**

Just like:

* The **Bank Account** is the publisher.
* **You**, the account holder (or tax department), are subscribers.
* You only get notified **when a certain rule or threshold is crossed**.


## 🧰 The Real Structure of an Event in C\#

Before we talk syntax, here's a real-world analogy:

🎓 Imagine a college:

* A **Student** scores below passing marks — the **Professor** raises an alert.
* **Parents** and the **Principal** are **subscribed** to get this alert.
* As soon as the student fails, the professor **raises an event** and **all subscribers get notified instantly**.


### 🔧 Step-by-Step: How Events Work in C\#

Here’s the simple mechanism:

1. **Declare a delegate** – This defines the *shape* of the method to be notified.
2. **Declare an event** – This wraps the delegate and allows only registered subscribers.
3. **Raise the event** – Only the publisher can trigger it.
4. **Subscribe** – Other classes subscribe using `+=` to get notified.


### 🏦 Let’s Understand With a Banking Example

```csharp
public delegate void AccountOperation(); // Step 1: Declare a delegate

public class Account
{
    private int balance;
    
    // Step 2: Declare events
    public event AccountOperation UnderBalance;
    public event AccountOperation OverBalance;

    public Account(int amount) { balance = amount; }

    public void Deposit(int amount)
    {
        balance += amount;
        if (balance > 100000)
            OverBalance(); // Step 3: Raise the event
    }

    public void Withdraw(int amount)
    {
        balance -= amount;
        if (balance < 5000)
            UnderBalance(); // Raise another event
    }
}
```

Now, let’s create subscribers.

```csharp
class Program
{
    static void Main()
    {
        Account myAccount = new Account(15000);

        // Step 4: Subscribe to events
        myAccount.UnderBalance += PayPenalty;
        myAccount.UnderBalance += BlockBankAccount;
        myAccount.OverBalance += PayIncomeTax;
        myAccount.OverBalance += PayProfessionalTax;

        myAccount.Withdraw(15000); // Will trigger UnderBalance
    }

    static void PayPenalty()
    {
        Console.WriteLine("⚠️ Pay ₹500 penalty within 15 days.");
    }

    static void BlockBankAccount()
    {
        Console.WriteLine("🚫 Your account has been blocked.");
    }

    static void PayIncomeTax()
    {
        Console.WriteLine("💰 Please pay applicable Income Tax.");
    }

    static void PayProfessionalTax()
    {
        Console.WriteLine("🧾 Professional Tax due this quarter.");
    }
}
```


### 🧠 Mentor’s Wisdom: Why Use Events?

* 🧩 **Decoupling**: Publisher doesn’t care *who* is listening. Just raises the event.
* 🔄 **Flexibility**: Add or remove subscribers anytime.
* 🎯 **Focus**: Makes classes do only what they are supposed to. Bank handles money. Tax Dept handles taxes. Clean.

## 🛠 Built-in Events: The Polished .NET Way

.NET has a **ready-made EventHandler** delegate so we don’t need to define one ourselves.

Here's a cleaner version of event handling:

```csharp
public class ProcessBusinessLogic
{
    public event EventHandler ProcessCompleted;

    public void StartProcess()
    {
        Console.WriteLine("📦 Process Started.");
        // Process logic
        OnProcessCompleted(EventArgs.Empty); // Raise event
    }

    protected virtual void OnProcessCompleted(EventArgs e)
    {
        ProcessCompleted?.Invoke(this, e); // Check and invoke
    }
}
```

### The Subscriber:

```csharp
class Program
{
    static void Main()
    {
        ProcessBusinessLogic bl = new ProcessBusinessLogic();
        bl.ProcessCompleted += Bl_ProcessCompleted; // Subscribe
        bl.StartProcess();
    }

    static void Bl_ProcessCompleted(object sender, EventArgs e)
    {
        Console.WriteLine("✅ Process Completed!");
    }
}
```

 

### 🧾 Mentor Notes: Key Takeaways

| Concept      | Real-World Analogy                             |
| ------------ | ---------------------------------------------- |
| Delegate     | Letter format or notification template         |
| Event        | The actual SMS or alert                        |
| Publisher    | Bank, college, hospital – whoever raises alert |
| Subscriber   | You, parents, tax dept, etc.                   |
| `+=`         | Subscribe to an alert                          |
| `-=`         | Unsubscribe from an alert                      |
| EventHandler | A generic built-in notification system         |
| EventArgs    | Extra data in alert – like “Amount = ₹12,000”  |

 

### 🧪 Mentor’s Closing Thought

> “Events in C# are like your personal alert system in software. They tell you exactly when something important happens — without needing to keep checking manually. They make your app **intelligent**, **reactive**, and **cleanly designed**. And remember, just like in life — it’s better to be **notified** than to keep asking.”

 
