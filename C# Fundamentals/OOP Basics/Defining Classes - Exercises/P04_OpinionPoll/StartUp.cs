namespace P04_OpinionPoll
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class StartUp
    {
        static void Main()
        {
            var people = new List<Person>();

            var peopleCount = int.Parse(Console.ReadLine());
            for (int person = 0; person < peopleCount; person++)
            {
                var personInfo = Console.ReadLine()?.Split();
                if (personInfo == null || personInfo.Length != 2) continue;

                var personName = personInfo[0];
                var personAge = int.Parse(personInfo[1]);
                people.Add(new Person(personName, personAge));
            }

            //Search people whose age is more than 30 years, sorted in alphabetical order
            var resultList = people.Where(p => p.Age > 30)
                .OrderBy(p => p.Name);
            Console.WriteLine(string.Join(Environment.NewLine, resultList));

        }
    }
}