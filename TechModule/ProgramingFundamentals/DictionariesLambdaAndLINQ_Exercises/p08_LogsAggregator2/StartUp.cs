using System;
using System.Collections.Generic;
using System.Linq;

class StartUp
{
    static void Main()
    {
        SortedDictionary<string, int> usersDurations = new SortedDictionary<string, int>();
        SortedDictionary<string, List<string>> usersIps = new SortedDictionary<string, List<string>>();

        int allSessions = int.Parse(Console.ReadLine());
        for (int currSession = 0; currSession < allSessions; currSession++)
        {
            string[] sessionParams = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            string ip = sessionParams[0];
            string user = sessionParams[1];
            int duration = int.Parse(sessionParams[2]);

            if (!usersDurations.ContainsKey(user))
            {
                usersDurations[user] = 0;
                usersIps[user] = new List<string>();
            }
            usersDurations[user] += duration;
            usersIps[user].Add(ip);
        }

        foreach (var userDuration in usersDurations)
        {
            string user = userDuration.Key;
            int duration = userDuration.Value;
            string ips = string.Join(", ", usersIps[user].Distinct().OrderBy(i => i));
            Console.WriteLine($"{user}: {duration} [{ips}]");
        }
    }
}
