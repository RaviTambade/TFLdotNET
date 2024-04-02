# Reflection

Reflection is a feature of .NET to write code that can examine and modify other pieces of code dynamically is quite a useful power. Using reflection, you can, for instance, load a class dynamically from an Assembly, test whether a given type has a specific member, and even create code dynamically.

A program reflects on itself when it extracts metadata from its assemblies, then uses it to modify its own behavior or inform the user. You can compare Reflection to C++RTTI (Runtime Type Information), except that it has a much wider swath of capabilities. When you write a C# program that uses reflection, you can use either the TypeOf operator or the GetType() method to get the object’s type.

Several important tools make use of reflection to enable their working. One example is unit test frameworks, which use reflection to identify test classes and methods marked with the necessary attributes.

Reflection can be used to create applications called type browsers which allow users to select types and then read the data provided about them

```
    // Using GetType to obtain type information:

    int i = 42;
    System.Type type = i.GetType();
    System.Console.WriteLine(type);
```

Implementing reflection in C# requires a two-step process. You first get the “type” object, then use the type to browse members such as “methods” and “properties.”


To access the sample class Calculator from Test.dll assembly, the Calculator class should be defined as the following:

```
namespace Test
{
    public class Calculator
    {
        public Calculator() { ... }
        private double _number;
        public double Number { get { ... } set { ... } }
        public void Clear() { ... }
        private void DoClear() { ... }
        public double Add(double number) { ... }
        public static double Pi { ... }
        public static double GetPi() { ... }
    }
}
```

Then, you can use reflection to load the Test.dll assembly:

```
// dynamically load assembly from file Test.dll
Assembly testAssembly = Assembly.LoadFile(@"c:\Test.dll");
```

To create an instance of the calculator class:

```
// get type of class Calculator from just loaded assembly
Type calcType = testAssembly.GetType("Test.Calculator");
 

// create instance of class Calculator
object calcInstance = Activator.CreateInstance(calcType);
And access its members (the following examples illustrate getting values for the public double Number property):

// get info about property: public double Number
PropertyInfo numberPropertyInfo = calcType.GetProperty("Number");

// get value of property: public double Number
double value = (double)numberPropertyInfo.GetValue(calcInstance, null);

// set value of property: public double Number
numberPropertyInfo.SetValue(calcInstance, 10.0, null);

```

### How Reflection in C# Works

The main class for reflection is the System.Type class, which is a partial abstract class representing a type in the Common Type System (CTS). When you use this class, you can find the types used in a module and namespace and also determine if a given type is a reference or value type. You can parse the corresponding metadata tables to look through these items:

- <b>Fields</b>
- <b>Properties</b>
- <b>Methods</b>
- <b>Events</b>

The System.Type class also comes with several instance methods you can use to get information from a specific type. Here’s a list of some of the most important ones:

- <b>GetConstructors()</b> – gets the constructors for the type as an array of System.Reflection.ConstructorInfo.
- <b>GetMethods()</b> – gets the methods for the type as an array of System.Reflection.MethodInfo.
- <b>GetMembers()</b> – gets the members for the type as an array of System.Reflection.MemberInfo.

The System.Reflection namespace, as the name suggests, holds several useful classes if you want to work with reflection. Some of those are the three ones you’ve just read about. Here are some more important ones:

- <b>ParameterInfo</b>
- <b>Assembly</b>
- <b>AssemblyName</b>
- <b>PropertyInfo</b>

Late bindings can also be achieved through reflection. To illustrate, you might not know which assembly to load during compile time. In this instance, you can ask the user to enter the assembly name and type during run time so the application can load the appropriate assembly. With the System.Reflection.Assembly type, you can get three static types which allow you to load an assembly directly:

- <b>LoadFrom</b>
- <b>LoadWithPartialName</b>

When you consider that an assembly is a logical DLL or EXE and a manifest is a detailed overview of an assembly, then it makes sense that a PE (portable executable) file for CTS would have the extension of .dll or .exe. Within the PE file is mainly metadata, which contains a variety of different tables such as a:

- <b>Filed definition table</b>
- <b>Type definition table</b>
- <b>Method definition table</b>

When you parse these tables, you can retrieve an assembly’s types and attributes.

#### Uses for Reflection C#
There are several uses including:

1. Use Module to get all global and non-global methods defined in the module.
2. Use MethodInfo to look at information such as parameters, name, return type, access modifiers and implementation details.
3. Use EventInfo to find out the event-handler data type, the name, declaring type and custom attributes.
4. Use ConstructorInfo to get data on the parameters, access modifiers, and implementation details of a constructor.
5. Use Assembly to load modules listed in the assembly manifest.
6. Use PropertyInfo to get the declaring type, reflected type, data type, name and writable status of a property or to get and set property values.
7. Use CustomAttributeData to find out information on custom attributes or to review attributes without having to create more instances.