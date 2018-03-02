using System;
using System.Collections.Generic;
using System.Linq;
using P05_BorderControl.Models;

namespace P05_BorderControl
{
    class StartUp
    {
        static void Main()
        {
            var inhabitants = new List<IIdentifiable>();

            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                var inhabitantInfo = input.Split();
                AddInhabitant(inhabitantInfo, inhabitants);
            }

            var fakeIdEnd = Console.ReadLine();
            var suspiciousInhabitants = inhabitants
                .Where(i => i.Id.EndsWith(fakeIdEnd))
                .Select(i => i.Id);

            Console.WriteLine(string.Join(Environment.NewLine, suspiciousInhabitants));
        }

        private static void AddInhabitant(string[] inhabitantInfo, List<IIdentifiable> inhabitants)
        {
            switch (inhabitantInfo.Length)
            {
                case 3:
                {
                    var name = inhabitantInfo[0];
                    var age = int.Parse(inhabitantInfo[1]);
                    var id = inhabitantInfo[2];
                    inhabitants.Add(new Citizen(id, name, age));
                    break;
                }
                case 2:
                {
                    var model = inhabitantInfo[0];
                    var id = inhabitantInfo[1];
                    inhabitants.Add(new Robot(id, model));
                    break;
                }
            }
        }
    }
}