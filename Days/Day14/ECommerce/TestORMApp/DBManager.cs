namespace DAL;
using BOL;
//CRUD Operations code against database inside DBManager
public class DBManager:IDBManager{
    public void Delete(int id)
    {
        using(var context = new CollectionContext())
        {
            context.Departments.Remove(context.Departments.Find(id));
            context.SaveChanges();
        }
    }

    public List<Department> GetAll()
    {   //Deterministic Finalization
        using (var context = new CollectionContext())
        {
            //LINQ
            var departments=from dept in context.Departments select dept;
            return departments.ToList<Department>();
        }
    }

    public Department GetById(int id)
    {
        using (var context = new CollectionContext())
        {
            var dept = context.Departments.Find(id);
            return dept;
        }
    }

    public void Insert(Department dept)
    {
        using (var context = new CollectionContext())
        {
            context.Departments.Add(dept);
            context.SaveChanges();  
        }
    }
    public void Update(Department dept)
    {
        using (var context = new CollectionContext())
        {
            var theDept = context.Departments.Find(dept.Id);
            theDept.Name =dept.Name;
            theDept.Location=dept.Location;
            context.SaveChanges();
        }
    }
}