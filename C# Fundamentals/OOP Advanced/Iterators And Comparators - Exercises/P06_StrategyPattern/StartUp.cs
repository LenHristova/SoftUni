using System;
using System.Collections.Generic;

namespace P06_StrategyPattern
{
    public class StartUp
    {
        static void Main()
        {
            var personsSortedByName = new SortedSet<Person>(new PersonNameComparator());
            var personsSortedByAge = new SortedSet<Person>(new PersonAgeComparator());

            var personsCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < personsCount; i++)
            {
                var personInfo = Console.ReadLine().Split();
                var person = new Person(personInfo[0],int.Parse(personInfo[1]));

                personsSortedByName.Add(person);
                personsSortedByAge.Add(person);
            }

            Console.WriteLine(string.Join(Environment.NewLine, personsSortedByName));
            Console.WriteLine(string.Join(Environment.NewLine, personsSortedByAge));
        }
    }
}