# Serialization

NET serialization is the process of converting an object's state into a binary format that can be stored, transmitted, or persisted, and then restoring the object's state back from the binary format. It is a fundamental technique used in .NET for data storage, inter-process communication, distributed computing, and object persistence.


Serialization plays a crucial role when objects need to be transmitted over a network, shared between different processes or applications, or saved to a file or a database. By converting objects into a serialized form, they can be easily transmitted or stored, and later deserialized to recreate the original object.

In .NET, serialization is primarily achieved through two mechanisms: Binary Serialization and XML Serialization. Let's explore each of them in detail:

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
JSONSerializer class. The XmlSerializer serializes an object graph by representing its properties, fields, and values as XML elements and attributes.

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