namespace Catalog;
public class SmartDevice:Product{
    public int Count{get;set;}
    public string? Specification{get;set;}

    public SmartDevice():base(){

    }

    ~SmartDevice(){
        
    }
    public void Connect(){
        Console.WriteLine("connecting to device");
    }
    public void DisConnect(){
            Console.WriteLine("disconnecting  device");
    }
}