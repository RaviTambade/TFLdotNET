using Catalog;
using OrderProcessing;
using Utility;
Product product = new Product("Laptop", 1200, "Dell Laptop", "dell.jpg", "Electronics", "1");
ProductManager productManager = new ProductManager();
productManager.AddProduct(product);

Product product2 = new Product("Mobile", 800, "Samsung Mobile", "samsung.jpg", "Electronics", "2");
productManager.AddProduct(product2);

Product product3 = new Product("Shirt", 35, "Blue Shirt", "shirt.jpg", "Fashion", "3");
productManager.AddProduct(product3);

List<Product> products = productManager.Products;
foreach (var p in products)
{
    Console.WriteLine($"Name: {p.Name}, Price: {p.Price}, Category: {p.Category}");
}

//Logic for order placement

Order order = new Order(1);
order.AddItem(new Item(1, "Laptop"));
order.AddItem(new Item(2, "Mobile"));
order.AddItem(new Item(3, "Shirt"));
order.PrintOrder();


 MathEngine engine = new MathEngine();
    int sum = engine.Add(10, 20);
    int difference = engine.Subtract(20, 10);
    Console.WriteLine($"Sum: {sum}, Difference: {difference}");

    //int result1 = UtilityManager.Multiply(10, 20);
    //int result2 = UtilityManager.Divide(20, 10);
   // Console.WriteLine($"Multiplication: {result1}, Division: {result2}");

   int multiplyResult= engine.Multiply(10, 20);  
    Console.WriteLine($"Multiplication: {multiplyResult}");

    HRManager mgr=new HRManager();
    mgr.AddEmployee();
    mgr.ProcessSalary();

    Console.WriteLine("Hello, World!");
 

