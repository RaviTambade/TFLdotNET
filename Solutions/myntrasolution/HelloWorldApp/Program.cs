using HR;


DateTime day=DateTime.Now;
Console.WriteLine("" + day.ToString());
Console.WriteLine("Hello, World!");

Employee employee= new Employee();
employee.Name="Ravi Tambade";
employee.Location="Pune";
employee.Age=49;
employee.BasicSalary=45000;
employee.DailyAllowance=1000;
employee.Department="Training";
employee.Id=7654;
employee.HRA=25000;

Console.WriteLine(employee.Name  + " "+ employee.Location);