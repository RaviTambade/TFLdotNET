using  TFL.Data.Entities;
namespace TFL.Data.Services;
public interface IDataService{
    bool Serialize(string fileName, List<Employee> employees);
    List<Employee> DeSerialize(string fileName);
}