## LINQ Language Integrated Query

The C# 3.0 language enhancements build on C# 2.0 to increase developer productivity: they make written code more 
concise and make working with data as easy as working with objects. These features provide the foundation for the 
LINQ project, a general purpose declarative query facility that can be applied to in-memory collections and data stored in 
external sources such as XML files and relational databases.
The C# 3.0 language enhancements consist of:

Auto-implemented properties automate the process of creating properties with trivial implementations
Implicitly typed local variables permit the type of local variables to be inferred from the expressions used to 
initialize them
Implicitly typed arrays a form of array creation and initialization that infers the element type of the 
array from an array initializer
Extension methods which make it possible to extend existing types and constructed types with 
additional methods
Lambda expressions an evolution of anonymous methods that concisely improves type inference and 
conversions to both delegate types and expression trees
Expression trees permit lambda expressions to be represented as data (expression trees) instead 
of as code (delegates)
Object and collection 
initializers
you can use to conveniently specify values for one or more fields or properties 
for a newly created object, combining creation and initialization into a single 
step
Query expressions provide a language-integrated syntax for queries that is similar to relational and 
hierarchical query languages such as SQL and XQuery
Anonymous types are tuple types automatically inferred and created from object initializer


Use of Automatically Implemented Properties
Easy Initialization with Object and Collection Initializers

```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace NewLanguageFeatures
{
 public class Customer
 {
 public int CustomerID { get; private set; }
 public string Name { get; set; }
 public string City { get; set; }
 public Customer(int ID)
 {
 CustomerID = ID;
 }
 public override string ToString()
 {
 return Name + "\t" + City + "\t" + CustomerID;
 }
 }
 class Program
 {
 static void Main(string[] args)
 {
 Customer c = new Customer(1);
 c.Name = "Maria Anders";
 c.City = "Berlin";
 Console.WriteLine(c);
 }
 }
}


Implicitly Typed Local Variables and Implicitly Typed Arrays

```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace NewLanguageFeatures
{
 public class Customer
 {
 public int CustomerID { get; private set; }
 public string Name { get; set; }
 public string City { get; set; }
 public Customer(int ID) { CustomerID = ID; }
 public override string ToString()
 {
 return Name + "\t" + City + "\t" + CustomerID;
 }
 }
 class Program
 {
 static void Main(string[] args)
 {
 List<Customer> customers = CreateCustomers();
 Console.WriteLine("Customers:\n");
 foreach (Customer c in customers)
 Console.WriteLine(c);
 }
 static List<Customer> CreateCustomers()
 {
 return new List<Customer>
 {
 new Customer(1) { Name = "Maria Anders", City = "Berlin" },
 new Customer(2) { Name = "Laurence Lebihan", City = "Marseille" },
 new Customer(3) { Name = "Elizabeth Brown", City = "London" },
 new Customer(4) { Name = "Ann Devon", City = "London" },
 new Customer(5) { Name = "Paolo Accorti", City = "Torino" },
 new Customer(6) { Name = "Fran Wilson", City = "Portland" },
 new Customer(7) { Name = "Simon Crowther", City = "London" },
 new Customer(8) { Name = "Liz Nixon", City = "Portland" }
 };
 } 
 }
}



Extending Types with Extension Methods




```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace NewLanguageFeatures
{
 public class Customer
 {
 public int CustomerID { get; private set; }
 public string Name { get; set; }
 public string City { get; set; }
 public Customer(int ID)
 {
 CustomerID = ID;
 }
 public override string ToString()
 {
 return Name + "\t" + City + "\t" + CustomerID;
 }
 }
 class Program
 {
 static void Main(string[] args)
 {
 List<Customer> customers = CreateCustomers();
 Console.WriteLine("Customers:\n");
 foreach (Customer c in customers)
 Console.WriteLine(c);
 }
 static List<Customer> CreateCustomers()
 {
 return new List<Customer>
 {
 new Customer(1) { Name = "Maria Anders", City = "Berlin" },
 new Customer(2) { Name = "Laurence Lebihan", City = "Marseille" },
 new Customer(3) { Name = "Elizabeth Brown", City = "London" },
 new Customer(4) { Name = "Ann Devon", City = "London" },
 new Customer(5) { Name = "Paolo Accorti", City = "Torino" },
 new Customer(6) { Name = "Fran Wilson", City = "Portland" },
 new Customer(7) { Name = "Simon Crowther", City = "London" },
 new Customer(8) { Name = "Liz Nixon", City = "Portland" }
 };
 } 
 }
}

```


Working with Lambda Expressions

```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace NewLanguageFeatures
{
 public static class Extensions
 {
 public static List<T> Append<T>(this List<T> a, List<T> b)
 {
 var newList = new List<T>(a);
 newList.AddRange(b);
 return newList;
 }
 public static bool Compare(this Customer customer1, Customer customer2)
 {
 if (customer1.CustomerID == customer2.CustomerID &&
 customer1.Name == customer2.Name &&
 customer1.City == customer2.City)
 {
 return true;
 }
 return false;
 }
 }
 public class Customer
 {
 public int CustomerID { get; private set; }
 public string Name { get; set; }
 public string City { get; set; }
 public Customer(int ID)
 {
 CustomerID = ID;
 }
 public override string ToString()
 {
 return Name + "\t" + City + "\t" + CustomerID;
 }
 }





 class Program
 {
 static void Main(string[] args)
 {
 var customers = CreateCustomers();
 var addedCustomers = new List<Customer>
 {
 new Customer(9) { Name = "Paolo Accorti", City = "Torino" },
 new Customer(10) { Name = "Diego Roel", City = "Madrid" }
 };
 var updatedCustomers = customers.Append(addedCustomers);
 var newCustomer = new Customer(10)
 {
 Name = "Diego Roel",
 City = "Madrid"
 };
 foreach (var c in updatedCustomers)
 {
 if (newCustomer.Compare(c))
 {
 Console.WriteLine("The new customer was already in the list");
 return;
 }
 }
 Console.WriteLine("The new customer was not in the list");
 }
 static List<Customer> CreateCustomers()
 {
 return new List<Customer>
 {
 new Customer(1) { Name = "Maria Anders", City = "Berlin" },
 new Customer(2) { Name = "Laurence Lebihan", City = "Marseille" },
 new Customer(3) { Name = "Elizabeth Brown", City = "London" },
 new Customer(4) { Name = "Ann Devon", City = "London" },
 new Customer(5) { Name = "Paolo Accorti", City = "Torino" },
 new Customer(6) { Name = "Fran Wilson", City = "Portland" },
 new Customer(7) { Name = "Simon Crowther", City = "London" },
 new Customer(8) { Name = "Liz Nixon", City = "Portland" }
 };
 } 
 }
}


```


Using Lambda Expressions to Create Expression Trees

```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace NewLanguageFeatures
{
 public static class Extensions
 {
 public static List<T> Append<T>(this List<T> a, List<T> b)
 {
 var newList = new List<T>(a);
 newList.AddRange(b);
 return newList;
 }
 public static bool Compare(this Customer customer1, Customer customer2)
 {
 if (customer1.CustomerID == customer2.CustomerID &&
 customer1.Name == customer2.Name &&
 customer1.City == customer2.City)
 {
 return true;
 }
 return false;
 }
 }
 public class Customer
 {
 public int CustomerID { get; private set; }
 public string Name { get; set; }
 public string City { get; set; }
 public Customer(int ID)
 {
 CustomerID = ID;
 }
 public override string ToString()
 {
 return Name + "\t" + City + "\t" + CustomerID;
 }
 }



class Program
 {
 static void Main(string[] args)
 {
 var customers = CreateCustomers();
 foreach (var c in FindCustomersByCity(customers, "London"))
 Console.WriteLine(c);
 }
 public static List<Customer> FindCustomersByCity(
 List<Customer> customers,
 string city)
 {
 return customers.FindAll(c => c.City == city);
 }
 static List<Customer> CreateCustomers()
 {
 return new List<Customer>
 {
 new Customer(1) { Name = "Maria Anders", City = "Berlin" },
 new Customer(2) { Name = "Laurence Lebihan", City = "Marseille" },
 new Customer(3) { Name = "Elizabeth Brown", City = "London" },
 new Customer(4) { Name = "Ann Devon", City = "London" },
 new Customer(5) { Name = "Paolo Accorti", City = "Torino" },
 new Customer(6) { Name = "Fran Wilson", City = "Portland" },
 new Customer(7) { Name = "Simon Crowther", City = "London" },
 new Customer(8) { Name = "Liz Nixon", City = "Portland" }
 };
 } 
 }
}



```


Understanding Queries and Query Expressions

```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
namespace NewLanguageFeatures
{
 public static class Extensions
 {
 public static List<T> Append<T>(this List<T> a, List<T> b)
 {
 var newList = new List<T>(a);
 newList.AddRange(b);
 return newList;
 }
 public static bool Compare(this Customer customer1, Customer customer2)
 {
 if (customer1.CustomerID == customer2.CustomerID &&
 customer1.Name == customer2.Name &&
 customer1.City == customer2.City)
 {
 return true;
 }
 return false;
 }
 }
 public class Customer
 {
 public int CustomerID { get; private set; }
 public string Name { get; set; }
 public string City { get; set; }
 public Customer(int ID)
 {
 CustomerID = ID;
 }
 public override string ToString()
 {
 return Name + "\t" + City + "\t" + CustomerID;
 }
 }

class Program
 {
 static void Main(string[] args)
 {
 Func<int, int> addOne = n => n + 1;
 Console.WriteLine("Result: {0}", addOne(5));
 Expression<Func<int, int>> addOneExpression = n => n + 1;
 var addOneFunc = addOneExpression.Compile();
 Console.WriteLine("Result: {0}", addOneFunc(5));
 }
 public static List<Customer> FindCustomersByCity(
 List<Customer> customers,
 string city)
 {
 return customers.FindAll(c => c.City == city);
 }
 static List<Customer> CreateCustomers()
 {
 return new List<Customer>
 {
 new Customer(1) { Name = "Maria Anders", City = "Berlin"},
 new Customer(2) { Name = "Laurence Lebihan", City = "Marseille" },
 new Customer(3) { Name = "Elizabeth Brown", City = "London" },
 new Customer(4) { Name = "Ann Devon", City = "London" },
 new Customer(5) { Name = "Paolo Accorti", City = "Torino" },
 new Customer(6) { Name = "Fran Wilson", City = "Portland" },
 new Customer(7) { Name = "Simon Crowther", City = "London" },
 new Customer(8) { Name = "Liz Nixon", City = "Portland" }
 };
 } 
 }
}


```

Anonymous Types and Advanced Query Creation
```
using System;
using System.Collections.Generic;
using System.Linq;using System.Text;
using System.Linq.Expressions;
namespace NewLanguageFeatures
{
 public static class Extensions
 {
 public static List<T> Append<T>(this List<T> a, List<T> b)
 {
 var newList = new List<T>(a);
 newList.AddRange(b);
 return newList;
 }
 public static bool Compare(this Customer customer1, Customer customer2)
 {
 if (customer1.CustomerID == customer2.CustomerID &&
 customer1.Name == customer2.Name &&
 customer1.City == customer2.City)
 { return true; }
 return false;
 }
 }
 public class Store
 {
 public string Name { get; set; }
 public string City { get; set; }
 public override string ToString()
 {
 return Name + "\t" + City;
 }
 }
 public class Customer
 {
 public int CustomerID { get; private set; }
 public string Name { get; set; }
 public string City { get; set; }
 public Customer(int ID) { CustomerID = ID; }
 public override string ToString()
 { return Name + "\t" + City + "\t" + CustomerID; }
 }


class Program
 {
 static void Query()
 {
 var stores = CreateStores();
 var numLondon = stores.Count(s => s.City == "London");
 Console.WriteLine("There are {0} stores in London. ", numLondon);
 }
 static void Main(string[] args)
 {
 Query();
 }
 public static List<Customer> FindCustomersByCity(
 List<Customer> customers,
 string city)
 {
 return customers.FindAll(c => c.City == city);
 }
 static List<Store> CreateStores()
 {
 return new List<Store>
 {
 new Store { Name = "Jim’s Hardware", City = "Berlin"},
 new Store { Name = "John’s Books", City = "London"},
 new Store { Name = "Lisa’s Flowers", City = "Torino"},
 new Store { Name = "Dana’s Hardware", City = "London"},
 new Store { Name = "Tim’s Pets", City = "Portland"},
 new Store { Name = "Scott’s Books", City = "London"},
 new Store { Name = "Paula’s Cafe", City = "Marseille"}
 };
 } 
 static List<Customer> CreateCustomers()
 {
 return new List<Customer>
 {
 new Customer(1) { Name = "Maria Anders", City = "Berlin"},
 new Customer(2) { Name = "Laurence Lebihan",City = "Marseille"},
 new Customer(3) { Name = "Elizabeth Brown", City = "London"},
 new Customer(4) { Name = "Ann Devon", City = "London"},
 new Customer(5) { Name = "Paolo Accorti", City = "Torino"},
 new Customer(6) { Name = "Fran Wilson", City = "Portland"},
 new Customer(7) { Name = "Simon Crowther", City = "London"},
 new Customer(8) { Name = "Liz Nixon",City = "Portland" }
 };
 } 
 }

}


```