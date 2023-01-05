namespace Mathematics;
public static class Helper{

    public static  int Multiply( this MathEngine m,int num1, int num2){
       
       m.Result=num1 * num2;
       return m.Result;

    }
    public static int Divide(int num1, int num2){
        return num1/num2;
    }
}