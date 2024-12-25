
namespace DAO{
    public class DBManager :IDisposable{
        public DBManager(){
            Console.WriteLine("DBManager Instance Created");
            //exernal resource allocation
            //connection open
            //file open
            Console.WriteLine("Connection opened");
            Console.WriteLine("File opened");

        }
        public void GetData( string query){
            //JDBC, ODBC, ADO.NET
            //Entity Framework, Hibernate
            //JPA
            Console.WriteLine("Data Fetched from Database");
        }
        public void SaveData(string query){
            Console.WriteLine("Data Saved to Database");
        }
        public void UpdateData(){
            Console.WriteLine("Data Updated in Database");
        }

        public void Dispose()
        {
            Console.WriteLine("DBManager Instance Disposed");
            //exernal resource deallocation
            //connection close
            //file close
            Console.WriteLine("Connection released");
            Console.WriteLine("File closed");
            GC.SuppressFinalize(this);
        }

        ~DBManager(){
            Console.WriteLine("DBManager Instance Destroyed");
            //exernal resource deallocation
            //connection close
            //file close
              Console.WriteLine("Connection released");
            Console.WriteLine("File closed");
        }
    }


}