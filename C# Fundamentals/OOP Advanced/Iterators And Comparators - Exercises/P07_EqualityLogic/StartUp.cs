using System;
using System.Collections.Generic;

namespace P07_EqualityLogic
{
    public class StartUp
    {
        static void Main()
        {
            var persons1 = new SortedSet<Person>();
            var persons2 = new HashSet<Person>();

            var personsCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < personsCount; i++)
            {
                var personInfo = Console.ReadLine().Split();
                var person = new Person(
                    personInfo[0],
                    int.Parse(personInfo[1])
                );

                persons1.Add(person);
                persons2.Add(person);
            }

            Console.WriteLine(persons1.Count);
            Console.WriteLine(persons2.Count);
        }
    }
}