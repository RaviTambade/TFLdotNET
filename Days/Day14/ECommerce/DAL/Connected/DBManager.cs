namespace DAL.Connected;
using BOL;
using MySql.Data.MySqlClient;
//using inbuilt, external Object Models


public class DBManager{

    public static string conString=@"server=localhost;port=3306;user=root; password=password;database=transflower";       
    public  static List<Department> GetAllDepartments(){
            List<Department> allDepartments=new List<Department>();
            //database connectivity code
            //Connected Data Access Mode
            //MySqlConnection  : establishing connection
            //MySqlCommand      : query execution
            //MySqlDataReader   : result of query to be catured after processing query
            MySqlConnection con=new MySqlConnection();
            con.ConnectionString=conString;
            try{
                con.Open();
                //fire query to database
                MySqlCommand cmd=new MySqlCommand();
                cmd.Connection=con;
                string query="SELECT * FROM departments";
                cmd.CommandText=query;
                MySqlDataReader reader=cmd.ExecuteReader();
                while(reader.Read()){
                    int id = int.Parse(reader["id"].ToString());
                    string name = reader["name"].ToString();
                    string location = reader["location"].ToString();

                    Department dept=new Department{
                                                    Id = id,
                                                    Name = name,
                                                    Location = location
                    };
                    allDepartments.Add(dept);
                }
            }
            catch(Exception ee){
                Console.WriteLine(ee.Message);
            }
            finally{
                    con.Close();
            }

            return allDepartments;
    }

    public static Department GetDeparmentDetails(int id){
        Department dept = null;
        MySqlConnection con = new MySqlConnection();
        con.ConnectionString = conString;
        try
        {
            string query = "SELECT * FROM departments WHERE id=" + id;
            con.Open();
            MySqlCommand command = new MySqlCommand(query, con);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                id = int.Parse(reader["id"].ToString());
                string name = reader["name"].ToString();
                string location = reader["location"].ToString();
                dept = new Department
                {
                    Id = id,
                    Name = name,
                    Location = location
                };
            }

        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            con.Close();
        }
        return dept;
    }

     public static bool Insert(Department dept){
        bool status=false;
        string query = "INSERT INTO departments(name,location)" +
                            "VALUES('" + dept.Name + "','" + dept.Location + "')";

        MySqlConnection con = new MySqlConnection();
        con.ConnectionString = conString;
        try{
            con.Open();
            MySqlCommand command = new MySqlCommand(query, con);
            command.ExecuteNonQuery();  //DML
            status = true;
        } 
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            con.Close();
        }               
        return status;
     }

    public static bool Update(Department dept)
    {
        bool status = false;
        MySqlConnection con = new MySqlConnection();
        con.ConnectionString = conString;
        try
        {
            string query = "UPDATE departments SET location='" + dept.Location + "', name='" + dept.Name + "' WHERE id=" + dept.Id;
            MySqlCommand command = new MySqlCommand(query, con);
            con.Open();
            command.ExecuteNonQuery();
            status = true;
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            con.Close();
        }
        return status;
    }

    public static bool Delete(int id){
        bool status=false;
        MySqlConnection con = new MySqlConnection();
        con.ConnectionString = conString;
        try
        {
            string query = "DELETE FROM departments WHERE id=" + id;
            MySqlCommand command = new MySqlCommand(query, con);
            con.Open();
            command.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            con.Close();
        }
      return status;
    }



//Employee Operations CRUD
    public static bool DoesEmployeeExists(int id)
    {
        MySqlConnection con = new MySqlConnection();
        con.ConnectionString = conString;
        bool status = false;
        try
        {
            string query = "SELECT EXISTS (SELECT * FROM employees where id=" + id + ")";
            con.Open();
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();

            if ((Int64)reader[0] == 1)
            {
                status = true;
            }

            reader.Close();
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            con.Close();
        }
        return status;
    }
    public static List<Employee> GetAllEmployees()        //get all rows of employee table
    {

        List<Employee> employees = new List<Employee>();
        MySqlConnection con = new MySqlConnection();
        con.ConnectionString = conString;
        try
        {
            string query = " SELECT * FROM employees";
            con.Open();
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = int.Parse(reader["id"].ToString());
                string firstname = reader["firstname"].ToString();
                string lastname = reader["lastname"].ToString();
                string email = reader["email"].ToString();
                string address = reader["address"].ToString();
                int deptid = int.Parse(reader["deptid"].ToString());
                int managerid = int.Parse(reader["managerid"].ToString());

                Employee emp = new Employee
                {
                    Id = id,
                    FirstName = firstname,
                    LastName = lastname,
                    Email = email,
                    Address = address,
                    DeptId = deptid,
                    ManagerId = managerid,

                };
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
            con.Close();
        }
        return employees;
    }
    public static Employee GetById(int id)                   //show employee data by id
    {
        Employee foundEmployee = null;

        MySqlConnection con = new MySqlConnection();
        con.ConnectionString = conString;
        try
        {
            string query = "SELECT * FROM employees WHERE id =" + id;
            con.Open();
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                id = int.Parse(reader["id"].ToString());
                string firstName = reader["firstName"].ToString();
                string lastName = reader["lastName"].ToString();
                string email = reader["email"].ToString();
                string address = reader["address"].ToString();
                int deptid = int.Parse(reader["deptid"].ToString());
                int managerid = int.Parse(reader["managerid"].ToString());



                foundEmployee = new Employee
                {
                    Id = id,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Address = address,
                    DeptId = deptid,
                    ManagerId = managerid,

                };
            }
            reader.Close();
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            con.Close();
        }
        return foundEmployee;
    }
    public static List<Role> GetRolesOfEmployee(int empId){
        List<Role> roles=new List<Role>();
        //get all roles belong to empid 
        //*****************************
        MySqlConnection con = new MySqlConnection();
        con.ConnectionString = conString;
        try
        {
            //Query to return roles belong to employee id
            string query = "select rolename from roles"+
                            "where roleid IN("+
                            "select roleid from emp_roles"+
                            "where empid="+empId+");";
            Console.WriteLine(query);

            con.Open();
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Role theRole=new Role();
                theRole.Id=int.Parse(reader["roleid"].ToString());
                theRole.Name=reader["rolename"].ToString();
                roles.Add(theRole);
            }
        }
        catch(Exception ee){

        }
        finally{
            con.Close();
        }
        //******************************
        return roles;
    }
    public static bool Insert(Employee emp)         //insertion of employee table data 
    {
        bool status = false;
        MySqlConnection con = new MySqlConnection();
        con.ConnectionString = conString;
        try
        {
            string query = "Insert into employees (firstName,lastName,email,address,password, deptid,managerid) Values( '" + emp.FirstName + "','"
            + emp.LastName + "','" + emp.Email + "','" + emp.Address + "',  '" + emp.Password + "' ,'" + emp.DeptId + "','" + emp.ManagerId + "')";
            MySqlCommand cmd = new MySqlCommand(query, con);
            con.Open();
            cmd.ExecuteNonQuery();

            status = true;
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            con.Close();
        }
        return status;

    }
    public static bool SetPassword(string email, string password)
    {
        bool status = false;
        //set password for existing employee whoes email id matches
        //call ado.net code to update password field of employee
        return status;
    }
    public static bool Update(Employee emp)           //updating employee table data 
    {

        MySqlConnection con = new MySqlConnection();
        con.ConnectionString = conString;
        bool status = false;

        try
        {
            string query = "Update employees SET firstName ='" + emp.FirstName + "'," + "lastName ='" + emp.LastName + "',"
                    + "email='" + emp.Email + "'," + "address='" + emp.Address + "'," + "managerid=" + emp.ManagerId + "," +
                    "deptid=" + emp.DeptId + " WHERE id =" + emp.Id;

            MySqlCommand cmd = new MySqlCommand(query, con);
            con.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            con.Close();
        }
        return status;
    }
    public static void Delete(Employee emp)                  //delete employee table data
    {
        MySqlConnection con = new MySqlConnection();
        con.ConnectionString = conString;

        try
        {
            string query = " DELETE FROM employees WHERE id =" + emp.Id;
            MySqlCommand cmd = new MySqlCommand(query, con);
            con.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception ee)
        {
            throw ee;
        }
        finally
        {
            con.Close();
        }

    }
    public static List<Employee> GetEmployeesByDepartment(int deptid)   //get all rows of employee table
    {
        List<Employee> employees = new List<Employee>();
        MySqlConnection con = new MySqlConnection();
        con.ConnectionString = conString;
        try
        {
            string query = " SELECT * FROM employees where deptid=" + deptid;
            con.Open();
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = int.Parse(reader["id"].ToString());
                string firstname = reader["firstname"].ToString();
                string lastname = reader["lastname"].ToString();
                string email = reader["email"].ToString();
                string address = reader["address"].ToString();
                int dptid = int.Parse(reader["deptid"].ToString());
                int managerid = int.Parse(reader["managerid"].ToString());

                Employee emp = new Employee
                {
                    Id = id,
                    FirstName = firstname,
                    LastName = lastname,
                    Email = email,
                    Address = address,
                    DeptId = dptid,
                    ManagerId = managerid,

                };
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
            con.Close();
        }
        return employees;
    }
    public static bool Transfer(int empId, int deptId)
    {
        bool status = false;

        // create connection objet
        // set connection string to connection

        MySqlConnection con = new MySqlConnection();
        con.ConnectionString = conString;

        // define query to update existing empoyees department id
        string query = "Update employees SET deptid=" + deptId + " WHERE id=" + empId;
        try
        {
            con.Open();
            // create command object
            //
            // associate connection and query 
            MySqlCommand cmd = new MySqlCommand(query, con);
            // execute command
            cmd.ExecuteNonQuery();
            status = true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        // set status true on success 
        return status;
    }
    
    //Roles crude operations
    public static List<Role> GetAllRolesOfEmployee(int empId){
        List<Role> roles = new List<Role>();
        MySqlConnection con = new MySqlConnection();
        con.ConnectionString = conString;
        try
        {
            string query = "select * from roles " +
                            "where roleid IN ( "+
                            "select roleid from emp_roles "+
                            "where empid="+ empId+ ")";
            Console.WriteLine(query);
            con.Open();
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int roleId = int.Parse(reader["roleid"].ToString());
                string roleName = reader["rolename"].ToString();
                Role theRole= new Role
                {
                    Id = roleId,
                    Name= roleName
                };
                roles.Add(theRole);
            }
                reader.Close();
        }
        catch(Exception e)
        {
            throw e;
        }
        finally
        {
            con.Close();
        }
        return roles;
    }
    public static List<Role> GetAllRoles()
    {
        List<Role> roles = new List<Role>();
        MySqlConnection con = new MySqlConnection();
        con.ConnectionString = conString;
        try
        {
            string query = "SELECT * FROM roles order by roleid asc";
            con.Open();
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int roleId = int.Parse(reader["roleid"].ToString());
                string roleName = reader["rolename"].ToString();

                Role role1= new Role
                {
                    Id = roleId,
                    Name= roleName
                };
                roles.Add(role1);
            }
                reader.Close();
        }
        catch(Exception e)
        {
            throw e;
        }
        finally
        {
            con.Close();
        }
        return roles;
    }
    public static List<Employee> GetAllEmployeesByRole(string rolename)
    {
          List<Employee> employees = new List<Employee>();
        MySqlConnection con = new MySqlConnection();
        con.ConnectionString = conString;
        try
        {
            string query = " select * from employees"+
                           " where id IN("+
                                    "select  empid from emp_roles "+
                                    "where  roleid=(" +
                                                    "select roleid from roles "+
                                                    "where rolename='"+rolename+"'))";
            con.Open();
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = int.Parse(reader["id"].ToString());
                string firstname = reader["firstname"].ToString();
                string lastname = reader["lastname"].ToString();
                string email = reader["email"].ToString();
                string address = reader["address"].ToString();
                int deptid = int.Parse(reader["deptid"].ToString());
                int managerid = int.Parse(reader["managerid"].ToString());

                Employee emp = new Employee
                {
                    Id = id,
                    FirstName = firstname,
                    LastName = lastname,
                    Email = email,
                    Address = address,
                    DeptId = deptid,
                    ManagerId = managerid,

                };
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
            con.Close();
        }
        return employees;
    }
    
    //Add role for existing employee 
    public static bool AssignRole(int empid,int roleid)
    {
        bool status=false;
         MySqlConnection con = new MySqlConnection();
        con.ConnectionString = conString;
        try
        {
            string query = "Insert into emp_roles(empid,roleid) values("+empid+","+roleid+")";
             con.Open();
             MySqlCommand cmd=new MySqlCommand(query,con);
             cmd.ExecuteNonQuery(); 
             status=true;
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            con.Close();
        }
        return status;
    }

    //Remove role of existing employee from RoleMapping
    public static bool UnAssignRole(int empid,int roleid)
    {
        bool status=false;
         MySqlConnection con = new MySqlConnection();
        con.ConnectionString = conString;
        try
        {
            //*****************************"
            //Query for removing roles assigned to emp id
            string query = "delete from emp_roles WHERE empid=" + empid+ 
                            " AND roleid="+roleid;
             con.Open();
             MySqlCommand cmd=new MySqlCommand(query,con);
             cmd.ExecuteNonQuery(); 
             status=true;
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            con.Close();
        }
        return status;
    }}



//

            //DisConnected Data Access Mode
             //MySqlConnection  : establishing connection
            //MySqlCommand      : query execution
            //MySqlDataApater
            //DataSet
            //DataTable
            //DataRow
            //DataColumn
            //DataRealtion
