using TFL.Data.Managers;
using TFL.Data.Entities;
using TFL.Data.Services;

using System.Xml;


Employee e1= new Employee{ Id=66, Name="Shama"};
Employee e2= new Employee{ Id=67, Name="Rama"};
Employee e3= new Employee{ Id=68, Name="Krishna"};

List<Employee> employees=new List<Employee>();
employees.Add(e1);
employees.Add(e2);
employees.Add(e3);

//XMLManager.ReadXMLFileUsingXMLDocument();
//XMLManager.WriteXMLFileUsingXMLDocument(employees);

//string fileName=@"D:/tstEmployees.xml";
//string fileName=@"D:/tstEmployees.json";
//string fileName=@"D:/tstEmployees_rec.csv";

//string fileName=@"D:/tstEmployees_rec.xml";
string fileName=@" ";
IDataService service=new XMLDataService();
//IDataService service=new JSONDataService();
//IDataService service=new CSVDataService();


bool status=service.Serialize(fileName, employees);

if(status){
    Console.WriteLine("Data is Serialized into file");
}
else{
     Console.WriteLine("Something has gone wrong");

}

 

 employees=service.DeSerialize(fileName);

foreach( Employee emp in employees){
    Console.WriteLine(emp.Id.ToString() + "  "+emp.Name );
}

 
