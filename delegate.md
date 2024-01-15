# Delegates
A delegate is a reference to a method.
All delegates inherit from the System.delegate type
It is foundation for Event Handling.
Delegate Types
Unicast (Single cast)
Multicast Delegate
Unicast (Single cast) Delegate
Steps in using delegates
i. Define delegate
ii. Create instance of delegate
iii. Invoke delegate


```
delegate string strDelegate(string str);
strDelegate strDel =new strDelegate(strObject.ReverseStr);
string str=strDel(“Hello Transflower”);
// or use this Syntax
string str =strDel.Invoke(“Hello Transflower”);
```

Muticase Delegate
 
A Multicast delegate derives from System.MulticastDelegate class.
It provides synchronous way of invoking the methods in the invocation list.
Generally multicast delegate has void as their return type
```
delegate void strDelegate(string str);
strDelegate delegateObj;
strDelegate Upperobj = new strDelegate(obj.UppercaseStr);
strDelegate Lowerobj = new strDelegate(obj.LowercaseStr);
delegateObj=Upperobj;
delegateObj+=Lowerobj;
delegateObj(“Welcome to Transflower”);
```

Delegate chaining
Instances of delegate can be combined together to form a chain
Methods used to manage the delegate chain are
• Combine method
• Remove method


```
//calculator is a class with two methods Add and Subtract
Calculator obj1 = new Calculator();
CalDelegate [] delegates = new CalDelegate[];
 { new CalDelegate(obj1.Add),
 new CalDelegate(Calculator. Subtract)};
CalDelegate chain = (CalDelegate)delegate.Combine(delegates);
Chain = (CalDelegate)delegate.Remove(chain, delegates [0]);

```



Asynchronous Delegate
It is used to invoke methods that might take long time to complete.
Asynchronous mechanism more based on events rather than on delegate

```

delegate void strDelegate(string str);
public class Handler
{
public static string UpperCase(string s) {return s. ToUpper() ;}
} 
strDelegate caller = new strDelegate(handler. UpperCase);
IAsyncResult result = caller.BeginInvoke(“transflower”, null, null);
// . . .
String returnValue = caller.EndInvoke(result);

```

Anonymous Method
It is called as inline Delegate.
It is a block of code that is used as the parameter for the delegate
```
delegate string strDelegate(string str);
public static void Main()
{
strDelegate upperStr = delegate(string s) {return s.ToUpper() ;};
}
 


```

Events
• An Event is an automatic notification that some action has occurred.
• An Event is built upon a Delegat


```
public delegate void AccountOperation();
public class Account
 { private int balance;
 public event AccountOperation UnderBalance;
 public event AccountOperation OverBalance;
 public Account() {balance = 5000 ;}
 public Account(int amount) {balance = amount ;}
 public void Deposit(int amount)
 { balance = balance + amount;
 if (balance > 100000) { OverBalance(); }
 }
 public void Withdraw(int amount)
 { balance=balance-amount;
 if(balance < 5000) { UnderBalance () ;}
 }}}




```

Event Registrations using Event Handlers

```
class Program
 { static void Main(string [] args)
 { Account axisBanktflAccount = new Account(15000);
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
 {Console.WriteLine("Pay Penalty of 500 within 15 days"); }
 static void BlockBankAccount()
 {Console.WriteLine("Your Bank Account has been blocked") ;}
 static void PayProfessionalTax()
 {Console.WriteLine("You are requested to Pay Professional Tax") ;}
 static void PayIncomeTax()
 {Console.WriteLine("You are requested to Pay Income Tax as TDS") ;}
}



```