## **Electronics Smart Device Management System**

 

## 🏫 Project Title

**Design and Development of Smart Electronics Device Management System using C#**


## 📌 Problem Statement

A consumer electronics company wants to build a **console-based application** to manage its smart devices, customers, and service records.

The system must:

* Store electronic product information
* Manage IoT-enabled smart devices
* Track warranty status
* Provide service functionality
* Save device records to files
* Follow Object-Oriented Programming principles

You are required to design and implement this system using **C# and .NET**.



## 🎯 Project Objectives

After completing this project, students will be able to:

✔ Apply OOP concepts in real systems
✔ Implement inheritance and interfaces
✔ Use file handling for persistence
✔ Design modular software
✔ Simulate industry-level device management
✔ Improve coding and documentation skills


## 🧩 Technologies Used

| Component | Technology              |
| --------- | ----------------------- |
| Language  | C#                      |
| Platform  | .NET Console App        |
| Storage   | Text File / JSON        |
| IDE       | Visual Studio / VS Code |



## 🏗️ System Modules

### 1️⃣ Product Module

* Base class `Product`
* Stores name and price
* Virtual display method



### 2️⃣ Electronics Module

* Derived class `ElectronicsProduct`
* Adds brand information
* Overrides display method



### 3️⃣ Smart Device Module

* Class `SmartDevice`
* Implements `IServiceable`
* Stores IP address
* Supports connect/disconnect


### 4️⃣ Warranty Module

* Class `Warranty`
* Manages warranty period
* Checks validity

---

### 5️⃣ Service Module

* Interface `IServiceable`
* Defines service behavior


### 6️⃣ Storage Module

* Class `FileManager`
* Saves and reads device data
* Uses file handling


## 📐 System Design (Class Structure)

```
Product
   ↓
ElectronicsProduct
   ↓
SmartDevice → IServiceable
      |
   Warranty

FileManager
```



## 🔧 Functional Requirements

The system must:

- ✔ Add new smart devices
- ✔ Display device information
- ✔ Connect devices to network
- ✔ Check warranty validity
- ✔ Perform servicing
- ✔ Save data to file
- ✔ Load data from file


## 🖥️ Sample Menu (Console UI)

```text
----------------------------------
 Smart Device Management System
----------------------------------
1. Add New Device
2. View All Devices
3. Connect Device
4. Service Device
5. Check Warranty
6. Save to File
7. Load from File
8. Exit
----------------------------------
Enter Choice:
```

## 🧪 Sample Workflow

- 1️⃣ User selects "Add Device"
- 2️⃣ Enters product details
- 3️⃣ System creates SmartDevice object
- 4️⃣ Warranty is attached
- 5️⃣ Device is stored in memory
- 6️⃣ Data is saved in file


## 📁 Suggested Folder Structure

```
SmartDeviceProject
│
├── Models
│   ├── Product.cs
│   ├── ElectronicsProduct.cs
│   ├── SmartDevice.cs
│   └── Warranty.cs
│
├── Interfaces
│   └── IServiceable.cs
│
├── Services
│   └── FileManager.cs
│
└── Program.cs
```

## 📝 Implementation Guidelines

* Use private data members
* Use constructors
* Apply inheritance properly
* Override methods
* Implement interface fully
* Handle file exceptions
* Follow naming conventions

## 📊 Evaluation Criteria

| Criteria      | Marks   |
| ------------- | ------- |
| OOP Design    | 20      |
| Functionality | 25      |
| File Handling | 15      |
| Code Quality  | 15      |
| Documentation | 15      |
| Presentation  | 10      |
| **Total**     | **100** |



## 🚀 Extension Tasks (Bonus)

(For Advanced Students)

* JSON Serialization
* Database Integration
* Cloud Sync
* REST API
* Mobile App Interface


## 📚 Learning Outcomes

After completing this project, students will be able to:

✅ Design real-world software systems
✅ Apply OOP in production-style code
✅ Handle device lifecycle
✅ Build scalable applications
✅ Work in teams


## 🧑‍🏫 Mentor Note (Your Style)

> *"Dear Student, this project is not just about writing code.
> It is about learning how real systems are built, maintained, and scaled.
> Write code that another engineer can understand."*

