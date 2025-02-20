using catalog;

//Main Entry Point function
//Product usuage Implementation

ProductManager mgr=new ProductManager();
List<Product> retrivedProducts=mgr.GetAll();

for(int i=0;i<retrivedProducts.Count;i++){
    Console.WriteLine("Title: "+ retrivedProducts[i].Title);
}