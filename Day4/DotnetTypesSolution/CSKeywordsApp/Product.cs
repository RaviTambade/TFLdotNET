
namespace catalog;
public class Product:IDisposable{
    public Product(){
        
    }
    public string Title{get;set;}
    public double UnitPrice{get;set;}

    public void Dispose(){
        //Release resource which are occupied during 
        //life time of Object till now
        //Deterministic Finalization
        Console.WriteLine("Dispose is invoked....");
        GC.SuppressFinalize(this);
    }

    ~Product(){
        //indeterministic finalization
         Console.WriteLine("Finalize(destructor) is invoked....");
    }
}