using System;

namespace p08_GreaterOfTwoValues
{
    class Program
    {
        static void Main(string[] args)
        {
            string type = Console.ReadLine();

            switch (type)
            {
                case "int":
                    int num1 = int.Parse(Console.ReadLine());
                    int num2 = int.Parse(Console.ReadLine());
                    int maxNum = GetMax(num1, num2);
                    Console.WriteLine(maxNum);
                    break;
                case "char":
                    char ch1 = char.Parse(Console.ReadLine());
                    char ch2 = char.Parse(Console.ReadLine());
                    char maxChar = GetMax(ch1, ch2);
                    Console.WriteLine(maxChar);
                    break;
                case "string":
                    string str1 = Console.ReadLine();
                    string str2 = Console.ReadLine();
                    string maxStr = GetMax(str1, str2);
                    Console.WriteLine(maxStr);
                    break;
            }
        }

        private static int GetMax(int num1, int num2)
        {
            return Math.Max(num1, num2);
        }
        private static char GetMax(char ch1, char ch2)
        {
            return ch1 >= ch2 ? ch1 : ch2;
        }
        private static string GetMax(string str1, string str2)
        {
            return str1.CompareTo(str2) >= 0 ? str1 : str2;
        }
    }
}
