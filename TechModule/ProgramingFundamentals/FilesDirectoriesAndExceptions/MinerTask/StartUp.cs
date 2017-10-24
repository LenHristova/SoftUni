using System;
using System.Collections.Generic;
using System.IO;

namespace MinerTask
{
    class StartUp
    {
        static void Main()
        {
            File.Delete(@"..\..\output.txt");

            string[] lines = File.ReadAllLines(@"..\..\input.txt");

            var resoursesQuantity = new Dictionary<string, long>();
            for (var i = 0; i < lines.Length - 1; i += 2)
            {
                if (!resoursesQuantity.ContainsKey(lines[i]))
                {
                    resoursesQuantity[lines[i]] = 0L;
                }
                resoursesQuantity[lines[i]] += long.Parse(lines[i + 1]);
            }

            foreach (var resourseQuantity in resoursesQuantity)
            {
                var res = $"{resourseQuantity.Key} -> {resourseQuantity.Value}{Environment.NewLine}";
                File.AppendAllText(@"..\..\output.txt", res);
            }
        }
    }
}
