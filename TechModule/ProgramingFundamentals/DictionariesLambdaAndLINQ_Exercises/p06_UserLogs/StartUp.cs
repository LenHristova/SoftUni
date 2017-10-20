using System;
using System.Collections.Generic;
using System.Linq;

class StartUp
{
    static void Main()
    {
        SortedDictionary<string, Dictionary<string, int>> usersIps = new SortedDictionary<string, Dictionary<string, int>>();

        string input = Console.ReadLine();

        while (input != "end")
        {
            string[] argStrings = input
                .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            string ip = argStrings[0]
                .Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries)
                .Last();
            string user = argStrings[2]
                .Split(new[] {'='}, StringSplitOptions.RemoveEmptyEntries)
                .Last();

            if (!usersIps.ContainsKey(user))
            {
                usersIps[user] = new Dictionary<string, int>();
            }
            if (!usersIps[user].ContainsKey(ip))
            {
                usersIps[user][ip] = 0;
            }
            usersIps[user][ip]++;

            input = Console.ReadLine();
        }

        foreach (var userIps in usersIps)
        {
            string user = userIps.Key;
            Console.WriteLine($"{user}:");
            string[] ipsCount = userIps.Value
                .Select(x => $"{x.Key} => {x.Value}")
                .ToArray();
            Console.WriteLine($"{string.Join(", ", ipsCount)}.");
        }
    }
}