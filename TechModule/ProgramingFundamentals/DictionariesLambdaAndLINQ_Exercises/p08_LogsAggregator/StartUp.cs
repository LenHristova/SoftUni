using System;
using System.Collections.Generic;
using System.Linq;

class StartUp
{
    static void Main()
    {
        SortedDictionary<string, string[]> aggregatedLogs = new SortedDictionary<string, string[]>();

        int allSessions = int.Parse(Console.ReadLine());
        for (int currSession = 0; currSession < allSessions; currSession++)
        {
            string[] sessionParams = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            string ip = sessionParams[0];
            string user = sessionParams[1];
            int duration = int.Parse(sessionParams[2]);

            if (!aggregatedLogs.ContainsKey(user))
            {
                aggregatedLogs[user] = new string[2];
            }

            aggregatedLogs[user][0] += duration + " ";
            aggregatedLogs[user][1] += ip + " ";
        }

        foreach (var userLog in aggregatedLogs)
        {
            int allDuration = userLog.Value[0]
                .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse).Sum();
            string allIps = string.Join(", ", userLog.Value[1]
                .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                .Distinct()
                .OrderBy(i => i));
            Console.WriteLine($"{userLog.Key}: {allDuration} [{allIps}]");
        }
    }
}