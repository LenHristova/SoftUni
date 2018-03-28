using System;
using System.Linq;

namespace P03_Stack
{
    public class StartUp
    {
        static void Main()
        {
            var stack = new Stack<int>();

            string input;
            while ((input = Console.ReadLine()) != "END")
            {
                try
                {
                    ParseCommand(stack, input);
                }
                catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            foreach (var i in stack)
            {
                Console.WriteLine(i);
            }
            foreach (var i in stack)
            {
                Console.WriteLine(i);
            }
        }

        private static void ParseCommand(Stack<int> stack, string input)
        {
            var commandArgs = input.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            var command = commandArgs[0];
            if (command == "Push")
            {
                var intValues = commandArgs
                    .Skip(1)
                    .Select(int.Parse);
                foreach (var i in intValues)
                {
                    stack.Push(i);
                }
            }
            else if (command == "Pop")
            {
                stack.Pop();
            }
        }
    }
}