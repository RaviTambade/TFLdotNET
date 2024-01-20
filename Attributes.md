# Attributes
- In C#, an attribute is a declarative tag that you can apply to classes, methods, properties, and other code elements.
- An attribute provides additional information about the code element to which it is applied. For example, you can use an attribute to indicate how an object should be serialized.
- All attributes class inherits from the System.Attribute class. Besides providing built-in attributes, you can create custom attribute classes that extend the System.Attribute class.

The following example demonstrates how to use a built-in Serializable attribute for the Person class. The Serializable attribute instructs .NET that the Person class can be serialized into a binary format:
```
[Serializable]
class Person
{
    public string? Name { get; set; }
    public sbyte? Age {  get; set; }
}
```

## Why do you need C# attributes?
- Attributes add metadata to your code so that .NET or other tools can use it at run time. 
    - For example, Visual Studio IDE uses attributes to provide IntelliSense and code completion suggestions. Also, the .NET runtime uses attributes to determine how to execute your code.
- Attributes can also be useful for enforcing coding conventions. 
    - For example, you can use the Obsolete attribute to mark a method of a class obsolete. Then, Visual Studio can give a warning if you attempt to call the obsolete method.

### Creating a custom attribute

The following program demonstrates how to create a custom attribute called Author:
 
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

