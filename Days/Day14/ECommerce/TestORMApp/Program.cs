using System;
using System.Collections.Generic;
using BOL;
using DAL;
Console.WriteLine ("Welcome to Microlearning :Transflower");
IDBManager dbm=new DBManager();
bool status=true;
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
        case 2:
            var newDept = new Department()
            {
                Id = 23,
                Name = "Research",
                Location = "Chennai"
            };
            dbm.Insert(newDept);
        break;
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
        case 4:
            dbm.Delete(23);
        break;
        case 5:
            status = false;
        break;
    }
}
