namespace HRService.DAL;
using HRService.BOL;
public class DBManager:IDBManager{
    public List<Employee> GetAll(){
        using(var context=new CollectionContext()){
            var employees=from emp in context.Employees select emp;
            return employees.ToList<Employee>();
            //tolist(), used to convert the data elements of an array into a list.            
        }
    }
}