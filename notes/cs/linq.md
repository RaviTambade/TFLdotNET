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
- <b> Readability and Maintainability</b>: When the priority is to have code that is easy to read and maintain. Query Syntax often leads to cleaner filtering, sorting, and selection logic separation.


### Basic Ways to Write LINQ Queries
There are two basic ways to write LINQ queries:

1. Query Syntax
2. Method Syntax


#### Query Syntax

```
static IQueryable<Student> GetStudentsFromDb()
{
    return new[] {  
        new Student() { StudentID = 1, StudentName = "John Nigel", Mark = 73, City ="NYC"} ,
        new Student() { StudentID = 2, StudentName = "Alex Roma",  Mark = 51 , City ="CA"} ,
        new Student() { StudentID = 3, StudentName = "Noha Shamil",  Mark = 88 , City ="CA"} ,
        new Student() { StudentID = 4, StudentName = "James Palatte" , Mark = 60, City ="NYC"} ,
        new Student() { StudentID = 5, StudentName = "Ron Jenova" , Mark = 85 , City ="NYC"} 
    }.AsQueryable();
}


//Query data 

var studentList = GetStudentsFromDb();
var highPerformingStudents = from student in studentList
    where student.Mark > 80
    select student;

```

####  Method Syntax
Method syntax use extension methods provided in the Enumerable and Queryable classes.

To see that syntax in action, let’s create another query:

```
    var highPerformingStudents = studentList.Where(s => s.Mark > 80);
```

In this example, we are using the Where() extension method and provide a lambda expression s => s.Mark > 80 as an argument.


#### Lambda Expressions With LINQ

In LINQ, we use lambda expressions in a convenient way to define anonymous functions. It is possible to pass a lambda expression as a variable or as a parameter to a method call. However, in many LINQ methods, lambda expressions are used as a parameter. As a result, it makes the syntax short, and precise. Its scope is limited to where it is used as an expression. Therefore, we are not able to reuse it afterward.

```
    var firstStudent = studentList.Select(x => x.StudentName);
```


#### Frequently Used LINQ Methods 

##### Sorting: OrderBy, OrderByDecending
We can use the OrderBy() method to sort a collection in ascending order based on the selected property:

```
var selectStudentsWithOrderById = studentList.OrderBy(x => x.StudentID);
```

Similar to OrderBy() method, the OrderByDescending() method sorts the collection using the StudentID property in descending order:

```
var selectStudentsWithOrderByDescendingId = studentList.OrderByDescending(x => x.StudentID);
```

##### Projection: Select
We use the Select method to project each element of a sequence into a new form:

```
var studentsIdentified = studentList.Where(c => c.StudentName == name)
    .Select(stu => new Student {StudentName = stu.StudentName , Mark = stu.Mark});

```
Here, we filter only the students with the required name and then use the projection Select method to return students with only StudentName and Mark properties populated. This way, we can easily extract only the required information from our objects.


#### Grouping: GroupBy
We can use the GroupBy() method to group elements based on the specified key selector function. In this example, we are using City:

```
    var studentListGroupByCity = studentList.GroupBy(x => x.City);
```

One thing to mention. All the previous methods (Where, OrderBy, OrderByDescending, Select, GroupBy) return collection as a result. So, in order to use all the data inside the collection, we have to iterate over it

#### All, Any, Contains
We can use All() to determine whether all elements of a sequence satisfy a condition:

```
    var hasAllStudentsPassed = studentList.All(x => x.Mark > 50);
```
Similarly, we can use Any() to determine if any element of a sequence exists or satisfies a condition:

```
    var hasAnyStudentGotDistinction = studentList.Any(x => x.Mark > 86);
```

The Contains() method determines whether a sequence or a collection contains a specified element:

```
    var studentContainsId = studentList.Contains(new Student { StudentName = "Noha Shamil"}, new StudentNameComparer());
```
##### Partitioning: Skip, Take
Skip() will bypass a specified number of elements in a sequence and return the remaining elements:

```
    var skipStuentsUptoIndexTwo = studentList.Skip(2);
```

Take() will return a specified number of elements from the first element in a sequence:

```
    var takeStudentsUptoIndexTwo = studentList.Take(2);
```


#### Aggregation: Count, Max, Min, Sum, Average
Applying the Sum() method on the property Mark will give the summation of all marks:

```
    var sumOfMarks = studentList.Sum(x => x.Mark);
    var countOfStudents = studentList.Count(x => x.Mark > 65);
    var maxMarks = studentList.Max(x => x.Mark);
    var minMarks = studentList.Min(x => x.Mark);
    var avgMarks = studentList.Average(x => x.Mark);
```

## Advantages of Using LINQ
- Improves code readability
- Provides compile-time object type-checking
- Provides IntelliSense support for generic collection
- LINQ queries can be reused
- Provides in-built methods to write less code and expedite development
- Provides common query syntax for various data sources

## Disadvantages of Using LINQ
- Difficult to write complex queries as SQL
- Performance degradation if queries are not written accurately
- Require to recompile, and redeploy every time a change is made to the query
- Doesn’t take full advantage of SQL features such as cached execution plan for stored procedure.