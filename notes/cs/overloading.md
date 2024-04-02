
## Method Overloading

Overloading is the ability to define several methods with the same name, provided each method has a different signature

```
public class MathEngine
{
    public static double FindSquare (double number) { // logic defined   }
    public static double FindSquare (int number) { // another logic defined }
}
public static void Main ()
{
    double res= MathEngine.FindSquare(12.5);
    double num= MathEngine.FindSquare(12);
}

```

## Operator Overloading

Giving additional meaning to existing operators

```

public static Complex Operator + (Complex c1, Complex c2) 
 {
    Complex temp= new Complex();
    temp.real = c1.real+ c2.real;
    templ.imag = c1.image + c2.imag;
    return temp;
 }
public static void Main ()
{
    Complex o1= new Complex (2, 3);
    Complex o2= new Complex (5, 4);
    Complex o3= 1+ o2;
	Console.WriteLine (o3.real + “ “ + o3.imag);
}



```