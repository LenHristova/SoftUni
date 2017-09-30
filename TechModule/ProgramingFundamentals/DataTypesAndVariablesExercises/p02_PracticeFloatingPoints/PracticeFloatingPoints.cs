using System;

namespace p02_PracticeFloatingPoints
{
    class PracticeFloatingPoints
    {
        static void Main(string[] args)
        {
            decimal decimalNum = 3.141592653589793238m;
            double doubleNum = 1.60217657d;
            decimal decimalNum2 = 7.8184261974584555216535342341m;
            Console.WriteLine(
                $"{decimalNum}\r\n" +
                $"{doubleNum}\r\n" +
                $"{decimalNum2}");
        }
    }
}
