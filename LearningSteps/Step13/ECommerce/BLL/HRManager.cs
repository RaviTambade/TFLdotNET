namespace BLL;
using BOL;
using DAL.DisConnected;
public class HRManager
{
    public List<Department> GetAllDepartments(){
        List<Department> allDepartments = new List<Department>();
        allDepartments=DBManager.GetAllDepartments();
        return allDepartments;
    }

}