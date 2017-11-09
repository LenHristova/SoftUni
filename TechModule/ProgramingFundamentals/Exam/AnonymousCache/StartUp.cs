using System;
using System.Collections.Generic;
using System.Linq;

namespace AnonymousCache
{
    class DataSet
    {
        public string Key { get; set; }
        public long Size { get; set; }
    }
    class StartUp
    {
        static void Main()
        {
            Dictionary<string, List<DataSet>> keySet = new Dictionary<string, List<DataSet>>();
            Dictionary<string, List<DataSet>> cache = new Dictionary<string, List<DataSet>>();

            while (true)
            {
                string input = Console.ReadLine();
                if (input == "thetinggoesskrra")
                    break;

                string[] commandArgs = input
                    .Split(new[] {" -> ", " | "}, StringSplitOptions.RemoveEmptyEntries);


                if (commandArgs.Length == 1)
                {
                    string dataSet = commandArgs[0];
                    if (!keySet.ContainsKey(dataSet))
                    {
                        keySet[dataSet] = new List<DataSet>();
                    }

                    if (cache.ContainsKey(dataSet))
                    {
                        keySet[dataSet] = cache[dataSet];
                    }
                }
                else
                {
                    string dataKey = commandArgs[0];
                    long dataSize = long.Parse(commandArgs[1]);
                    string dataSet = commandArgs[2];

                    if (keySet.ContainsKey(dataSet))
                    {
                        keySet[dataSet].Add(new DataSet
                        {
                            Key = dataKey,
                            Size = dataSize
                        });

                        continue;
                    }

                    if (!cache.ContainsKey(dataSet))
                    {
                        cache[dataSet] = new List<DataSet>();
                    }

                    cache[dataSet].Add(new DataSet
                    {
                        Key = dataKey,
                        Size = dataSize
                    });
                }
            }

            if (keySet.Any())
            {
                var bigest = keySet
                    .ToDictionary(x => x.Key, x => x.Value.Select(l => l.Size).Sum())
                    .OrderByDescending(d => d.Value)
                    .First();

                string bigestSumSyzeKye = bigest.Key;
                long bigestSum = bigest.Value;

                Console.WriteLine($"Data Set: {bigestSumSyzeKye}, Total Size: {bigestSum}");

                foreach (var dataSet in keySet[bigestSumSyzeKye])
                {
                    Console.WriteLine($"$.{dataSet.Key}");
                }
            }
        }
    }
}