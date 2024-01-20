## LINQ : Language Integrated Query

Language-Integrated Query (LINQ) is a powerful set of technologies based on the integration of query capabilities directly into the C# language. LINQ Queries are the first-class language construct in C# .NET, just like classes, methods, events. The LINQ provides a consistent query experience to query objects (LINQ to Objects), relational databases (LINQ to SQL), and XML (LINQ to XML).

### Key Features of LINQ:
LINQ provides the following Key Features:

- <b>Uniform Query Syntax</b>: Whether you are querying an SQL database, an XML document, or an in-memory collection, the syntax is consistent and typically involves query operators like where, select, groupby, etc.
- <b>Consistency</b>: LINQ provides a consistent and uniform way to query different data sources, simplifying code and reducing the need to learn multiple query languages for different data types.
- <b>Improved Productivity</b>: LINQ simplifies common data operations like filtering, sorting, and grouping, reducing the amount of repetitive code that developers need to write.
- </b>Strongly Typed</b>: LINQ is strongly typed, which means the compiler checks the syntax against the types of objects being queried, thus minimizing runtime errors.
- <b>SQL-Like Syntax</b>: LINQ queries use a SQL-like syntax that is familiar to many developers, making it easier to express complex data retrieval and manipulation operations.

```

// Data source
string[] names = {"Bill", "Steve", "James", "Mohan" };

// LINQ Query 
var myLinqQuery = from name in names
                where name.Contains('a')
                select name;
    
// Query execution
foreach(var name in myLinqQuery)
    Console.Write(name + " ");

```

### Example Using LINQ Query Syntax in C#:
The following Example code is self-explained, so please go through the comment lines. Here, we have created a collection of integers, i.e., going to be our data source. Then, we created one LINQ query, which will fetch the numbers from the data source that are greater than 5, and finally, we executed the query and printed the result on the Console window. The LINQ query contains three things, i.e., Data Source, Condition, and Selection.

```
using System;
using System.Collections.Generic;
using System.Linq;
namespace LINQDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Step1: Data Source
            List<int> integerList = new List<int>()
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10
            };

            //Step2: Query
            //LINQ Query using Query Syntax to fetch all numbers which are > 5
            var QuerySyntax = from obj in integerList //Data Source
                              where obj > 5 //Condition
                              select obj; //Selection

            //Step3: Execution
            foreach (var item in QuerySyntax)
            {
                Console.Write(item + " ");
            }

            Console.ReadKey();
        }
    }
}
```

### Use LINQ Query Syntax:
Query Syntax is often more readable for queries that involve joining, grouping, or sorting operations. It is closer to the SQL style of queries and is great for developers already comfortable with SQL. The LINQ Query Syntax in C# is often preferred in certain scenarios due to its readability and similarity to SQL. It’s particularly advantageous in the following situations:

- <b>SQL-Like Queries</b>: For developers familiar with SQL, Query Syntax provides a more intuitive and familiar way to compose queries. Its declarative nature makes it easy to read and understand, especially for simple queries.
- <b>Complex Queries</b>: When dealing with complex queries that involve multiple operations like joins, grouping, and ordering, Query Syntax can be more readable and organized. The operations flow logically, similar to how SQL queries are structured.
- <b>Comprehension Syntax</b>: For those who prefer the comprehension syntax (from…where…select), which is more expressive and closer to natural language.
- <b>Grouping Operations</b>: When performing grouping operations, Query Syntax often results in more concise and readable code compared to Method Syntax.
- <b>Joining Operations</b>: Query Syntax can be more straightforward and less verbose for queries involving multiple joins, especially those that resemble relational database queries.
-<b> Readability and Maintainability</b>: When the priority is to have code that is easy to read and maintain. Query Syntax often leads to cleaner filtering, sorting, and selection logic separation.


### Working with Lambda Expressions (Arrow Function)
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


### Using Lambda Expressions to Create Expression Trees

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

    public static List<Customer> FindCustomersByCity( List<Customer> customers, string city)
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


### Understanding Queries and Query Expressions

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


    public static List<Customer> FindCustomersByCity(List<Customer> customers,string city)
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

### Anonymous Types and Advanced Query Creation
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

        public static List<Customer> FindCustomersByCity(List<Customer> customers, string city)
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