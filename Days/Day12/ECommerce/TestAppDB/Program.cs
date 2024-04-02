using Testing;
using System.Collections.Generic;
using HR;

List<Department> departments=DbTestManager.GetAllDepartments();
foreach ( Department dept in departments){
    Console.WriteLine(dept.Name + "  " + dept.Location);
}

//Database Operations unit Testing

