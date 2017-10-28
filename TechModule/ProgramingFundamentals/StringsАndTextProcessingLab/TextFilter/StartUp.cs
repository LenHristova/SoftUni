using System;

namespace TextFilter
{
    class StartUp
    {
        static void Main()
        {
            var banedWords = Console.ReadLine()
                .Split(new[] {' ', ','}, StringSplitOptions.RemoveEmptyEntries);
            var text = Console.ReadLine();

            foreach (var banedWord in banedWords)
            {
                text = text.Replace(banedWord, new string('*', banedWord.Length));
            }
            Console.WriteLine(text);
        }
    }
}