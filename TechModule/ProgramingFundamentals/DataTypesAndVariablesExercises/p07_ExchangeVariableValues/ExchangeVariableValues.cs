using System;

namespace p07_ExchangeVariableValues
{
    class ExchangeVariableValues
    {
        static void Main(string[] args)
        {
            int num1 = 5;
            int num2 = 10;

            int tempNum = num1;
            num1 = num2;
            num2 = tempNum;
            Console.WriteLine($"Before:\r\n" +
                $"a = {num2}\r\n" +
                $"b = {num1}\r\n" +
                $"After:\r\n" +
                $"a = {num1}\r\n" +
                $"b = {num2}");
        }
    }
}
