using System.Linq;
using System.Text;
using System.Xml;
using TFL.Data.Entities;
namespace TFL.Data.Managers;
public class XMLManager{
   public static void ReadXMLFileUsingXMLDocument()
    {
        XmlDocument xmlDcoument = new XmlDocument();
        xmlDcoument.Load(@"D:\employees_save.xml");

        XmlNodeList? xmlNodeList = xmlDcoument.DocumentElement.SelectNodes("/Employees/Employee");

        Console.WriteLine("Output using XMLDocument");
        foreach (XmlNode xmlNode in xmlNodeList)
        {
            Console.WriteLine("Id of the Employee is : " + xmlNode.SelectSingleNode("Id").InnerText);
            Console.WriteLine("Name of the Employee is : " + xmlNode.SelectSingleNode("Name").InnerText);
            Console.WriteLine();
        }
    }

    public static void WriteXMLFileUsingXMLDocument(List<Employee> employees)
    {
       
        XmlDocument xmlDoc = new XmlDocument();  

        XmlNode docNode = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
        xmlDoc.AppendChild(docNode);

        XmlNode employeesNode = xmlDoc.CreateElement("Employees");
        xmlDoc.AppendChild(employeesNode);
    
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
        xmlDoc.Save(@"D:\employees_save.xml");
    }
}