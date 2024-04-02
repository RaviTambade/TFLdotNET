namespace Drawing ;
//in C# multiple implementation inheritance is not allowed

public class Rectangle:Shape,IPrintable{
    public Point StartPoint{get;set;}
    public Point EndPoint{get;set;}

    public Rectangle(){
        this.StartPoint=new Point(0,0);
        this.EndPoint=new Point(0,0);    
    }
    public Rectangle(Point pt1, Point pt2){
        this.StartPoint=pt1;
        this.EndPoint=pt2;
    }

    public override void Draw()
    {
        Type t=this.GetType();
        Console.WriteLine("Type ="+ t.Name);
       Console.WriteLine("("+ this.StartPoint+ "), (" + this.EndPoint+ ")," +
       this.Width+"," + this.Color);
    }

    public void Print(){
        Console.WriteLine("Printing object on existing printer");

        Type t=this.GetType();
        Console.WriteLine("Type ="+ t.Name);
       Console.WriteLine("("+ this.StartPoint+ "), (" + this.EndPoint+ ")," +
       this.Width+"," + this.Color);
    }
}