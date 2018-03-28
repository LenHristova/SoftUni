using System;

namespace P09_LinkedListTraversal
{
    public class StartUp
    {
        static void Main()
        {
            var linkedList = new LinkedList<int>();

            var numbersCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < numbersCount; i++)
            {
                var commandArgs = Console.ReadLine().Split();
                var command = commandArgs[0];
                var number = int.Parse(commandArgs[1]);

                if (command == "Add")
                {
                    linkedList.Add(number);
                }
                else
                {
                    linkedList.Remove(number);
                }
            }

            Console.WriteLine(linkedList.Count);
            Console.WriteLine(string.Join(" ", linkedList));
        }
    }
}