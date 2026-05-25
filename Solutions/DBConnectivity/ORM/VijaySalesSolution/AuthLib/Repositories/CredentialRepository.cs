using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Net.Http.Headers;

namespace AuthLib.Repositories
{
    public  class CredentialRepository
    {
        public  bool Validate(string email, string password)
        {
            bool status=false;

            string connectionString = @"Data Source=DESKTOP-H1K53PL\SQLEXPRESS;Initial Catalog=AssessmentDB;Integrated Security=True;Connect Timeout=30;Encrypt=False";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * from Credentials where Email='"+ email +"'  AND Password='"+ password+ "'";

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    status= true;
                }
                con.Close();
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
            return status;

        }
        public void ShowAllCredentials()
        {
            //Database Connectivity 
       

            //Connection----SqlConnection
            //Command-------SqlCommand
            //DataReader----SqlDataReader

            //steps for database connectivity

            //Receipe for database accessibilty
            //1.define connection string
            //Create connection object
            //Create command object
            //Open connection
            //Exeucte Query
            //Iterate result set
            //Show result console

            string connectionString = @"Data Source=DESKTOP-H1K53PL\SQLEXPRESS;Initial Catalog=AssessmentDB;Integrated Security=True;Connect Timeout=30;Encrypt=False";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * from Credentials where Email='ravi.tambade@transflower.in'";
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(reader["Id"].ToString());
                    Console.WriteLine(reader["Email"].ToString());
                    Console.WriteLine(reader["Password"].ToString());
                }
                con.Close();
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }


        }
    }
}
