using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthLib.Entities;
using System.Data.SqlClient;




namespace AuthLib.Repositories
{
    public  class HRRepository
    {
        public  List<Employee> GetAllEmployee()
        {

            List<Employee> list = new List<Employee>();

            //Data Access Logic using database connectivity

            string connectionString = @"Data Source=DESKTOP-H1K53PL\SQLEXPRESS;Initial Catalog=AssessmentDB;Integrated Security=True;Connect Timeout=30;Encrypt=False";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * from Employees";
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Employee emp = new Employee();
                    emp.Id = int.Parse(reader["Id"].ToString());
                    emp.FirstName = reader["firstname"].ToString();
                    emp.LastName = reader["lastname"].ToString();
                    emp.Email = reader["email"].ToString();
                    emp.Contact = reader["contact"].ToString();

                    list.Add(emp);
                }
                con.Close();
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
            return list;
        }
    }
}
