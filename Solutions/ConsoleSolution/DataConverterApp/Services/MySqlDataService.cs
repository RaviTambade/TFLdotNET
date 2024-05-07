using System.Data;
using MySql.Data.MySqlClient;

namespace TFL.Data.Services;

using  TFL.Data.Entities;
public class  MySqlDataService:IDataService{

    public bool Serialize(string connectionString, List<Employee> employees)
    {
        bool status=false;
        MySqlConnection connection = new MySqlConnection();
        connection.ConnectionString = connectionString;
        connection.Open();
        try
        {
            foreach( Employee emp in employees)
            {
                string query =@"Insert Into employees(id,name)  values(@employeeId,@name)";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@employeeId", emp.Id);
                cmd.Parameters.AddWithValue("@name", emp.Name); 
                cmd.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
             connection.Close();
        }
        status=true;
        return status;
    }
           

     public List<Employee> DeSerialize(string connectionString){
        List<Employee> employees=new List<Employee>();
        bool status=false;
        MySqlConnection connection = new MySqlConnection();
        connection.ConnectionString = connectionString;
        connection.Open();
        try
        {
                string query =@"SELECT * FROM employees";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader=cmd.ExecuteReader();
                while(reader.Read()){
                    Employee emp=new Employee();
                    emp.Id=int.Parse(reader["Id"].ToString());
                    emp.Name=reader["Name"].ToString();
                    employees.Add(emp);
                }
                reader.Close();
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            connection.Close();
        }
        return employees;
    }
}