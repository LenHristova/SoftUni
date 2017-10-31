using System;
using System.Linq;

namespace Ladybugs
{
    class StartUp
    {
        static void Main()
        {
            int[] fields = new int[int.Parse(Console.ReadLine())]
                .Select(f => 0)
                .ToArray();

            int[] ladybugsIndexes = Console.ReadLine()
                .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            FillFields(fields, ladybugsIndexes);

            while (true)
            {
                string input = Console.ReadLine();
                if (input == "end")
                    break;

                string[] commandArgs = input
                    .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                int index = int.Parse(commandArgs[0]);
                string direction = commandArgs[1];
                int flyLength = int.Parse(commandArgs[2]);

                if (index >= fields.Length || index < 0 || fields[index] != 1)
                {
                    continue;
                }

                fields[index] = 0;
                int newBugIndex = direction == "right"
                    ? index + flyLength
                    : index - flyLength;

                while (newBugIndex < fields.Length && newBugIndex > -1
                    && fields[newBugIndex] == 1)
                {
                    newBugIndex = direction == "right"
                        ? newBugIndex + flyLength
                        : newBugIndex - flyLength;
                }

                if (newBugIndex < fields.Length && newBugIndex > -1)
                {
                    fields[newBugIndex] = 1;
                }
            }

            Console.WriteLine(string.Join(" ", fields));
        }

        //put ladybugs to their index in fields array -> field with ladybugs become 1
        private static void FillFields(int[] fields, int[] ladybugsIndexes)
        {
            for (int index = 0; index < fields.Length; index++)
            {
                if (ladybugsIndexes.Contains(index))
                {
                    fields[index] = 1;
                }
            }
        }
    }
}