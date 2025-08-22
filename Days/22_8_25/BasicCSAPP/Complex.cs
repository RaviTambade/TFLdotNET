
namespace Mathematics;

public class Complex
{
    private int real;
    private int imag;

    public Complex(int real, int imag)
    {
        this.real = real;
        this.imag = imag;
    }


    //Property sytax
    public int Real
    {
        get { return real; }
        set { real = value; }
    }

    public int Imag
    {
        get { return imag; }
        set { imag = value; }
    }

}
