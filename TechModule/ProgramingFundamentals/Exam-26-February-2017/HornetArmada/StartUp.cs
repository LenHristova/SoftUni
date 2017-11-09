using System;
using System.Collections.Generic;
using System.Linq;

namespace HornetArmada
{
    class StartUp
    {
        static void Main()
        {
            Dictionary<string, Legion> legions = new Dictionary<string, Legion>();

            long n = long.Parse(Console.ReadLine());
            for (long i = 0; i < n; i++)
            {
                string[] soldiersAndLegionsData = Console.ReadLine()
                    .Split(new[] { " = ", " -> ", ":" }, StringSplitOptions.RemoveEmptyEntries);

                UpdateLegionsInfo(legions, soldiersAndLegionsData);
            }

            string[] commandsArgs = Console.ReadLine()
                .Split(new[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);

            switch (commandsArgs.Length)
            {
                case 2:
                    PrintLegionsAndSoldiersCountOfGivenTypeByActivity(commandsArgs, legions);
                    break;
                case 1:
                    PrintLegionsAndActivityOfGivenTypeByActivity(commandsArgs, legions);
                    break;
            }
        }

        private static void PrintLegionsAndActivityOfGivenTypeByActivity(string[] commandsArgs, Dictionary<string, Legion> legions)
        {
            string soldierType = commandsArgs[0];
            foreach (var legion in legions
                .Where(l => l.Value.SoldersTypeAndCount.ContainsKey(soldierType))
                .OrderByDescending(l => l.Value.LastActivity))
            {
                Console.WriteLine($"{legion.Value.LastActivity} : {legion.Key}");
            }
        }

        private static void PrintLegionsAndSoldiersCountOfGivenTypeByActivity(string[] commandsArgs, Dictionary<string, Legion> legions)
        {
            long activity = long.Parse(commandsArgs[0]);
            string soldierType = commandsArgs[1];

            foreach (var legion in legions
                .Where(l => l.Value.SoldersTypeAndCount.ContainsKey(soldierType) && l.Value.LastActivity < activity)
                .OrderByDescending(l => l.Value.SoldersTypeAndCount[soldierType]))
            {
                Console.WriteLine($"{legion.Key} -> {legion.Value.SoldersTypeAndCount[soldierType]}");
            }
        }

        private static void UpdateLegionsInfo(Dictionary<string, Legion> legions, string[] soldiersAndLegionsData)
        {
            long activity = long.Parse(soldiersAndLegionsData[0]);
            string legionName = soldiersAndLegionsData[1];
            string soldierType = soldiersAndLegionsData[2];
            long soldierCount = long.Parse(soldiersAndLegionsData[3]);

            if (!legions.ContainsKey(legionName))
            {
                legions[legionName] = new Legion
                {
                    SoldersTypeAndCount = new Dictionary<string, long>(),
                    LastActivity = 0
                };
            }

            if (!legions[legionName].SoldersTypeAndCount.ContainsKey(soldierType))
            {
                legions[legionName].SoldersTypeAndCount[soldierType] = 0;
            }

            legions[legionName].SoldersTypeAndCount[soldierType] += soldierCount;

            if (activity > legions[legionName].LastActivity)
            {
                legions[legionName].LastActivity = activity;
            }
        }
    }
}