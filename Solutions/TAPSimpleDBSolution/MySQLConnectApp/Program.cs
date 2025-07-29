using MySql.Data.MySqlClient;
using System.Data;
//Connect to MySQL Database
string ConnectionString = "Server=localhost; Port=3306; Database=ecommerce; User=root; password=password";
MySqlConnection con = new MySqlConnection(ConnectionString);
try
{
    con.Open();
    //Console.WriteLine("Connected to MySQL!");
    string strCmd = "SELECT * from product";
    MySqlCommand cmd = new MySqlCommand(strCmd, con);
    cmd.CommandType = CommandType.Text;
    MySqlDataReader reader = cmd.ExecuteReader();
    while (reader.Read())
    {
         Console.WriteLine($"ProductID: {reader["ProductID"]}, Name: {reader["Title"]}, Description: {reader["Description"]}");
    }

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    Console.WriteLine($"Error: {ex.Message}");
}
finally
{
    con.Close();
}