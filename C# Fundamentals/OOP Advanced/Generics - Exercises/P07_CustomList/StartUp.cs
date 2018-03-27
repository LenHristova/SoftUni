using System;
using System.Collections.Generic;
using System.Text;

public class StartUp
{
    static void Main()
    {
        var m = new CustomList<int>(new List<int>{1,2,3});



        var commandInterpreter = new CommandInterpreter();
        var sb = new StringBuilder();

        string input;
        while ((input = Console.ReadLine()) != "END")
        {
            var result = commandInterpreter.ParseCommand(input);
            if (!string.IsNullOrWhiteSpace(result))
            {
                sb.AppendLine(result);
            }
        }

        Console.WriteLine(sb.ToString().TrimEnd());
    }
}