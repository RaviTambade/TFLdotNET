# Attributes
Declarative tags that convey information to runtime.
Stored with the metadata of the Element
.NET Framework provides predefined Attributes
The Runtime contains code to examine values of attributes and to act on them
Types of Attributes
Standard Attributes
Custom Attributes
Standard Attributes
.NET  provides many pre-defined attributes.
General Attributes
COM Interoperability Attributes
Transaction Handling Attributes
Visual designer component- building attributes

```
[Serializable]
public class Employee
{
 [NonSerialized]
public string name;
}
```

Custom Attributes
User defined class which can be applied as an attribute on any .NET compatibility Types like:
Class,Constructor, Delegate, Enum, Event, Field, Interface, Method, Parameter, Property, Return Value, Structure

```
// Custom Attribute

namespace TFL.Annotations;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class PermissionRequiredAttribute : Attribute
{
    public string Permission { get; }
    public PermissionRequiredAttribute(string permission)
    {
        Permission = permission;
       }
}

public interface ICredentials
{
        public   string[] GetCredentials();
}


using TFL.Annotations;
namespace TFL.Security;

[PermissionRequired("administrator")]
[PermissionRequired("manager")]
public class Credentials : ICredentials
{
    public string[] GetCredentials()
    {
        throw new NotImplementedException();
    }
}


//Main Entrypoint code segment 
using TFL.Annotations;
using TFL.Security;
Credentials myBankCredentials=new Credentials();
Credentials mycompanyCredentials=new Credentials();
Credentials mySocialNetworkingCredentials=new Credentials();

//Reflection:
//Getting information about code at runtime
Type t=myBankCredentials.GetType();
Type t2= typeof(Credentials);
Console.WriteLine(t.Name);
Console.WriteLine(t.IsClass);

IEnumerable<string> permissions = 
    typeof(Credentials).GetCustomAttributes(
        typeof(PermissionRequiredAttribute), true)
            .Cast<PermissionRequiredAttribute>()
            .Select(x => x.Permission);

foreach( string permission in permissions){
    Console.WriteLine(permission);
}

```