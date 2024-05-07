using System.Linq;
using System.Text;
using System.Xml;
using  TFL.Data.Entities;
namespace TFL.Data.Services;

public class  XMLDataService:IDataService {
    public bool Serialize(string fileName, List<Employee> employees){
        bool status=false;
        XmlDocument xmlDoc = new XmlDocument();  

        XmlNode docNode = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
        xmlDoc.AppendChild(docNode);

        XmlNode employeesNode = xmlDoc.CreateElement("Employees");
        xmlDoc.AppendChild(employeesNode);
    
        try{
            foreach (Employee employee in employees){
            XmlNode empNode = xmlDoc.CreateElement("Employee");
            employeesNode.AppendChild(empNode);
        
            XmlNode idNode = xmlDoc.CreateElement("Id");
            idNode.InnerText = employee.Id.ToString();
            empNode.AppendChild(idNode);
        
            XmlNode nameNode = xmlDoc.CreateElement("Name");
            nameNode.InnerText = employee.Name;
            empNode.AppendChild(nameNode);
        }
        xmlDoc.Save(fileName);
        status=true;
        }
        catch (Exception e){
            Console.WriteLine(e.Message);
        }
        return status;
    }

    public  List<Employee> DeSerialize(string fileName){
        List<Employee> list=new List<Employee>();
        XmlDocument xmlDcoument = new XmlDocument();
        xmlDcoument.Load(fileName);

        XmlNodeList? xmlNodeList = xmlDcoument.DocumentElement.SelectNodes("/Employees/Employee");
        
        foreach (XmlNode xmlNode in xmlNodeList)
        {
            Employee employee=new Employee();
            employee.Id=int.Parse(xmlNode.SelectSingleNode("Id").InnerText);
            employee.Name=xmlNode.SelectSingleNode("Name").InnerText;
            list.Add(employee);
        }
        return list;
    }
}