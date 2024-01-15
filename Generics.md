

Generics
Are classes, structures, interfaces and methods that have placeholders (type parameters) for one or more of the types 
they store or use.


List<T> class
Represents a strongly typed list of objects that can be accessed by index.
Generic equivalent of ArrayList class.

```
List<string> months = new List <string> ();
months.Add(“January”);
months.Add(“February”);
months.Add(“April”);
months.Add(“May”);
months.Add(“June”);
foreach(string mon in months)
Console.writeLine(mon);
Months.Insert(2,”March”);

```

List of user defined objects

```

class Employee
{
int eid;//appropriate constructor and properties for Employee Entity
string ename;
}
class EmpComparer:IComparer<Employee>
{
public int Compare(Employee e1, Employee e2)
 { int ret = e1.Name.Length.CompareTo(e2.Name.Length); return ret;
 }
}
public static void Main ()
{
List<Employee>list1 = new List<Employee>();
List1.Add(new Employee(1, “Raghu”);
List1.Add(new Employee(2, “Seeta”);
List1.Add(new Employee(4, “Leela”);
EmpComparer ec = new EmpComparer();
List1.Sort(ec);
foreach(Employee e in list1)
Console.WriteLine(e.Id + “---------“+ e.Name);
}

```

Stack<T> class
```
Stack<int>numStack = new Stack<int>();
numStack.Push(23);
numStack.Push(34);
numStack.Push(65);
Console.WriteLine(“Element removed: “, numStack. Pop())

```

Queue<T> class

```
Queue<string> q = new Queue<string>();
q.Enqueue(“Message1”);
q.Enqueue(“Message2”);
q.Enqueue(“Message3”);
Console.WriteLine(“First message: {0}”, q.Dequeue());
Console.WriteLine(“The element at the head is {0}”, q.Peek());
IEnumerator<string> e= q.GetEnumerator();
while(e.MoveNext())
Console.WriteLine(e.Current);

```

LinkedList<T> class
Represents a doubly linked List
Each node is on the type LinkedListNode

```
LinkedList<string> l1= new LinkedList<string>();
L1.AddFirst(new LinkedListNode<string>(“Apple”));
L1.AddFirst(new LinkedListNode<string>(“Papaya”));
L1.AddFirst(new LinkedListNode<string>(“Orange”));
L1.AddFirst(new LinkedListNode<string>(“Banana”));
LinkedListNode<string> node=l1.First;
Console.WriteLine(node. Value);
Console.WriteLine(node.Next.Value);


```

Dictionary<K, V> class
Represents a collection of keys and values.
Keys cannot be duplicate

```
Dictionary<int, string> phones= new Dictionary<int, string>();
phones.Add(1, “James”);
phones.Add(35, “Rita”);
phones.Add(16, “Meenal”);
phones.Add(41, “jim”);
phones[16] = “Aishwarya”;
Console.WriteLine(“Name {0}”, phones [12]);
if (!phone.ContainsKey(4))
phones.Add(4,”Tim”);
Console.WriteLine(“Name is {0}”, phones [4]);

```

Custom Generic Types
Generic function

```
static void Main(string[] args)
 { // Swap 2 ints.
 int a = 10, b = 90;
 Console.WriteLine("Before swap: {0}, {1}", a, b);
 Swap<int>(ref a, ref b);
 Console.WriteLine("After swap: {0}, {1}", a, b);
 Console.WriteLine();
 // Swap 2 strings.
 string s1 = "Hello", s2 = "There";
 Console.WriteLine("Before swap: {0} {1}!", s1, s2);
 Swap<string>(ref s1, ref s2);
 Console.WriteLine("After swap: {0} {1}!", s1, s2);
 Console.WriteLine();
 
 // Compiler will infer System.Boolean.
 bool b1=true, b2=false;
 Console.WriteLine("Before swap: {0}, {1}", b1, b2);
 Swap (ref b1, ref b2);
 Console.WriteLine("After swap: {0}, {1}", b1, b2);
 Console.WriteLine();
 // Must supply type parameter if the method does not take params.
 DisplayBaseClass<int>();
 DisplayBaseClass<string>();
 }
 static void Swap<T>(ref T a, ref T b)
 {
 Console.WriteLine("You sent the Swap() method a {0}", typeof(T));
 T temp; temp = a; a = b; b = temp;
 }
static void DisplayBaseClass<T>()
 {
 Console.WriteLine("Base class of {0} is: {1}.", 
 typeof(T), typeof(T).BaseType);
 }

```

Custom Structure

```
// A generic Point structure. 
 public struct Point<T>
 {// Generic state date.
 private T xPos;
 private T yPos;
 
 // Generic constructor.
 public Point(T xVal, T yVal)
 { xPos = xVal; yPos = yVal; }
 
 // Generic properties. 
 public T X
 { get { return xPos; } set { xPos = value; } }
 public T Y
 { get { return yPos; } set { yPos = value; } }
 public override string ToString()
 { return string.Format("[{0}, {1}]", xPos, yPos); }
 // Reset fields to the default value of the type parameter.
 public void ResetPoint()
 { xPos = default(T); yPos = default(T); }
 }



 static void Main(string[] args)
 {
 // Point using ints.
 Point<int> p = new Point<int>(10, 10);
 Console.WriteLine("p.ToString()={0}", p.ToString());
 p.ResetPoint();
 Console.WriteLine("p.ToString()={0}", p.ToString());
 
 // Point using double.
 Point<double> p2 = new Point<double>(5.4, 3.3);
 Console.WriteLine("p2.ToString()={0}", p2.ToString());
 p2.ResetPoint();
 Console.WriteLine("p2.ToString()={0}", p2.ToString());
 }



```


Custom Generic collection clas

```
public class Car
 { public string PetName; public int Speed;
 public Car(string name, int currentSpeed)
 { PetName = name; Speed = currentSpeed; }
 public Car() { }
 }
public class SportsCar : Car
 { public SportsCar(string p, int s): base(p, s) { }
 // Assume additional SportsCar methods.
 }
public class MiniVan : Car
 { public MiniVan(string p, int s) : base(p, s) { }
 // Assume additional MiniVan methods.
 }
// Custom Generic Collection
 public class CarCollection<T> : IEnumerable<T> where T : Car
 { private List<T> arCars = new List<T>();
 public T GetCar(int pos) { return arCars[pos]; }
 public void AddCar(T c) { arCars.Add(c); }
 public void ClearCars() { arCars.Clear(); }
 public int Count { get { return arCars.Count; }

 // IEnumerable<T> extends IEnumerable, 
//therefore we need to implement both versions of GetEnumerator().
 IEnumerator<T> IEnumerable<T>.GetEnumerator()
 {return arCars.GetEnumerator(); }
 IEnumerator IEnumerable.GetEnumerator()
 { return arCars.GetEnumerator(); }
// This function will only work because of our applied constraint. 
public void PrintPetName(int pos)
{ Console.WriteLine(arCars[pos].PetName); }
}
static void Main(string[] args)
 { // Make a collection of Cars.
 CarCollection<Car> myCars = new CarCollection<Car>();
 myCars.AddCar(new Car("Alto", 20));
 myCars.AddCar(new Car("i20", 90));
 foreach (Car c in myCars)
 { Console.WriteLine("PetName: {0}, Speed: {1}",
 c.PetName, c.Speed);
 } 
// CarCollection<Car> can hold any type deriving from Car.
 CarCollection<Car> myAutos = new CarCollection<Car>();
 myAutos.AddCar(new MiniVan("Family Truckster", 55));
 myAutos.AddCar(new SportsCar("Crusher", 40));
 foreach (Car c in myAutos)
 { Console.WriteLine("Type: {0}, PetName: {1}, Speed: {2}",
 c.GetType().Name, c.PetName, c.Speed);
 }


 }

```