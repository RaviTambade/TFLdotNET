# Events
An event is a notification sent by an object to signal the occurrence of an action. Events in .NET follow the observer design pattern.

The class who raises events is called Publisher, and the class who receives the notification is called Subscriber. There can be multiple subscribers of a single event. Typically, a publisher raises an event when some action occurred. The subscribers, who are interested in getting a notification when an action occurred, should register with an event and handle it.

In C#, an event is an encapsulated delegate. It is dependent on the delegate. The delegate defines the signature for the event handler method of the subscriber class.

### Declare an Event
An event can be declared in two steps:
1. Declare a delegate.
2. Declare a variable of the delegate with event keyword.


```

public delegate void AccountOperation();

public class Account
{
     private int balance;
     public event AccountOperation UnderBalance;
    public event AccountOperation OverBalance;
 
    public Account() 
    {
        balance = 5000;
    }

    public Account(int amount) 
    {
        balance = amount;
    }
 
    public void Deposit(int amount)
    {
        balance = balance + amount;
        if (balance > 100000) 
        {
            OverBalance(); 
        }
    }

    public void Withdraw(int amount)
    { 
        balance=balance-amount;
        if(balance < 5000) 
        { 
            UnderBalance ();
        }
    }
}
```

### Event Registrations using Event Handlers

```
class Program
{
    static void Main(string [] args)
    { 
        Account axisBanktflAccount = new Account(15000);
        //register Event Handlers
        axisBanktflAccount.UnderBalance+=PayPenalty;
        axisBanktflAccount.UnderBalance+=BlockBankAccount;
        axisBanktflAccount.OverBalance+=PayProfessionalTax;
        axisBanktflAccount.OverBalance+= PayIncomeTax;
        
        //Perform Banking Operations
        axisBanktflAccount.Withdraw(15000);
        Console.ReadLine();
    }


    //Event handlers
    static void PayPenalty()
    {
        Console.WriteLine("Pay Penalty of 500 within 15 days");
    }
    
    static void BlockBankAccount()
    {
        Console.WriteLine("Your Bank Account has been blocked");
    }
    
    static void PayProfessionalTax()
    {
        Console.WriteLine("You are requested to Pay Professional Tax");
    }
    
    static void PayIncomeTax()
    {
        Console.WriteLine("You are requested to Pay Income Tax as TDS");
    }
}
```

### Built-in EventHandler Delegate

.NET Framework includes built-in delegate types EventHandler and EventHandler<TEventArgs> for the most common events. Typically, any event should include two parameters: the source of the event and event data. Use the EventHandler delegate for all events that do not include event data. Use EventHandler<TEventArgs> delegate for events that include data to be sent to handlers.

```
class Program
{
    public static void Main()
    {
        ProcessBusinessLogic bl = new ProcessBusinessLogic();
        bl.ProcessCompleted += bl_ProcessCompleted; // register with an event
        bl.StartProcess();
    }

    // event handler
    public static void bl_ProcessCompleted(object sender, EventArgs e)
    {
        Console.WriteLine("Process Completed!");
    }
}

public class ProcessBusinessLogic
{
    // declaring an event using built-in EventHandler
    public event EventHandler ProcessCompleted; 

    public void StartProcess()
    {
        Console.WriteLine("Process Started!");
        // some code here..
        OnProcessCompleted(EventArgs.Empty); //No event data
    }

    protected virtual void OnProcessCompleted(EventArgs e)
    {
        ProcessCompleted?.Invoke(this, e);
    }
}

```

In the above example, the event handler bl_ProcessCompleted() method includes two parameters that match with EventHandler delegate. Also, passing this as a sender and EventArgs.Empty, when we raise an event using Invoke() in the OnProcessCompleted() method. Because we don't need any data for our event, it just notifies subscribers about the completion of the process, and so we passed EventArgs.Empty.

### Points to Remember :

1. An event is a wrapper around a delegate. It depends on the delegate.
2. Use "event" keyword with delegate type variable to declare an event.
3. Use built-in delegate EventHandler or EventHandler<TEventArgs> for common events.
4. The publisher class raises an event, and the subscriber class registers for an event and provides the event-handler method.
5. Name the method which raises an event prefixed with "On" with the event name.
6. The signature of the handler method must match the delegate signature.
7. Register with an event using the += operator. Unsubscribe it using the -= operator. Cannot use the = operator.
8. Pass event data using EventHandler<TEventArgs>.
9. Derive EventArgs base class to create custom event data class.
10. Events can be declared static, virtual, sealed, and abstract.
11. An Interface can include the event as a member.
12. Event handlers are invoked synchronously if there are multiple subscribers.
