using System;
using System.Linq;
using System.Text;

namespace MultiplyBigNumber
{
    class StartUp
    {
        static void Main()
        {
            string multiplier1 = Console.ReadLine();
            int multiplier2 = int.Parse(Console.ReadLine());

            string result = "0";
            if (multiplier1 != "0" && multiplier2 != 0)
            {
                result = Multiply(multiplier1, multiplier2);
            }
             
            Console.WriteLine(result);
        }

        private static string Multiply(string multiplier1, int multiplier2)
        {
            StringBuilder sb = new StringBuilder();
            int reminder = 0;
            foreach (var digit in multiplier1.Reverse())
            {
                string multipliedDigit = $"{((digit - 48) * multiplier2 + reminder):D2}";
                sb.Insert(0, multipliedDigit[1]);
                reminder = multipliedDigit[0]-48;
            }
            sb.Insert(0, reminder);

            return sb.ToString().TrimStart('0');
        }
    }
}