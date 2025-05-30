# **Generics: A Story of Type Safety, Power, and Reusability**
> *“When I first started coding in C#, I used collections like `ArrayList`, but it always felt risky — like walking a tightrope without a safety net. I could add any type, and mistakes would show up at runtime, not while I was writing the code. That’s when I discovered **Generics**, and everything changed."*
>
> *Let me walk you through this beautiful journey of **Generics** — one of the most empowering features in C#. Just like how a carpenter uses the right tool for each task, Generics allow us to **write tools that work with any material — safely and efficiently**.*



## 📦 1. **List<T>: The New Age Array**

> *Imagine you're managing a list of months — earlier you'd use `ArrayList`, but then you realize it's not type-safe. What if someone accidentally added a number instead of a string?*

Let’s see the modern approach:

```csharp
List<string> months = new List<string>();
months.Add("January");
months.Add("February");
months.Add("April");
months.Add("May");
months.Add("June");

// Oops! Missed March.
months.Insert(2, "March");

// Display all months
foreach (string mon in months)
    Console.WriteLine(mon);
```

> *See how clean that looks? And C# won’t let you add an `int` by mistake — safety built right in.*


## 👨‍💼 2. **Working with User-Defined Types**

> *Now let’s imagine you're a team lead managing employees — their data, not just names. You'd want to sort them by name length. Let’s define an `Employee` class and compare them.*

```csharp
class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Employee(int eid, string ename)
    {
        Id = eid;
        Name = ename;
    }
}
```

And the comparer:

```csharp
class EmpComparer : IComparer<Employee>
{
    public int Compare(Employee e1, Employee e2)
    {
        return e1.Name.Length.CompareTo(e2.Name.Length);
    }
}
```

Now put it into action:

```csharp
List<Employee> list1 = new List<Employee>
{
    new Employee(1, "Raghu"),
    new Employee(2, "Seeta"),
    new Employee(4, "Leela")
};

list1.Sort(new EmpComparer());

foreach (Employee e in list1)
    Console.WriteLine($"{e.Id} --------- {e.Name}");
```

> *We’re not just storing data; we’re organizing it smartly.*

## 📚 3. **Stack<T> – Last In, First Out**

> *Think of a stack of plates — the last one you placed is the first you take out.*

```csharp
Stack<int> numStack = new Stack<int>();
numStack.Push(23);
numStack.Push(34);
numStack.Push(65);

Console.WriteLine("Element removed: {0}", numStack.Pop());
```

## 🎫 4. **Queue<T> – First In, First Out**

> *Like people standing in a queue at a ticket counter — first come, first served.*

```csharp
Queue<string> q = new Queue<string>();
q.Enqueue("Message1");
q.Enqueue("Message2");
q.Enqueue("Message3");

Console.WriteLine("First message: {0}", q.Dequeue());
Console.WriteLine("Next in line: {0}", q.Peek());
```

## 🔗 5. **LinkedList<T> – Flexible Insertion**

> *A LinkedList is like a train — each bogie (node) is connected to the next and the previous.*

```csharp
LinkedList<string> l1 = new LinkedList<string>();
l1.AddFirst("Apple");
l1.AddFirst("Papaya");
l1.AddFirst("Orange");
l1.AddFirst("Banana");

LinkedListNode<string> node = l1.First;
Console.WriteLine(node.Value);         // Banana
Console.WriteLine(node.Next.Value);    // Orange
```

## 🧾 6. **Dictionary\<TKey, TValue> – Key-Value Power**

> *Imagine a phonebook — you look up a name by a number. Dictionary makes this lightning fast.*

```csharp
Dictionary<int, string> phones = new Dictionary<int, string>();
phones.Add(1, "James");
phones.Add(35, "Rita");
phones.Add(16, "Meenal");

// Update a name
phones[16] = "Aishwarya";

// Safe add
if (!phones.ContainsKey(4))
    phones.Add(4, "Tim");

Console.WriteLine("Name is {0}", phones[4]);
```

## 🔁 7. **Custom Generic Method – Swap Anything!**

> *One of my favorite moments is showing students this: one method to swap anything — int, string, bool...*

```csharp
static void Swap<T>(ref T a, ref T b)
{
    Console.WriteLine("Swapping type: {0}", typeof(T));
    T temp = a;
    a = b;
    b = temp;
}
```

And in `Main()`:

```csharp
int x = 10, y = 20;
Swap(ref x, ref y);

string s1 = "Hello", s2 = "World";
Swap(ref s1, ref s2);

bool b1 = true, b2 = false;
Swap(ref b1, ref b2);
```

> *Pure magic. One generic method, infinite use cases.*

## 📐 8. **Generic Struct – Flexible Point System**

> *You’re building a game. You want coordinates. Sometimes integers, sometimes floating-points. Enter generic struct.*

```csharp
public struct Point<T>
{
    public T X { get; set; }
    public T Y { get; set; }

    public Point(T x, T y)
    {
        X = x;
        Y = y;
    }

    public void ResetPoint()
    {
        X = default(T);
        Y = default(T);
    }

    public override string ToString() => $"[{X}, {Y}]";
}
```

Usage:

```csharp
Point<int> p = new Point<int>(10, 20);
Console.WriteLine(p); // [10, 20]

Point<double> p2 = new Point<double>(5.5, 3.3);
Console.WriteLine(p2); // [5.5, 3.3]
```

## 🚗 9. **Custom Generic Collection – Garage for Cars**

> *Let’s say you're creating a garage — but not for just any vehicles, only for `Car` and its child types.*

```csharp
public class Car
{
    public string PetName;
    public int Speed;

    public Car(string name, int speed)
    {
        PetName = name;
        Speed = speed;
    }

    public Car() { }
}
```

Two derived classes:

```csharp
public class SportsCar : Car
{
    public SportsCar(string name, int speed) : base(name, speed) { }
}

public class MiniVan : Car
{
    public MiniVan(string name, int speed) : base(name, speed) { }
}
```

Now the **Generic Collection**:

```csharp
public class CarCollection<T> : IEnumerable<T> where T : Car
{
    private List<T> cars = new List<T>();

    public void AddCar(T car) => cars.Add(car);
    public void PrintPetName(int index) => Console.WriteLine(cars[index].PetName);

    public IEnumerator<T> GetEnumerator() => cars.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
```

Usage:

```csharp
CarCollection<Car> myGarage = new CarCollection<Car>();
myGarage.AddCar(new SportsCar("Ferrari", 200));
myGarage.AddCar(new MiniVan("Dodge", 100));

foreach (Car c in myGarage)
    Console.WriteLine($"{c.GetType().Name} - {c.PetName} @ {c.Speed} km/h");
```


## 🎓 Final Words from Mentor

> *Students, Generics are not just a syntax feature — they’re a **paradigm**. They make your code **reusable**, **type-safe**, and **elegant**. Whether you’re creating collections, utilities, or game engines — they serve as your Swiss army knife.*

> *Keep exploring. Play with types. Make mistakes. And ask yourself — **Can I make this more generic?** If yes, that’s the path to better code.*


