# Serialization

NET serialization is the process of converting an object's state into a binary format that can be stored, transmitted, or persisted, and then restoring the object's state back from the binary format. It is a fundamental technique used in .NET for data storage, inter-process communication, distributed computing, and object persistence.


Serialization plays a crucial role when objects need to be transmitted over a network, shared between different processes or applications, or saved to a file or a database. By converting objects into a serialized form, they can be easily transmitted or stored, and later deserialized to recreate the original object.

In .NET, serialization could be  achieved through  mechanisms: Binary Serialization and XML Serialization and JSON Serialization.

### Binary Serialization

Binary serialization in .NET involves converting objects into a binary format using the BinaryFormatter class. This format is compact and optimized for performance. The BinaryFormatter serializes an object graph by traversing its members and writing their values to a binary stream.

```
[Serializable]
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}

// Serialization
Person person = new Person { Name = "John Doe", Age = 30 };
using (FileStream stream = new FileStream("person.bin", FileMode.Create))
{
    BinaryFormatter formatter = new BinaryFormatter();
    formatter.Serialize(stream, person);
}

// Deserialization
using (FileStream stream = new FileStream("person.bin", FileMode.Open))
{
    BinaryFormatter formatter = new BinaryFormatter();
    Person deserializedPerson = (Person)formatter.Deserialize(stream);
    Console.WriteLine($"Name: {deserializedPerson.Name}, Age: {deserializedPerson.Age}");
}
```

### XML Serialization
XML serialization in .NET involves converting objects into an XML-based format using the XmlSerializer class. The XmlSerializer serializes an object graph by representing its properties, fields, and values as XML elements and attributes.

```
[Serializable]
public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
}
// Serialization
Book book = new Book { Title = "The Catcher in the Rye", Author = "J.D. Salinger" };
using (FileStream stream = new FileStream("book.xml", FileMode.Create))
{
    XmlSerializer serializer = new XmlSerializer(typeof(Book));
    serializer.Serialize(stream, book);
}
// Deserialization
using (FileStream stream = new FileStream("book.xml", FileMode.Open))
{
    XmlSerializer serializer = new XmlSerializer(typeof(Book));
    Book deserializedBook = (Book)serializer.Deserialize(stream);
    Console.WriteLine($"Title: {deserializedBook.Title}, Author: {deserializedBook.Author}");
}
```

### JSON Serialization

JSON data is a common format these days when passing data between applications. When building a .NET application, JSON data format conversion to .NET objects and vice versa is very common. Serialization is the process of converting .NET objects, such as strings, into a JSON format, and deserialization is the process of converting JSON data into .NET objects. The JsonSerializer serializes an object graph by representing its properties, fields, and values as JSON Object.

#### What is JSON?
JSON (JavaScript Object Notation) is a lightweight data interchange format. JSON is a text format that is completely language-independent. It is easy for humans to read and write and for machines to parse and generate. 

JSON supports the following two data structures,

Collection of name/value pairs - Different programming languages support this Data Structure.
Ordered list of values - includes an array, list, vector or sequence, etc. 

1.Object
An unordered "name/value" assembly. An object begins with "{" and ends with "}." Behind each "name," there is a colon. And comma is used to separate much "name/value." For example,
```
var user = {"name":"Shiv","gender":"Male","birthday":"2000-8-9"}
```

2. C# Array
Value order set. An array begins with "[" and ends with "]." And values are separated with commas. For example,
```
var userlist = [
    {"user":{"name":"Shiv","gender":"Male","birthday":"2000-8-9"}},
    {"user":{"name":"Mohapatra","Male":"Female","birthday":"2004-6-7"}
    }
]
```

Let us write RepositoryManager class with implementation of Serialization/DeSerialization process for 
Employee Collection objects/


```
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class RepositoryManager{

    public void Serialize(List<Employee> employees,string fileName){
        var options=new JsonSerializerOptions {IncludeFields=true};
        var employeesJson=JsonSerializer.Serialize<List<Employee>>(employees,options);
        File.WriteAllText(fileName,employeesJson);
       }

    public List<Employee> DeSerialize(string fileName){
            string jsonString = File.ReadAllText(fileName);
            List<Employee> jsonEmployees = JsonSerializer.Deserialize<List<Employee>>(jsonString);
            foreach( Employee emp in jsonEmployees)
            {
                Console.WriteLine($"{emp.Id} : {emp .Name}");   
            }     
        return jsonEmployees;
    }
}
```