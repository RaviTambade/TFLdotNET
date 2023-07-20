namespace BLL;
using BOL;
using DAL.DisConnected;
using DAL.Connected;
public class HRManager
{
    public List<Department> GetAllDepartments(){
        List<Department> allDepartments = new List<Department>();
        allDepartments=DAL.DisConnected.DBManager.GetAllDepartments();
        //filter department which are not performing
        //Sort all Deparment based on
        //number of employees work in dept
        List<Department> sortedPerformingDepartments = allDepartments;
        return allDepartments;
    }

    public Department GetDepartment(int id) {
        Department department = null;
        department = DAL.Connected.DBManager.GetDeparmentDetails(id);
        return department;

    }
}