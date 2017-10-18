using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p03_SafeManipulation
{
    class StartUp
    {
        static void Main()
        {
            string[] words = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            words = ExecuteCommands(words);
            Console.WriteLine(string.Join(", ", words));
        }

        private static string[] ExecuteCommands(string[] words)
        {
            while (true)
            {
                string[] commandParam = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();
                string command = commandParam[0];
                if (command == "END")
                {
                    break;
                }
                try
                {
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
                        default:
                            Console.WriteLine("Invalid input!");
                            break;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid input!");
                }
            }
            return words;
        }
    }
}
