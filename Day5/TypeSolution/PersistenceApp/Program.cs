using Catalog;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

Product p1=new Product{ Id=12, Title="Gerbera", Description="Wedding flower", UnitPrice=12};
Product p2=new Product{ Id=13, Title="Rose", Description="Valentine flower", UnitPrice=34};
Product p3=new Product{ Id=14, Title="Lotus", Description="Worhip flower", UnitPrice=28};
Product p4=new Product{ Id=15, Title="Jasmine", Description="Smelling flower", UnitPrice=2};

List<Product> flowers=new List<Product>();

 flowers.Add(p1);
 flowers.Add(p2);
 flowers.Add(p3);
 flowers.Add(p4);

 try{
            // dynamic data type variable
            var options=new JsonSerializerOptions {IncludeFields=true};
            var produtsJson=JsonSerializer.Serialize<List<Product>>(flowers,options);
            string fileName=@"d:\ravi\products.json";
            //Serialize all Flowers into json file

            File.WriteAllText(fileName,produtsJson);
            //Deserialize from JSON file
            string jsonString = File.ReadAllText(fileName);
            List<Product> jsonFlowers = JsonSerializer.Deserialize<List<Product>>(jsonString);
            Console.WriteLine("\n JSON :Deserializing data from json file\n \n");
            foreach( Product flower in jsonFlowers)
            {
                Console.WriteLine($"{flower.Title} : {flower.Description}");   
            }   
    }
   catch(Exception exp){
    
    }
    finally{ }
