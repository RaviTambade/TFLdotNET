Exceptions Handling
Abnormalities that occur during the execution of a program (runtime error).
.NET framework terminates the program execution for runtime error.
e.g. divide by Zero, Stack overflow, File reading error, loss of network connectivity
Mechanism to detect and handle runtime error.

```
int a, b=0;
Console.WriteLine(“My program starts“);
try 
{
a= 10/b;
}
catch(Exception e)
{
 Console.WriteLine(e.Message);
}
Console.WriteLine(“Remaining program”);
```


.NET Exception classe


SystemException FormatException
ArgymentException IndexOutOfException
ArgumentNullException InvalidCastExpression
ArraytypeMismatchException InvalidOperationException
CoreException NullReferenceException
DivideByZeroException OutOfMemoryException
StackOverflowException


User Defined Exception classes
Application specific class can be created using ApplicationException class.


```
class StackFullException:ApplicationException
{
 public string message;
 public StackFullException(string msg)
 {
Message = msg;
 }
}
public static void Main(string [] args)
{
 StackClass stack1= new StackClass();
 try
 { stack1.Push(54);
stack1.Push(24);
stack1.Push(53);
stack1.Push(89);
 }
catch(StackFullException s)
{
Console.WriteLine(s.Message);
}
}
```