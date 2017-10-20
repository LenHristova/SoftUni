using System;
using System.Collections.Generic;
using System.Linq;

class StartUp
{
    static void Main()
    {
        Dictionary<string, string> nameEmails = new Dictionary<string, string>();
        string input = Console.ReadLine();

        while (input != "stop")
        {
            string name = input;
            string email = Console.ReadLine();

            string domain = email.Split('.').Last();
            if (domain != "us" && domain != "uk")
            {
                if (!nameEmails.ContainsKey(name))
                {
                    nameEmails[name] = null;
                }
                nameEmails[name] = email;
            }
            input = Console.ReadLine();
        }

        foreach (var name in nameEmails)
        {
            Console.WriteLine($"{name.Key} -> {name.Value}");
        }
    }
}

