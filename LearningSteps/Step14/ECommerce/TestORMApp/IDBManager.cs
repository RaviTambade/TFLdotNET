namespace DAL;
using BOL;
    public interface IDBManager{
        List<Department> GetAll();
        Department GetById(int id);
        void Insert(Department dept);
        void Update(Department dept);
        void Delete(int id);
    }
