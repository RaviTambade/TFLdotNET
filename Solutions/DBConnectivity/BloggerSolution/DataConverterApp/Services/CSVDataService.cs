namespace TFL.Data.Services;
using System.Reflection;
using System.Text;
using System.Globalization;

using CsvHelper;

using  TFL.Data.Entities;
public class  CSVDataService:IDataService{

    public bool Serialize(string fileName, List<Employee> employees){
        bool status=false;
       /* 
        StringBuilder sb = new StringBuilder();
        PropertyInfo[]? properties = typeof(Employee).GetProperties();
        IEnumerable<string> columnNames = properties.Select(p => p.Name).ToList();
        sb.AppendLine(string.Join(",", columnNames));
        foreach (Employee obj in employees)
        {
            List<string>? values = new List<string>();
            foreach (PropertyInfo property in properties)
            {
                object? value = property.GetValue(obj);
                if (value is not null)
                {
                    values.Add(value.ToString()!);
                }
                else
                {
                    values.Add(" ");
                }
            }
            sb.AppendLine(string.Join(",", values));
        }
        File.WriteAllText($"{fileName}.csv", sb.ToString());
        */
        //List<T>

        using (var writer = new StreamWriter(fileName))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(employees);
        }
        status=true;
        return status;
    }
     public List<Employee> DeSerialize(string fileName){
        List<Employee> employees=new List<Employee>();
        var reader = new StreamReader(fileName);
        var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var records = csv.GetRecords<Employee>();
        return records.ToList() as List<Employee>;
    }
}