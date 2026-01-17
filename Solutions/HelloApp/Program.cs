using catalog;

//Main Entry Point function
//Product usuage Implementation

ProductManager mgr=new ProductManager();
List<Product> retrivedProducts=mgr.GetAll();
retrivedProducts.Add(new Product{Id=4,Title="Product4",Description="Description4",Quantity=40,Price=400.0});
for(int i=0;i<retrivedProducts.Count;i++){
    Console.WriteLine("Title: "+ retrivedProducts[i].Title);
}