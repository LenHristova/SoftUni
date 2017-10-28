using System;
using System.Collections.Generic;

namespace Palindromes
{
    class StartUp
    {
        static void Main()
        {
            var words = Console.ReadLine()
                .Split(new[] { ' ', ',', '.', '?', '!' },
                    StringSplitOptions.RemoveEmptyEntries);
            var palindromes = new List<string>();
            
            foreach (var word in words)
            {
                var isPalindrome = true;
                for (var i = 0; i < word.Length / 2; i++)
                {
                    if (word[i] != word[word.Length - i - 1])
                    {
                        isPalindrome = false;
                        break;
                    }

                }
                if (isPalindrome && !palindromes.Contains(word))
                {
                    palindromes.Add(word);
                }
            }

            palindromes.Sort();

            Console.WriteLine(string.Join(", ", palindromes));
        }
    }
}
