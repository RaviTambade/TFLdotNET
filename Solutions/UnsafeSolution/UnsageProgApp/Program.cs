using System;

class Program
{
    static unsafe void Main()
    {
        int[] numbers = { 10, 20, 30, 40 };

        fixed (int* ptr = numbers)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                 
                Console.WriteLine(*(ptr + i));
            }
        }
    }
}