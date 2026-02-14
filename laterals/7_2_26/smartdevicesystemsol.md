
## 📘 **Problem Statement: Electronics Device Management System**

### 🏭 Background

A consumer electronics manufacturing company wants to build a **Device Management System** to manage information about:

* Electronic devices
* Device owners
* Manufacturing dates
* Product categories

The system should follow **Object-Oriented Programming principles** for better maintainability and scalability.


## 🎯 Objective

Design and implement a C# application that demonstrates:

* **Encapsulation** for securing device and customer data
* **Abstraction** for defining common product behavior
* **Inheritance** for electronic product hierarchy
* **Method Overriding (Polymorphism)**
* **Has-a Relationship (Composition)**
* **Static Members for shared data**


## 📋 System Requirements

### 1️⃣ Date Class (Manufacturing Date)

Create a class `ManufactureDate` that stores:

* Day
* Month
* Year

All variables must be **private**.

Provide:

* A parameterized constructor
* A method `ShowDate()` to display the date

👉 This class represents the **manufacturing date of devices**.


### 2️⃣ Owner Class (Device Owner)

Create a class `DeviceOwner` that represents the customer.

Attributes (Private):

* Name
* Age
* ManufactureDate object (Has-a relationship)

Static Attribute:

* Total number of owners

Constructors:

* Default constructor
* Parameterized constructor

Methods:

* Getter and Setter for Name
* `ShowDetails()` to display owner info
* Override `ToString()`
* Static method `GetTotalOwners()`

👉 Demonstrates **Encapsulation and Composition**.


### 3️⃣ Product Base Class (Abstract Product Concept)

Create a base class `Product` that stores:

* Product Name
* Price

Methods:

* Constructor to initialize values
* Virtual method `ShowDetails()`

👉 Represents a **generic electronic product**.



### 4️⃣ ElectronicsProduct Class (Derived Class)

Create a class `ElectronicsProduct` that inherits from `Product`.

Additional Attribute:

* Brand

Override:

* `ShowDetails()` method to include brand information

👉 Demonstrates **Inheritance and Polymorphism**.



### 5️⃣ Main Program

In `Main()` method:

1. Create multiple `DeviceOwner` objects
2. Display owner information
3. Modify owner name using setter
4. Retrieve name using getter
5. Call static method to get total owners
6. Create `ElectronicsProduct` objects
7. Display product details using polymorphism



## 🧪 Sample Operations

The program should be able to perform:

✔ Create owners with default and custom values
✔ Store manufacturing date inside owner object
✔ Modify private data through public methods
✔ Count total owners using static variable
✔ Display different product types dynamically
✔ Override base class methods



## 📌 Learning Outcomes

After completing this problem, students will understand:

| Concept        | Demonstrated Through           |
| -------------- | ------------------------------ |
| Encapsulation  | Private fields + Getter/Setter |
| Abstraction    | Base Product class             |
| Inheritance    | ElectronicsProduct → Product   |
| Polymorphism   | Overridden ShowDetails()       |
| Composition    | Owner has ManufactureDate      |
| Static Members | Owner Count                    |



## ⭐ Bonus Challenge (Optional)

Extend the system to include:

* Warranty class
* SmartDevice class (IoT devices)
* Interface `IServiceable`
* File-based data storage

 

## Let us **extend your Electronics Device Management System** step-by-step by adding:

✔ `Warranty` class
✔ `SmartDevice` (IoT) class
✔ `IServiceable` interface
✔ File-based data storage

All while keeping it **simple, practical, and student-friendly**.

 

# ✅ 1. IServiceable Interface (Service Behavior)

This interface defines **service-related behavior** for devices.

```csharp
public interface IServiceable
{
    void Service();
    bool IsUnderWarranty();
}
```

👉 Any device that can be serviced must implement this.

 

# ✅ 2. Warranty Class (Composition)

Stores warranty information.

```csharp
using System;

public class Warranty
{
    private DateTime startDate;
    private int durationInYears;

    public Warranty(DateTime startDate, int durationInYears)
    {
        this.startDate = startDate;
        this.durationInYears = durationInYears;
    }

    public bool IsValid()
    {
        DateTime expiryDate = startDate.AddYears(durationInYears);
        return DateTime.Now <= expiryDate;
    }

    public void ShowWarranty()
    {
        Console.WriteLine("Warranty Start: " + startDate.ToShortDateString());
        Console.WriteLine("Duration: " + durationInYears + " years");
        Console.WriteLine("Valid: " + IsValid());
    }
}
```

👉 Demonstrates **Has-a relationship**.


# ✅ 3. SmartDevice Class (IoT Device)

Extends `ElectronicsProduct` and implements `IServiceable`.

```csharp
public class SmartDevice : ElectronicsProduct, IServiceable
{
    private string ipAddress;
    private bool isConnected;
    private Warranty warranty;

    public SmartDevice(string name, double price, string brand,
                       string ipAddress, Warranty warranty)
        : base(name, price, brand)
    {
        this.ipAddress = ipAddress;
        this.warranty = warranty;
        this.isConnected = false;
    }

    public void Connect()
    {
        isConnected = true;
        Console.WriteLine("Device connected to network: " + ipAddress);
    }

    public void Disconnect()
    {
        isConnected = false;
        Console.WriteLine("Device disconnected.");
    }

    public override void ShowDetails()
    {
        base.ShowDetails();
        Console.WriteLine("IP Address: " + ipAddress);
        Console.WriteLine("Connected: " + isConnected);
    }

    // IServiceable Implementation

    public void Service()
    {
        Console.WriteLine("Smart Device is being serviced...");
    }

    public bool IsUnderWarranty()
    {
        return warranty.IsValid();
    }

    public void ShowWarrantyDetails()
    {
        warranty.ShowWarranty();
    }
}
```

👉 Demonstrates:

* Inheritance
* Interface
* Polymorphism
* Composition

 

# ✅ 4. File-Based Data Storage (Using System.IO)

We will save device details in a file.

### FileManager Class

```csharp
using System.IO;

public class FileManager
{
    private static string filePath = "devices.txt";

    public static void SaveDevice(string data)
    {
        File.AppendAllText(filePath, data + "\n");
    }

    public static void ReadDevices()
    {
        if (File.Exists(filePath))
        {
            string content = File.ReadAllText(filePath);
            Console.WriteLine("---- Stored Devices ----");
            Console.WriteLine(content);
        }
        else
        {
            Console.WriteLine("No device data found.");
        }
    }
}
```

👉 Demonstrates **File I/O**.

 

# ✅ 5. Updated Main Program (Testing Everything)

```csharp
using System;

public class Program
{
    public static void Main(string[] args)
    {
        // Create Warranty
        Warranty w1 = new Warranty(DateTime.Now.AddMonths(-6), 2);

        // Create Smart Device
        SmartDevice sd1 = new SmartDevice(
            "Smart AC",
            45000,
            "LG",
            "192.168.1.10",
            w1
        );

        // Use Smart Device
        sd1.ShowDetails();
        sd1.Connect();

        // Warranty Info
        sd1.ShowWarrantyDetails();

        // Service Check
        if (sd1.IsUnderWarranty())
        {
            sd1.Service();
        }
        else
        {
            Console.WriteLine("Warranty expired. Paid service required.");
        }

        // Save to File
        string data =
            "Device: Smart AC | Brand: LG | Price: 45000 | IP: 192.168.1.10";

        FileManager.SaveDevice(data);

        // Read from File
        FileManager.ReadDevices();
    }
}
```

  

# 📌 Architecture After Extension

```
Product
   ↑
ElectronicsProduct
   ↑
SmartDevice  ------> IServiceable
      |
      |
   Warranty


FileManager  ---> File Storage
```

 

# 🎯 Concepts Covered

| Feature           | Concept                    |
| ----------------- | -------------------------- |
| Warranty          | Composition                |
| IServiceable      | Interface                  |
| SmartDevice       | Inheritance + Polymorphism |
| FileManager       | File I/O                   |
| IsUnderWarranty() | Business Logic             |
| Service()         | Abstraction                |

 

# 🧠 Real-World Mapping

| Software     | Electronics Industry |
| ------------ | -------------------- |
| SmartDevice  | IoT Device           |
| IP Address   | Network Identity     |
| Warranty     | After-sales Support  |
| IServiceable | Maintenance System   |
| File Storage | Device Records       |
