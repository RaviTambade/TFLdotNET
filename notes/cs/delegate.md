# Delegates
A delegate is a reference type that holds the reference of a class method. Any method which has the same signature as delegate can be assigned to delegate. It is very similar to the function pointer but with a difference that delegates are a type-safe. We can say that it is the object-oriented implementation of function pointers.

## steps for defining and using delegates

1. <b>Declaration</b>
A delegate is declared by using the keyword delegate, otherwise it resembles a method declaration.
2. <b>Instantiation</b>
To create a delegate instance, we need to assign a method (which has same signature as delegate) to delegate.
3. <b>Invocation</b>
Invoking a delegate is like as invoking a regular method.

### Types of delegates

1. <b>Single cast delegate</b>
A single cast delegate holds the reference of only single method. In previous example, created delegate is a single cast delegate.

```
delegate string strDelegate(string str);
strDelegate strDel =new strDelegate(strObject.ReverseStr);

string str=strDel(“Hello Transflower”);
// or use this Syntax
string str =strDel.Invoke(“Hello Transflower”);
```

2. <b>Multi cast delegate</b>
A delegate which holds the reference of more than one method is called multi-cast delegate. A multicast delegate only contains the reference of methods which return type is void. The + and += operators are used to combine delegate instances. Multicast delegates are considered equal if they reference the same methods in the same order.


 
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

### Delegate chaining
Instances of delegate can be combined together to form a chain
Methods used to manage the delegate chain are

• Combine method
• Remove method

```
//calculator is a class with two methods Add and Subtract

Calculator obj1 = new Calculator();
CalDelegate [] delegates = new CalDelegate[];
    { 
        new CalDelegate(obj1.Add),
        new CalDelegate(Calculator. Subtract)
    };
CalDelegate chain = (CalDelegate)delegate.Combine(delegates);
Chain = (CalDelegate)delegate.Remove(chain, delegates [0]);
```
### Asynchronous Delegate
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
string returnValue = caller.EndInvoke(result);
```

### Key points about delegates
1. Delegates are like C++ function pointers but are type safe.
2. Delegates allow methods to be passed as parameters.
3. Delegates are used in event handling for defining callback methods.
4. Delegates can be chained together i.e. these allow defining a set of methods that executed as a single unit.
5. Once a delegate is created, the method it is associated will never changes because delegates are immutable in nature.
6. Delegates provide a way to execute methods at run-time.
7. All delegates are implicitly derived from System.MulticastDelegate, class which is inheriting from System.Delegate class.
8. Delegate types are incompatible with each other, even if their signatures are the same. These are considered equal if they have the reference of same method.

### Anonymous Method
- It is called as inline Delegate.
- It is a block of code that is used as the parameter for the delegate
```
delegate string strDelegate(string str);

public static void Main()
{
    strDelegate upperStr = delegate(string s) {return s.ToUpper() ;};
}
```