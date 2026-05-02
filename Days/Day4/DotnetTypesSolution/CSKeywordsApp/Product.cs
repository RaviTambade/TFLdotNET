namespace catalog;
public class Product:IDisposable{
    public Product(){
    }
    public string Title{get;set;}
    public double UnitPrice{get;set;}
    public void Dispose(){
        Console.WriteLine("Dispose is invoked....");
        GC.SuppressFinalize(this);
    }
    ~Product(){
         Console.WriteLine("Finalize(destructor) is invoked....");
    }
}
