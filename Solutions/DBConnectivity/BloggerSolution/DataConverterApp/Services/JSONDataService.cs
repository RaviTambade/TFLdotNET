using  TFL.Data.Entities;
using System.IO;
using System.Text.Json;
namespace TFL.Data.Services;
public class JSONDataService:IDataService{
    public bool Serialize(string fileName, List<Employee> employees){
        bool status=false;
        var options = new JsonSerializerOptions { IncludeFields = true };
        var employeesJson = JsonSerializer.Serialize<List<Employee>>(employees, options);
        File.WriteAllText(fileName, employeesJson);
        status=true;
        return status;
    }
    public  List<Employee> DeSerialize(string fileName){
        List<Employee> list=new List<Employee>();
        string jsonString = File.ReadAllText(fileName);
        list = JsonSerializer.Deserialize<List<Employee>>(jsonString);
        return list;
    }
}