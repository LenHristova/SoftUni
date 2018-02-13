using System;
using System.Linq;

class StartUp
{
    static void Main()
    {
        var numbers = Console.ReadLine()
            .Split(new[] {' ', '\t', '\n'}, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse);

        string command;
        while ((command = Console.ReadLine()) != "end")
        {
            if (command == "print")
            {
                Console.WriteLine(string.Join(" ", numbers));
                continue;
            }

            numbers = numbers
                .Select(Command(command));
        }
    }

    static Func<int, int> Command(string command)
    {
        switch (command)
        {
            case "add":
                return num => num + 1;
            case "multiply":
                return num => num * 2;
            case "subtract":
                return num => num - 1;
                default:
                    throw new NotSupportedException(@"Command must be ""add"", ""multiply"", ""subtract"" or ""print""!");
        }
    }
}