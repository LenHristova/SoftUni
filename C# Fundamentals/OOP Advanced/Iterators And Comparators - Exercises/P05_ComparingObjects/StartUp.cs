using System;
using System.Collections.Generic;

namespace P05_ComparingObjects
{
    public class StartUp
    {
        static void Main()
        {
            var persons = new List<Person>();

            string input;
            while ((input = Console.ReadLine()) != "END")
            {
                var personInfo = input.Split();
                var person = new Person(
                    personInfo[0],
                    int.Parse(personInfo[1]),
                    personInfo[2]
                );

                persons.Add(person);
            }

            var personToCompareNumber = int.Parse(Console.ReadLine());

            var personToCompare = persons[personToCompareNumber - 1];

            var equals = persons.FindAll(p => p.CompareTo(personToCompare) == 0).Count;

            var result = equals <= 1
                ? "No matches"
                : $"{equals} {persons.Count-equals} {persons.Count}";

            Console.WriteLine(result);
        }
    }
}