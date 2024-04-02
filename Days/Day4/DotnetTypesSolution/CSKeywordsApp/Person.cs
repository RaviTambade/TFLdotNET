namespace HR;


//to block inheritance we use sealed keyword
public sealed class Person{
    public double PI;
    public  int Id{get;set;}
    public string FirstName{get;set;}
    public string LastName{get;set;}

    //Step 1: Singleton pattern
    public static Person _ref=null;

    //Step 2: Singleton pattern
    Person(){
        this.FirstName="Ravi";
        this.LastName="Tambade";
        this.Id=455;
        PI=3.14;
    }

    //Step 3: Singleton pattern
    public static Person CreateInstance(){
        if (_ref ==null){
            _ref=new Person();
        }
        return _ref;
    }
    public void Display( params string [] subjects){
        foreach(string s in subjects)
        Console.WriteLine(s);
    }


    public void Swap( ref int num1, ref int num2){
        int temp;
        temp=num1;
        num1=num2;
        num2=temp;

    }

    public void Calculate(int radius, out double area, out double circumference ){
       area=PI * radius * radius;
       circumference=2 * PI * radius;
    }
}