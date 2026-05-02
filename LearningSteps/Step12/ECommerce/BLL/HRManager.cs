namespace BLL;
using BOL;
using DAL.DisConnected;
using DAL.Connected;
public class HRManager
{
    public List<Department> GetAllDepartments(){
        List<Department> allDepartments = new List<Department>();
        allDepartments=DAL.DisConnected.DBManager.GetAllDepartments();
        List<Department> sortedPerformingDepartments = allDepartments;
        return allDepartments;
    }
    public Department GetDepartment(int id) {
        Department department = null;
        department = DAL.Connected.DBManager.GetDeparmentDetails(id);
        return department;
    }
}
