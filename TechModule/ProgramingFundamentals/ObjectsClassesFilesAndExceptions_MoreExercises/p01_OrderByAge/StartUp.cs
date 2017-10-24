using System;
using System.Collections.Generic;
using System.Linq;

namespace p01_OrderByAge
{
    class Person
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return $"{Name} with ID: {Id} is {Age} years old.";
        }
    }
    class StartUp
    {
        static void Main(string[] args)
        {
            List<Person> persons = new List<Person>();
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "End")
                    break;

                string[] perseonInfo = input
                    .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

                persons.Add(new Person
                {
                    Name = perseonInfo[0],
                    Id = perseonInfo[1],
                    Age = int.Parse(perseonInfo[2])
                });
            }

            persons
                .OrderBy(p => p.Age)
                .ToList()
                .ForEach(p => Console.WriteLine(p));
        }
    }
}