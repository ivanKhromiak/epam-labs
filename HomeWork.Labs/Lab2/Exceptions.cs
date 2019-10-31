
namespace Epam.HomeWork.Lab2
{
    using System;

    public static class Exceptions
    {
        public static int ArraySize { get; set; } = 1;

        public static void GenerateStackOverFlowException()
        {
            _ = f(0);
            
            int f(int a)
            {
                return f(a);
            }
        }

        public static void GenerateIndexOutOfRangeException()
        {
            int[] array = new int[ArraySize];

            _ = array[ArraySize];

        }

        public static int DoSomeMath(int a, int b)
        {
            if (a < 0)
                throw new ArgumentException("Parameter should be greater than 0", "a");
            if (b > 0)
                throw new ArgumentException("Parameter should be less than 0", "b");

            return a + b;
        }
    }
}
