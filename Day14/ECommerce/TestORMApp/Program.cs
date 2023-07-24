using System;
using System.Collections.Generic;
using BOL;
using DAL;
Console.WriteLine ("Welcome to Microlearning :Transflower");

IDBManager dbm=new DBManager();

bool status=true;
// Console based Menu driven User Interface
while(status)
{
    Console.WriteLine("Choose Option :");
    Console.WriteLine("1. Display  records");
    Console.WriteLine("2. Insert  new record");
    Console.WriteLine("3. Update existing record");
    Console.WriteLine("4. Delete existing record");
    Console.WriteLine("5. Exit");

    switch(int.Parse(Console.ReadLine()))
    {
        //Display Departments
        case 1:
        {
            List<Department> allDepartments=dbm.GetAll();

            foreach (var dept in allDepartments)
            {
                Console.WriteLine(" Id: {0}, Name: {1}, Location: {2}",
                                    dept.Id,dept.Name,dept.Location);
            }
        }
        break;
            
        //Insert new  Department
        case 2:
            var newDept = new Department()
            {
                Id = 23,
                Name = "Research",
                Location = "Chennai"
                
            };
            dbm.Insert(newDept);
        break;

        // Update existing Department
        case 3:
        {
            var updateDepartment = new Department(){
                Id = 23,
                Name = "Warehouse",
                Location = "Mumbai"   
            };
            dbm.Update(updateDepartment);
        }
        break;
    
        // Delete existing Department
        case 4:
            dbm.Delete(23);
        break;
    
        //Exit from loop
        case 5:
            status = false;
        break;
    }
}


