using Mathematics;
using tflDrawing;
Console.WriteLine("Hello, World!");

//value type
//value type variables data is kept on stack

int i = 45;

Complex complex = new Complex(1, 2); 
Complex complex2 = new Complex(1, 87);

int resultreal = complex.Real;
int resultimag = complex2.Imag;


//Polymorphism

Shape theShape = new Line();
theShape.Draw();

Shape theShape2 = new Circle();
theShape2.Draw();


