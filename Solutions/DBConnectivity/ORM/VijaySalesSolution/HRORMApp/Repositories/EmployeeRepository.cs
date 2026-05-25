using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRORMApp.DbContexts;
using HRORMApp.Entities;
namespace HRORMApp.Repositories
{
    public class EmployeeRepository
    {
        private HRContext _context;
        public EmployeeRepository(HRContext context)
        {
            _context = context;
        }

        public List<Employee> GetAll()
        {
            var employees = _context.Employees.ToList();
            return employees;
        }

        public bool Insert(Employee employee)
        {
            bool status = false;
            _context.Employees.Add(employee);
            _context.SaveChanges();
            status = true;
            return status;
        }

        public bool Update(Employee employee)
        {
            bool status = false;
            var empToUpdate = _context.Employees.FirstOrDefault(emp => emp.Id == employee.Id);
            if (empToUpdate != null)
            {
                empToUpdate.FirstName = "Suresh";
                empToUpdate.LastName = "Mane";
                _context.SaveChanges();
            }
            status = true;
            return status;
        }

        public bool Remove(int id)
        {
            bool status = false;
            var empToUpdate = _context.Employees.FirstOrDefault(emp => emp.Id == id);
            if (empToUpdate != null)
            {
                _context.Employees.Remove(empToUpdate);
                _context.SaveChanges();
            }
            status = true;
            return status;
        }
    }
}