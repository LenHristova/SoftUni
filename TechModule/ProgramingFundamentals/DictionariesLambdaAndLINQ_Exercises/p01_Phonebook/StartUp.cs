using System;
using System.Collections.Generic;

class StartUp
{
    static void Main()
    {
        Dictionary<string, string> phonebook = new Dictionary<string, string>();
        ExecuteCommands(phonebook);
    }

    private static void ExecuteCommands(Dictionary<string, string> phonebook)
    {
        string[] commandArgs = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        while (commandArgs[0] != "END")
        {
            string name = commandArgs[1];
            switch (commandArgs[0])
            {
                case "A":
                    AddNumber(phonebook, commandArgs, name);
                    break;
                case "S":
                    SearchNumber(phonebook, name);
                    break;
            }

            commandArgs = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }

    private static void SearchNumber(Dictionary<string, string> phonebook, string name)
    {
        Console.WriteLine(phonebook.TryGetValue(name, out string number)
            ? $"{name} -> {number}"
            : $"Contact {name} does not exist.");
    }

    private static void AddNumber(Dictionary<string, string> phonebook, string[] commandArgs, string name)
    {
        if (!phonebook.ContainsKey(name))
        {
            phonebook[name] = null;
        }
        phonebook[name] = commandArgs[2];
    }
}

