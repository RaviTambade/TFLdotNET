# The Story of C# Collections

## 🎙️ *Mentor Opens the Class...*

> *"Imagine you’re moving into a new house. You bring books, clothes, utensils — all kinds of things. You don’t just throw them into one big box. You organize them — in shelves, drawers, racks, and cabinets.*
>
> *In software development, **collections** are those smart storage units. They help us store, organize, and access data — dynamically and efficiently."*

Let me take you on a journey today — **from arrays to modern generic collections** — where you'll learn not only *how* to use them but *why* they exist in the first place.

## 🧺 What Is a Collection?

A **collection** in .NET is like a **container** that holds related items — but unlike arrays, collections can **grow** or **shrink** as needed.

> *"Think of an array like a tiffin box with fixed compartments. A collection is like a flexible lunchbox — you can add, remove, or resize compartments as your appetite changes."*

### So, Why Do Developers Love Collections?

* Because they allow **dynamic memory handling**
* You can **add or remove items easily**
* You don’t have to worry about the size ahead of time
* And — they come with built-in helpers for searching, sorting, indexing, and more!

## 🧱 Let’s Start with Arrays (The Building Blocks)

### 🎯 Single-Dimensional Arrays

> *"This is your old-school fixed list — like a tray with 5 cups. Once it’s made, you can’t increase or decrease it."*

```csharp

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }

    // Constructor
    public Product(int id, string name, double price)
    {
        Id = id;
        Name = name;
        Price = price;
    }
}



Product[] products = new Product[5]
{
    new Product(101, "Mouse", 500),
    new Product(102, "Keyboard", 800),
    new Product(103, "Monitor", 7000),
    new Product(104, "Laptop", 55000),
    new Product(105, "Pen Drive", 700)
};


```



Simple. Fast. But limited.


### 🧮 Multidimensional Arrays (2D Tables)

> *"Imagine a chessboard — rows and columns. That’s your 2D array. You define how big the table is and fill in the data."*

```csharp
int[,] mtrx = new int[2, 3] { {10, 20, 30}, {40, 50, 60} };
```

Used in mathematical operations, images, or spreadsheets.

### 🪢 Jagged Arrays (Array of Arrays)

> *"This is like a tiffin with uneven compartments — one row may have 2 items, another may have 5. That’s the flexibility of jagged arrays."*

```csharp
int[][] arr = new int[3][] {
  new int[] {11, 21, 56},
  new int[] {2, 5, 6, 7},
  new int[] {2, 5}
};

Product[][] products = new Product[3][]
{
    new Product[]
    {
        new Product(101, "Mouse", 500),
        new Product(102, "Keyboard", 800),
        new Product(103, "Monitor", 7000)
    },
    new Product[]
    {
        new Product(104, "Laptop", 55000),
        new Product(105, "Pen Drive", 700),
        new Product(106, "Webcam", 1500),
        new Product(107, "Speaker", 2000)
    },
    new Product[]
    {
        new Product(108, "USB Cable", 300),
        new Product(109, "Charger", 1200)
    }
};


using System;

public class Product
{
    public int Id;
    public string Name;
    public double Price;

    public Product(int id, string name, double price)
    {
        Id = id;
        Name = name;
        Price = price;
    }
}

class Program
{
    // Method to print all products from a jagged array
    static void PrintAllProducts(Product[][] products)
    {
        int rowNumber = 1;

        foreach (Product[] row in products)
        {
            Console.WriteLine($"--- Category {rowNumber} ---");
            foreach (Product p in row)
            {
                Console.WriteLine($"Id: {p.Id}, Name: {p.Name}, Price: ₹{p.Price}");
            }
            Console.WriteLine(); // Blank line between categories
            rowNumber++;
        }
    }

    static void Main()
    {
        // Jagged Array of Products
        Product[][] products = new Product[3][]
        {
            new Product[]
            {
                new Product(101, "Mouse", 500),
                new Product(102, "Keyboard", 800),
                new Product(103, "Monitor", 7000)
            },
            new Product[]
            {
                new Product(104, "Laptop", 55000),
                new Product(105, "Pen Drive", 700),
                new Product(106, "Webcam", 1500),
                new Product(107, "Speaker", 2000)
            },
            new Product[]
            {
                new Product(108, "USB Cable", 300),
                new Product(109, "Charger", 1200)
            }
        };

        // Call method to print all products
        PrintAllProducts(products);
    }
}




```

## 📦 Indexers — Smart Arrays with Custom Logic

> *"Suppose you have a bookshelf. But you don’t allow any book to be kept beyond a certain number. That’s what indexers do — they control access."*

```csharp
public string this[int index]
{
    get { return titles[index]; }
    set { titles[index] = value; }
}


```
Very useful when building custom containers.

Sure! Let me show you how to create a **Catalog class that contains a List of Products** and allows accessing products using an **indexer**—just like using an array (`catalog[0]`).

---

### ✅ Step 1: Product Class

```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }

    public Product(int id, string name, double price)
    {
        Id = id;
        Name = name;
        Price = price;
    }

    public override string ToString()
    {
        return $"{Id} - {Name} - ₹{Price}";
    }
}
```

---

### ✅ Step 2: Catalog Class with **Indexer**

```csharp
using System.Collections.Generic;

public class Catalog
{
    private List<Product> products = new List<Product>();

    // Indexer (like array access)
    public Product this[int index]
    {
        get 
        {
            if (index >= 0 && index < products.Count)
                return products[index];
            else
                throw new IndexOutOfRangeException("Invalid product index!");
        }
        set 
        {
            if (index >= 0 && index < products.Count)
                products[index] = value;
            else
                throw new IndexOutOfRangeException("Invalid product index!");
        }
    }

    // Add product to catalog
    public void AddProduct(Product p)
    {
        products.Add(p);
    }

    // Property to get number of products
    public int Count
    {
        get { return products.Count; }
    }
}
```

---

### ✅ Step 3: Use the Catalog and Indexer in Main()

```csharp
using System;

class Program
{
    static void Main()
    {
        Catalog catalog = new Catalog();

        // Adding products
        catalog.AddProduct(new Product(101, "Mouse", 500));
        catalog.AddProduct(new Product(102, "Keyboard", 800));
        catalog.AddProduct(new Product(103, "Monitor", 7000));

        // Access using indexer
        Console.WriteLine("First Product: " + catalog[0]);

        // Modify using indexer
        catalog[1] = new Product(202, "Mechanical Keyboard", 1500);

        Console.WriteLine("\nAll Products:");
        for (int i = 0; i < catalog.Count; i++)
        {
            Console.WriteLine(catalog[i]);
        }
    }
}
```

---

### ✅ Output:

```
First Product: 101 - Mouse - ₹500

All Products:
101 - Mouse - ₹500
202 - Mechanical Keyboard - ₹1500
103 - Monitor - ₹7000
```

---

Let us enhance the **Catalog class** by adding:

### ✅ What's Coming Next:

✔ **Overloaded indexer to access Product by Name (string key)**
✔ **Methods to search products by ID or Name**
✔ **Method to remove products**
✔ **Sort products by Price or Name**

---

### ✅ Enhanced Catalog Class with All Features

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }

    public Product(int id, string name, double price)
    {
        Id = id;
        Name = name;
        Price = price;
    }

    public override string ToString()
    {
        return $"{Id} - {Name} - ₹{Price}";
    }
}

public class Catalog
{
    private List<Product> products = new List<Product>();

    // ✅ Indexer using integer (like array)
    public Product this[int index]
    {
        get
        {
            if (index >= 0 && index < products.Count)
                return products[index];
            throw new IndexOutOfRangeException("Invalid index!");
        }
        set
        {
            if (index >= 0 && index < products.Count)
                products[index] = value;
            else
                throw new IndexOutOfRangeException("Invalid index!");
        }
    }

    // ✅ Overloaded Indexer using Product Name
    public Product this[string name]
    {
        get
        {
            return products.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }

    // ✅ Add product
    public void AddProduct(Product p)
    {
        products.Add(p);
    }

    // ✅ Remove product by ID
    public bool RemoveProduct(int id)
    {
        var product = products.FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            products.Remove(product);
            return true;
        }
        return false;
    }

    // ✅ Search by Name
    public Product SearchByName(string name)
    {
        return products.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    // ✅ Sorting
    public void SortByPrice(bool ascending = true)
    {
        products = ascending
            ? products.OrderBy(p => p.Price).ToList()
            : products.OrderByDescending(p => p.Price).ToList();
    }

    public void SortByName()
    {
        products = products.OrderBy(p => p.Name).ToList();
    }

    public int Count => products.Count;
}
```

---

### ✅ Usage in Main()

```csharp
class Program
{
    static void Main()
    {
        Catalog catalog = new Catalog();

        catalog.AddProduct(new Product(101, "Mouse", 500));
        catalog.AddProduct(new Product(102, "Keyboard", 800));
        catalog.AddProduct(new Product(103, "Monitor", 7000));
        catalog.AddProduct(new Product(104, "Laptop", 55000));

        // ✅ Using indexer by index
        Console.WriteLine(catalog[0]);

        // ✅ Using indexer by product name
        Console.WriteLine("Search by name (Monitor): " + catalog["Monitor"]);

        // ✅ Sort products by price descending
        catalog.SortByPrice(false);

        Console.WriteLine("\nProducts sorted by price (Desc):");
        for (int i = 0; i < catalog.Count; i++)
            Console.WriteLine(catalog[i]);

        // ✅ Remove a product
        catalog.RemoveProduct(102);
        Console.WriteLine("\nAfter removing Keyboard:");
        for (int i = 0; i < catalog.Count; i++)
            Console.WriteLine(catalog[i]);
    }
}
```

---

### ✅ Output:

```
101 - Mouse - ₹500
Search by name (Monitor): 103 - Monitor - ₹7000

Products sorted by price (Desc):
104 - Laptop - ₹55000
103 - Monitor - ₹7000
102 - Keyboard - ₹800
101 - Mouse - ₹500

After removing Keyboard:
104 - Laptop - ₹55000
103 - Monitor - ₹7000
101 - Mouse - ₹500
```

---



## 🧰 Time for the Modern Tools — **Generic Collections**

> *"Arrays are like regular drawers. But C# gives us **modular, adjustable, self-organizing shelves** through the generic collection framework."*

### ✅ Why Go Generic?

* Type-safe
* Faster at runtime
* No need for type conversions
* Reduce bugs at compile time

They live in `System.Collections.Generic`.

### 📋 List<T> — Dynamic Arrays

> *"This is your expandable list — like adding more seats to a table as guests arrive."*

```csharp
List<int> scores = new List<int> { 10, 20, 30 };
scores.Add(40);
```

### 📖 Dictionary\<TKey, TValue> — Key-Value Pair

> *"Think of it like a contact list. You don’t search by index, you search by name — the key."*

```csharp
Dictionary<int, string> foodItems = new Dictionary<int, string> {
    {1, "Soda"},
    {2, "Burger"}
};
```

### 🔢 SortedList\<TKey, TValue>

> *"Same as dictionary, but sorted automatically — like files arranged alphabetically."*

```csharp
SortedList<string, string> menu = new SortedList<string, string> {
    {"Lime", "Soda"},
    {"French", "Fries"}
};
```

## 🎯 Stack<T> — LIFO (Last In, First Out)

> *"A stack of trays in a canteen — the last one you place is the first one you remove."*

```csharp
Stack<string> names = new Stack<string>();
names.Push("Karan");
names.Pop();  // Removes "Karan"
```

## 🚶 Queue<T> — FIFO (First In, First Out)

> *"A queue at a railway station — first come, first served."*

```csharp
Queue<string> queue = new Queue<string>();
queue.Enqueue("Rajiv");
queue.Dequeue();  // Removes "Rajiv"
```

## 🧠 Mentor’s Tip: Specialized Collections Exist Too!

You’ll find more specialized collections in:

* `System.Collections.Specialized` (for name-value collections)
* `System.Collections.Concurrent` (for multi-threaded apps)

## 🌟 Summary: Why Collections Matter

> *"In life, you don’t store all your tools in one bag — you organize them by use: daily tools, gardening tools, emergency tools. Collections help you do that in code."*

### ✅ Benefits Recap:

* Faster and flexible
* Type-safe (generics)
* Built-in methods for manipulation
* Sorted, searchable, and scalable
* Tailored to real-world data needs


## 🧭 Where Do We Use These?

> *“Sir, why should we learn all this?”*
> Good question! You’ll need collections in almost **every project** — from a **shopping cart**, to **chat history**, to **user sessions**, to **caching layers**.
>
> *Learn them well — and your software will be organized, efficient, and smart.* ✅


Let’s keep learning, let’s keep growing 🌱
