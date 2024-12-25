namespace Mathematics
{
    public class Complex
    {
        public static int count=0;
        //auto property
        public int Real { get; set; }
        public int Imaginary { get; set; }

        //constructor overloading
        //method overloading
        public Complex()
        {
            Real = 0;
            Imaginary = 0;
            count++;
        }
        public Complex(int real, int imaginary)
        {
            Real = real;
            Imaginary = imaginary;
            count++;
        }

        //method overriding
        public override string ToString()
        {
            /*string result=this.Real + " + " + this.Imaginary + "i";
            reurn result;*/
            return $"{Real} + {Imaginary}i";
        }

        public static  int GetCount()
        {
            return count;
        }

        //arithmatic operators can be overloaded in C#
        public static Complex operator +(Complex c1, Complex c2)
        {
            return new Complex(c1.Real + c2.Real, c1.Imaginary + c2.Imaginary);
        }

        public static Complex operator -(Complex c1, Complex c2)
        {
            return new Complex(c1.Real + c2.Real, c1.Imaginary + c2.Imaginary);
        }
    }
}