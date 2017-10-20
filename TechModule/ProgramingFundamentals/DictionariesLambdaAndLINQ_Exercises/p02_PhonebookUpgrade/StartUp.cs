using System;
using System.Collections.Generic;
using System.Linq;

class StartUp
{
    static void Main()
    {
        SortedDictionary<string, string> phonebook = new SortedDictionary<string, string>();
        ExecuteCommands(phonebook);
    }

    private static void ExecuteCommands(SortedDictionary<string, string> phonebook)
    {
        string[] commandArgs = Console.ReadLine()
            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        while (commandArgs[0] != "END")
        {
            switch (commandArgs[0])
            {
                case "A":
                    AddNumber(phonebook, commandArgs, commandArgs[1]);
                    break;
                case "S":
                    SearchNumber(phonebook, commandArgs[1]);
                    break;
                case "ListAll":
                    PrintAllContacts(phonebook);
                    break;
            }

            commandArgs = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }

    static void PrintAllContacts(SortedDictionary<string, string> phonebook)
    {
        Console.WriteLine(string.Join(Environment.NewLine,
            phonebook.Select(c => $"{c.Key} -> {c.Value}")));
    }

    static void SearchNumber(SortedDictionary<string, string> phonebook, string name)
    {
        Console.WriteLine(phonebook.TryGetValue(name, out string number)
            ? $"{name} -> {number}"
            : $"Contact {name} does not exist.");
    }

    static void AddNumber(SortedDictionary<string, string> phonebook, string[] commandArgs, string name)
    {
        if (!phonebook.ContainsKey(name))
        {
            phonebook[name] = null;
        }
        phonebook[name] = commandArgs[2];
    }
}
