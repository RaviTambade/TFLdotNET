namespace HR{
    public abstract class Employee:Person{
        public int EmployeeId { get; set; }
        public string Department { get; set; }
        public double BasicSalary { get; set; }
        public double HRA { get; set; }

        //Member Initiaized List    
        public Employee():base(){
            EmployeeId = 0;
            Department = "Unknown";
            BasicSalary = 0;
        }

        public Employee(string name, int age, string address,
                      int employeeId, string department, double salary):base(name, age, address){
            EmployeeId = employeeId;
            Department = department;
            BasicSalary = salary;
        }

        public virtual double CalculateSalary(){
            return BasicSalary + HRA;
        }

        public abstract void PerformTask();
        public override string ToString(){
            double package=CalculateSalary();
            return $"{base.ToString()}, EmployeeId: {EmployeeId}, Department: {Department}, Package: {package}";
        }
    }
}