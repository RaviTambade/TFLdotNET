namespace Utility;
using System.IO;

public class FileIOManager{

     public  void ReadData( ){
        string data =File.ReadAllText(@"d:\test\products.json");

     }

    public void WriteData(){
        string data=" ";
        File.WriteAllText(@"d:\test\products.json");

    }
}