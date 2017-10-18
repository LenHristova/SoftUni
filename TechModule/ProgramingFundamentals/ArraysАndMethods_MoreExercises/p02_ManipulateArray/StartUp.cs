using System;
using System.Linq;

namespace p02_ManipulateArray
{
    class StartUp
    {
        static void Main()
        {
            string[] words = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            int commandCount = int.Parse(Console.ReadLine());

            words = ExecuteCommands(words, commandCount);
            Console.WriteLine(string.Join(", ", words));
        }

        private static string[] ExecuteCommands(string[] words, int commandCount)
        {
            for (int i = 0; i < commandCount; i++)
            {
                string[] commandParam = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();
                string command = commandParam[0];

                switch (command)
                {
                    case "Reverse":
                        words = words.Reverse().ToArray();
                        break;
                    case "Distinct":
                        words = words.Distinct().ToArray();
                        break;
                    case "Replace":                      
                        int index = int.Parse(commandParam[1]);
                        string newWord = commandParam[2];
                        words[index] = newWord;
                        break;
                }
            }
            return words;
        }
    }
}