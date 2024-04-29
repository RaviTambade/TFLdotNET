# IComparable and IComparer Interfaces

In .NET, both `IComparable` and `IComparer` interfaces are used for object comparison, but they serve different purposes:

1. **`IComparable` Interface**: This interface is typically implemented by a class when you want instances of that class to be able to compare themselves with other instances of the same class. It provides a natural ordering for objects. The `CompareTo` method, which is part of `IComparable`, returns an integer indicating whether the current object is less than, equal to, or greater than the other object.

2. **`IComparer` Interface**: This interface is used to define a separate class for comparing instances of a particular type. It allows you to create custom comparison logic without modifying the classes being compared. The `IComparer` interface defines a `Compare` method that compares two objects of the same type and returns an integer indicating their relative order.

Let's illustrate these concepts with a simple example:

Suppose we have a class called `Person` with properties `Name` and `Age`. We want to compare instances of this class based on their ages. We'll use both `IComparable` and `IComparer` interfaces.

```csharp
using System;
using System.Collections.Generic;

public class Person : IComparable<Person>
{
    public string Name { get; }
    public int Age { get; }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }

    // Implementing IComparable interface
    public int CompareTo(Person other)
    {
        // Compare the ages of two Person objects
        return Age.CompareTo(other.Age);
    }
}

public class AgeComparer : IComparer<Person>
{
    // Implementing IComparer interface
    public int Compare(Person x, Person y)
    {
        // Compare the ages of two Person objects
        // Compare the ages of two Person objects
        // Return -1 if x's age is less than y's age
        // Return 0 if they are equal
        // Return 1 if x's age is greater than y's age
        return x.Age.CompareTo(y.Age);
    }
}

class Program
{
    static void Main(string[] args)
    {
        var people = new List<Person>
        {
            new Person("Alice", 30),
            new Person("Bob", 25),
            new Person("Charlie", 35)
        };

        // Sorting using IComparable interface
        people.Sort();

        Console.WriteLine("Sorted by age using IComparable:");
        foreach (var person in people)
        {
            Console.WriteLine($"{person.Name} - {person.Age}");
        }

        // Sorting using AgeComparer class implementing IComparer interface
        people.Sort(new AgeComparer());

        Console.WriteLine("\nSorted by age using AgeComparer:");
        foreach (var person in people)
        {
            Console.WriteLine($"{person.Name} - {person.Age}");
        }
    }
}
```

In this example:

- The `Person` class implements the `IComparable<Person>` interface, providing a natural ordering based on age.
- The `AgeComparer` class implements the `IComparer<Person>` interface, providing a custom comparison logic for age.
- We create a list of `Person` objects and sort them using both natural order (using `Sort()` method directly) and custom order (by passing an instance of `AgeComparer` to the `Sort()` method).

This demonstrates how both `IComparable` and `IComparer` interfaces can be used for object comparison in .NET, offering flexibility and customization in sorting logic.

## Using IComparable and IComparer interface in HR Application for Employee class

In .NET, the `IComparable` interface is used to define a method that compares the current instance of a class with another object of the same type. It's typically used when you want instances of a class to be sortable based on their natural order.

Here's a simple example to illustrate its usage:

Let's say we have a class called `Employee` with a property `Salary`, and we want to sort a list of `Employee` objects based on their salaries.

```csharp
using System;
using System.Collections.Generic;

// Define the Employee class
public class Employee : IComparable<Employee>
{
    public string Name { get; set; }
    public double Salary { get; set; }

    public Employee(string name, double salary)
    {
        Name = name;
        Salary = salary;
    }

    // Implement the CompareTo method required by the IComparable interface
    public int CompareTo(Employee other)
    {
        // Compare the salaries of two Employee objects
        // Return -1 if the current instance's salary is less than the other instance's salary
        // Return 0 if they are equal
        // Return 1 if the current instance's salary is greater than the other instance's salary
        return Salary.CompareTo(other.Salary);
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create a list of Employee objects
        List<Employee> employees = new List<Employee>
        {
            new Employee("Alice", 2500),
            new Employee("Bob", 2000),
            new Employee("Charlie", 3000)
        };

        // Sort the list of employees based on salary using the natural order defined by IComparable
        employees.Sort();

        // Display the sorted list
        foreach (var employee in employees)
        {
            Console.WriteLine($"{employee.Name} - {employee.Salary}");
        }
    }
}
```

In this example:
- We define an `Employee` class with `Name` and `Salary` properties.
- We implement the `IComparable<Employee>` interface in the `Employee` class.
- The `CompareTo` method compares the salary of the current `Employee` object with the salary of another `Employee` object.
- In the `Main` method, we create a list of `Employee` objects and sort them using the `Sort` method of the `List<T>` class.

This allows us to sort the list of `Employee` objects based on their salaries in ascending order. The `CompareTo` method provides the natural order comparison logic, but you can customize it to achieve different sorting behaviors based on your requirements.

## Using IComparable interface in HR Application for Employee class

In .NET, the `IComparer` interface is used to define a method that compares two objects. It's commonly employed when you need custom sorting logic for collections of objects. By implementing this interface, you can provide your own comparison logic tailored to the specific requirements of your application.

Here's a simple example to illustrate its usage:

Let's say we have a class called `Employee` with two properties: `Name` and `Salary`. We want to sort a list of `Employee` objects based on their salary.

```csharp
using System;
using System.Collections;
using System.Collections.Generic;

// Define the Employee class
public class Employee
{
    public string Name { get; set; }
    public int Salary { get; set; }

    public Employee(string name, int salary)
    {
        Name = name;
        Salary = salary;
    }
}

// Implement a custom comparer for Employee objects based on salary
public class SalaryComparer : IComparer<Employee>
{
    public int Compare(Employee x, Employee y)
    {
        // Compare the ages of two Employee objects
        // Return -1 if x's salary is less than y's salary
        // Return 0 if they are equal
        // Return 1 if x's salary is greater than y's salary
        return x.Salary.CompareTo(y.Salary);
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create a list of Employee objects
        List<Employee> employess = new List<Employee>
        {
            new Employee("Vedant", 25000),
            new Employee("Abhay", 15000),
            new Employee("Shubham", 30000)
        };

        // Sort the list of people based on age using the custom comparer
        people.Sort(new AgeComparer());

        // Display the sorted list
        foreach (var employee in employess)
        {
            Console.WriteLine($"{employee.Name} - {employee.Salary}");
        }
    }
}
```

In this example:

- We define a `Employee` class with `Name` and `Salary` properties.
- We implement a custom comparer class `SalayComparer` that implements the `IComparer<Employee>` interface.
- The `Compare` method of `AgeComparer` compares two `Employee` objects based on their ages.
- In the `Main` method, we create a list of `Employee` objects and sort them using the `Sort` method of the `List<T>` class, passing an instance of `EmployeeComparer` as the comparer.

This allows us to sort the list of `Employee` objects based on their ages in ascending order. You can customize the comparison logic within the `Compare` method to achieve different sorting behaviors.
