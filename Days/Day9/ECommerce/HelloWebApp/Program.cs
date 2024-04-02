using HR;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var emp =new Employee(){ Id=29, FirstName="Shubhangi", LastName="Tambade"} ;
var employees=new List<Employee>();

employees.Add( new Employee(){ Id=23, FirstName="Ravi", LastName="Tambade"});
employees.Add( new Employee(){ Id=24, FirstName="Sachin", LastName="Nene"});
employees.Add( new Employee(){ Id=25, FirstName="Shivani", LastName="Pande"});
employees.Add( new Employee(){ Id=26, FirstName="Madhu", LastName="Sharma"});

app.MapGet("/", () => "Hello World!");
app.MapGet("/api/hello",()=>"Transflower Store");
app.MapGet("/api/employees", ()=>employees);
app.MapGet("/api/employee",()=>emp);
app.Run();
