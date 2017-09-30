using System;

namespace p14_IntegerToHexAndBinary
{
    class IntegerToHexAndBinary
    {
        static void Main(string[] args)
        {
            int num = int.Parse(Console.ReadLine());
            string hex = Convert.ToString(num, 16).ToUpper();
            string binary = Convert.ToString(num, 2);
            Console.WriteLine(hex + "\r\n" + binary);
        }
    }
}
