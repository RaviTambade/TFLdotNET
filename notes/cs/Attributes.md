# Attributes in C#
Using attributes or annotations in programming offers several advantages:

1. **Metadata**: Attributes provide a way to attach metadata or additional information to code elements such as classes, methods, properties, parameters, etc. This metadata can be inspected at runtime, allowing for dynamic behavior based on it.

2. **Declarative Programming**: Attributes enable declarative programming by allowing developers to specify behavior or configuration directly within the code, rather than scattering configuration logic throughout the application.

3. **Extensibility**: Attributes make the code more extensible by providing a mechanism for adding custom behaviors or features without modifying the existing code. Other parts of the system can then utilize these attributes to customize behavior.

4. **Framework Integration**: Attributes are often used for integration with frameworks and libraries. Many frameworks use attributes to configure behavior, define routes, specify serialization settings, etc. This simplifies configuration and promotes consistency within the ecosystem.

5. **Tooling Support**: Attributes are recognized by IDEs and other development tools, providing better tooling support. For example, IDEs can offer auto-completion, validation, and other features based on the presence of attributes.

6. **Documentation**: Attributes can serve as documentation by providing additional context or information about code elements. They make it easier for developers to understand the purpose and usage of different parts of the codebase.

7. **Aspect-Oriented Programming (AOP)**: Attributes are commonly used in AOP to implement cross-cutting concerns such as logging, security, and transaction management. By attaching attributes to methods or classes, developers can apply these concerns uniformly across the application without cluttering the core business logic.

8. **Validation and Constraints**: Attributes can be used for validation purposes, allowing developers to define rules and constraints directly within the code. Frameworks and libraries often use validation attributes to enforce data integrity and ensure data consistency.

Overall, attributes provide a flexible and powerful mechanism for adding metadata, configuring behavior, and extending the functionality of code in a wide range of programming scenarios.

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