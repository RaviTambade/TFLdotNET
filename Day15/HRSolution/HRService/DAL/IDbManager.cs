namespace HRService.DAL;
using HRService.BOL;
public interface IDBManager{
    List<Employee> GetAll();
    // Employee GetById(int id);
    // void Insert(Employee employee);
    // void Update(Employee employee);
    // void Delete(int id);
}