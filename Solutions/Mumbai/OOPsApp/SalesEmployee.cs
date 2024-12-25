namespace HR{

    //Multilevel Inheritance
    //multiple inheritance is not supported in C#
    //Multiple interface inheritance is supported in C#
    public class SalesEmployee:Employee,IWorkable,IPresentable{
        //Clean Code:
        //Software Design Principles
        //Uncle Bob: Robert C. Martin
        //SOLID: Single Responsibility Principle
        //S: Single Responsibility Principle
        //O: Open Closed Principle (Open for Extension, Closed for Modification)
        //OOPs: Object Oriented Programming
        //KISS: Keep It Simple and Stupid
        //DRY: Don't Repeat Yourself
        //YAGNI: You Ain't Gonna Need It

        //Property
        private double bonus;
        public double Bonus
        {
            get { return bonus; }
            set { bonus = value; }
        }

        //auto property
        public double Sales { get; set; }
        public double Commission { get; set; }
        //constructor Chainning
        public SalesEmployee():this("Sameer",23 ,"Mumbai", 20000, "Accounts", 5000,700000, 10000){
            Sales = 0;
            Commission = 0;
        }

        public SalesEmployee(string name, int age, string address,
                    int employeeId, string department, double salary,
                     double sales, double commission):base(name, age, address, employeeId, department, salary){
            Sales = sales;
            Commission = commission;
        }

        public override double CalculateSalary(){
            HRA = 0.1 * BasicSalary;
            return BasicSalary + HRA + Commission;
        }

        public override   void PerformTask(){
            Console.WriteLine("Sales Employee is selling products");
        }

        public override string ToString(){
            double package=CalculateSalary();
            return $"{base.ToString()}, Sales: {Sales}, Commission: {Commission}, Package: {package}";
        }

        public void Work()
        {
            Console.WriteLine("Sales Employee is working");
        }

        public void Present()
        {
            Console.WriteLine("Sales Employee is presenting");
        }
    }
}