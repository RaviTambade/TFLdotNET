public class UtilityManager
{
    public static void ViewNames(params object[] names)
    {
        foreach (var name in names)
        {
            Console.WriteLine(name);
        }
    }   

    //pass by reference
    public void Swap(ref string a, ref string b)
    {
        string temp = a;
        a = b;
        b = temp;
    }
    public void  Calculate(int radius, out double area, out double circumference)
    {
        //no local varaiable declaration
        area = Math.PI * radius * radius;
        circumference = 2 * Math.PI * radius;
    }
}