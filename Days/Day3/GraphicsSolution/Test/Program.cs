using Drawing;
using System.Collections.Generic;
Point pt1=new Point(12,12);
Point pt2=new Point(120,145);

Shape shape1=new Line(pt1, pt2);
Point pt3=new Point(54,15);
Point pt4=new Point(65,15);

Shape shape2=new Rectangle(pt3,pt4);
Point center=new Point(45,45);
int radius=25;
Shape shape3=new Circle(center, radius);

//

List<Shape> shapes=new List<Shape>();
shapes.Add(shape1);
shapes.Add(shape2);
shapes.Add(shape3);

Console.WriteLine( "Drawing all Shapes");

foreach ( Shape  s in shapes){
    s.Draw();
    IPrintable obj= (IPrintable)s;
    obj.Print();

}



/*Console.WriteLine( "Printing all Shapes");
foreach ( IPrintable  obj in printingShapes){
    obj.Print();
}*/


 
