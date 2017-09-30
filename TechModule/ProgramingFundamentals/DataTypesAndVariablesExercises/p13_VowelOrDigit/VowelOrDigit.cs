using System;

namespace p13_VowelOrDigit
{
    class VowelOrDigit
    {
        static void Main(string[] args)
        {
            char symbol = char.Parse(Console.ReadLine());
            if (symbol > 47 && symbol < 58)
            {
                Console.WriteLine("digit");
            }
            else if (symbol == 'a' ||
                symbol == 'o' || 
                symbol == 'e' || 
                symbol == 'i' || 
                symbol == 'u' || 
                symbol == 'y' ||
                symbol == 'A' ||
                symbol == 'O' ||
                symbol == 'E' ||
                symbol == 'I' ||
                symbol == 'U' ||
                symbol == 'Y')
            {
                Console.WriteLine("vowel");
            }
            else
            {
                Console.WriteLine("other");
            }
        }
    }
}
