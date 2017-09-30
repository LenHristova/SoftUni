using System;

namespace p09_ReverseCharacters
{
    class ReverseCharacters
    {
        static void Main(string[] args)
        {
            char firstLetter = char.Parse(Console.ReadLine());
            char secondLetter = char.Parse(Console.ReadLine());
            char thirdLetter = char.Parse(Console.ReadLine());

            Console.WriteLine("" + thirdLetter + secondLetter + firstLetter);
        }
    }
}
