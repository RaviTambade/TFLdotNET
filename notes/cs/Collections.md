# NET Collection Framework

A collection is a set of related objects. Unlike arrays, a collection can grow and shrink dynamically as the number of objects added or deleted. A collection is a class, so you must declare a new collection before you can add elements to that collection.

Many applications need the creation and management of groups of linked items. Items may be grouped in two ways: by generating arrays of objects or collections of objects. In terms of Data Structures, collections mimic the Array Data Structures; the main difference is that, unlike arrays, collections do not require a minimum size to be specified.

C# collections are made to more effectively organize, store, and modify comparable data. Adding, deleting, discovering, and inserting data into the collection are all examples of data manipulation. These classes support stacks, queues, lists, and hash tables. Most collection classes implement the same interfaces.

### Arrays

Like other programming languages, array in C# is a group of similar types of elements that have contiguous memory location. In C#, array is an object of base type System.Array. In C#, array index starts from 0. We can store only fixed set of elements in C# array.

#### Advantages of Arrays
- Code Optimization (less code)
- Random Access
- Easy to traverse data
- Easy to manipulate data
- Easy to sort data etc.



#### C# Array Types
There are 3 types of arrays in C# programming:
- Single Dimensional Array
- Multidimensional Array
- Jagged Array
 
### C# Single Dimensional Array
To create single dimensional array, you need to use square brackets [] after the type.

```
int[] intArray= new int[5] { 22,11,33,44,55 };

foreach (int i in intArray )
{
     Console.WriteLine( “\t {0}”, i);
}

Array.Sort(intArray);
Array.Reverse(intArray);

int[] arr = new int[5];//creating array  
int[] arr2 = new int[]{ 10, 20, 30, 40, 50 };  

foreach (int i in arr2)  
{  
    Console.WriteLine(i);  
}  
```

## Multidimensional Arrays

The multidimensional array is also known as rectangular arrays in C#. It can be two dimensional or three dimensional. The data is stored in tabular form (row * column) which is also known as matrix.

```
int [ , ]  mtrx = new int [2, 3];

//Can initialize declaratively
int [ , ] mtrx = new int [2, 3] { {10, 20, 30}, {40, 50, 60} }

//traversal  
for(int i=0;i<3;i++){  
    for(int j=0;j<3;j++){  
        Console.Write(mtrx[i,j]+" ");  
    }  
    Console.WriteLine();//new line at each row  
}  
```

#### Jagged Arrays
In C#, jagged array is also known as "array of arrays" because its elements are arrays. The element size of jagged array can be different.

```

int [ ]  [ ]  mtrxj = new  int [2] [];

//Initialization of Jagged array upon Declaration

int[][] arr = new int[3][]{  
          new int[] { 11, 21, 56, 78 },  
          new int[] { 2, 5, 6, 7, 98, 5 },  
          new int[] { 2, 5 }  
          };  

// Traverse array elements  
  for (int i = 0; i < arr.Length; i++)  
  {  
      for (int j = 0; j < arr[i].Length; j++)  
      {  
          System.Console.Write(arr[i][j]+" ");  
      }  
      System.Console.WriteLine();  
  }  
```




### Indexers (smart Array)
```
public class Books
{
 private string [] titles= new string [100];
 
 public string this [int index]
  {
      get{ 
          if (index <0 || index >=100)
            return 0;
          else
            return titles [index];
      }

    set{
        if (! index <0 || index >=100)
            return 0;
          else
            titles [index] =value;
        }
   }
}

public static void Main ()
{ 
  Books mybooks=new Books ();
  Mybooks [3] =”Mogali in Jungle”;
}
```




## Generic Collections

A Generic collection provides the type safety without derivation from a basic collection type and the implementation of type-specific members. The Generic Collection classes are found in the namespace "System.Collections.Generics." Internally, Generic Collections store elements in arrays of their respective types.

### List:
In Generic List, we have to specify a data type to its contents, and all elements will have the same datatype.
```
using System;
using System.Collections.Generic;
namespace genericList
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> GenericList = new List<int>();
            GenericList.Add(30);
            GenericList.Add(60);
            GenericList.Add(90);
            GenericList.Add(120);
            foreach (int x in GenericList)
            {
                Console.WriteLine(x);
            }
        }
    }
}
```

### Dictionary:
Dictionaries usually store data in key-value pairs, and we have to specify both data types beforehand.

```
using System;
using System.Collections.Generic;
namespace TFL
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, string> foodItems = new Dictionary<int, string>();
            foodItems.Add(1, "Soda");
            foodItems.Add(2, "Burger");
            foodItems.Add(3, "Fries");=
            foodItems.Add(4, "Onion Rings");

            foreach (KeyValuePair<int, string> item in foodItems)
            {
                Console.WriteLine(item.Key + " " + item.Value);
            }  
        }
    }
}
```

### Sorted List:
A sorted list also stores a key-value pair and automatically sorts its elements in ascending order based on their keys. In the generic Sorted list, we have to specify the datatypes of its content beforehand.

```
using System;
using System.Collections.Generic;
namespace TFL
{
    class Program

    {
        static void Main(string[] args)
        {

            SortedList<string, string> list = new SortedList<string, string>();
            list.Add("American", "Burger");
            list.Add("Lime", "Soda");
            list.Add("French", "Fries");
            list.Add("Onion", "Rings");

            foreach (KeyValuePair<string, string> item in list)
            {
                Console.WriteLine(item.Key + " " + item.Value);
            }
        }
    }
}
```


### Stack:
Values are kept in Stack using LIFO (Last In First Out). It offers the Push() and Pop() & Peek() methods to add and retrieve values, respectively. In generic Stack, we have to specify the datatypes of its content beforehand.

```
using System;
using System.Collections.Generic;
namespace TFL
{
    class Program
    {
        static void Main(string[] args)
        {
            Stack<string> steak = new Stack<string>();
            steak.Push("Karan");
            steak.Push("Sameer");=
            steak.Push("Manoj");
            steak.Push("Santosh");

            foreach (string s in steak)
            {
                Console.WriteLine(s);
            }
        }
    }
}
```

### Queue:  
Values are kept in a queue in a FIFO fashion (First In, First Out). The sequence in which the values were inserted is preserved. It offers the Enqueue() and Dequeue() methods to add and remove values from the collection. In the generic queue, we have to specify the datatypes of its content beforehand.

```
using System;
using System.Collections.Generic;
namespace TFL
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<string> queue = new Queue<string>();
            queue.Enqueue("Rajiv");
            queue.Enqueue("Shailesh");
            queue.Enqueue("Ram");
            queue.Enqueue("Reena");
            foreach (string s in queue)
            {
                Console.WriteLine(s);
            }
        }
    }
}

```

### Benefits of Collections in C#

There are many benefits of Collections in C#.
- Generic collections work faster than non-generic ones and decrease exceptions by revealing compile-time faults.
- Non-generic collection types are in "System. Collections," while generic types are in "System.Collections.Generic."
- C# also has several specialized collections tuned to deal with a specific data type, which we can find in the "System.Collections.Specialized" namespace.
- The Collection class supports null as a valid reference type value and enables redundant elements.