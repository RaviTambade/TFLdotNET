using System; //importing the namespace

namespace  SOA{  //namespace declaration
    public class ProductService{
        public void AddProduct(){
            Console.WriteLine("Product Added");
            int count=0;
            for(int i=0;i<count;i++){
                Console.WriteLine("Product Added");
                if(i==5){
                    Console.WriteLine("Product Added");
                    int size=67;
                    if(size>50){
                        Console.WriteLine("Product Added");
                    }
                    else{
                        Console.WriteLine("Product Added");
                    }
                }   
                else{
                    Console.WriteLine("Product Added");
                }
            }
        }
        public void UpdateProduct(){
            Console.WriteLine("Product Updated");
        }
        public void DeleteProduct(){
            Console.WriteLine("Product Deleted");
        }
        public void GetProduct(){
            Console.WriteLine("Product Fetched");
        }

    }

}