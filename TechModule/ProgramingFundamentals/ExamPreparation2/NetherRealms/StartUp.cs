using System;
using System.Collections.Generic;
using System.Linq;

namespace NetherRealms
{
    class StartUp
    {
        static void Main()
        {
            string[] demonsNames = Console.ReadLine()
                .Split(new[] { ',', ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            List<Demon> demons = new Demon[demonsNames.Length]
                .Select((d, index) => new Demon { Name = demonsNames[index] })
                .ToList();
            
            Console.WriteLine(string.Join(Environment.NewLine, demons.OrderBy(d => d.Name)));
        }
    }
}