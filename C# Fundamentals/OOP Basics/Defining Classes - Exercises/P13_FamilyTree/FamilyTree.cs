namespace P13_FamilyTree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class FamilyTree
    {
        public List<Person> Persons { get; set; }

        public FamilyTree()
        {
            Persons = new List<Person>();
        }

        public void UpdateFamilyTieInfo(string input)
        {
            var commandParams = input.Split(" - ");
            var parentInfo = commandParams[0];
            var childInfo = commandParams[1];

            Person parent = FirstOrDefaultInFamilyTree(parentInfo);
            Person child = FirstOrDefaultInFamilyTree(childInfo);

            if (parent == null)
            {
                parent = new Person(parentInfo);
                Persons.Add(parent);
            }
            if (child == null)
            {
                child = new Person(childInfo);
                Persons.Add(child);
            }

            parent.Children.Add(child);
            child.Parents.Add(parent);
        }

        private Person FirstOrDefaultInFamilyTree(string personInfo)
        {
            return IsBirthday(personInfo)
                ? FindPersonByBirthday(personInfo)
                : FindPersonByName(personInfo);
        }

        private bool IsBirthday(string info)
        {
            return int.TryParse(info.First().ToString(), out int _);
        }

        private Person FindPersonByBirthday(string birthday)
        {
            return Persons.FirstOrDefault(person => person.Birthday == birthday);
        }

        private Person FindPersonByName(string name)
        {
            return Persons.FirstOrDefault(person => person.Name == name);
        }

        public void UpdatePersonInfo(string input)
        {
            var commandParams = input.Split();
            if (commandParams.Length != 3) return;

            var name = commandParams[0] + " " + commandParams[1];
            var birthday = commandParams[2];

            var personWithThatName = FindPersonByName(name);
            var personWithThatBirthday = FindPersonByBirthday(birthday);

            var hasPersonWithThatName = personWithThatName != null;
            var hasPersonWithThatBirthday = personWithThatBirthday != null;

            if (!hasPersonWithThatName && !hasPersonWithThatBirthday)
            {
                Persons.Add(new Person(name, birthday));
            }
            else if (hasPersonWithThatName && hasPersonWithThatBirthday)
            {
                MergePersonInfo(birthday, personWithThatName, personWithThatBirthday);
            }
            else if (hasPersonWithThatName)
            {
                personWithThatName.Birthday = birthday;
            }
            else
            {
                personWithThatBirthday.Name = name;
            }
        }

        private void MergePersonInfo(string birthday, Person personFirstCopy, Person personSecondCopy)
        {
            foreach (var parent in personSecondCopy.Parents)
            {
                var index = parent.Children.IndexOf(personSecondCopy);
                parent.Children[index] = personFirstCopy;
            }

            foreach (var child in personSecondCopy.Children)
            {
                var index = child.Parents.IndexOf(personSecondCopy);
                child.Parents[index] = personFirstCopy;
            }

            personFirstCopy.Parents.AddRange(personSecondCopy.Parents);
            personFirstCopy.Parents = personFirstCopy.Parents
                .Distinct()
                .ToList();
            personFirstCopy.Children.AddRange(personSecondCopy.Children);
            personFirstCopy.Children = personFirstCopy.Children
                .Distinct()
                .ToList();
            personFirstCopy.Birthday = birthday;

            Persons.Remove(personSecondCopy);
        }

        public void DisplayInfoFor(string personInfo)
        {
            var person = FirstOrDefaultInFamilyTree(personInfo);

            if (person != null)
            {
                Console.WriteLine(person);
                Console.WriteLine("Parents:");
                foreach (var parent in person.Parents)
                {
                    Console.WriteLine(parent);
                }
                Console.WriteLine("Children:");
                foreach (var child in person.Children)
                {
                    Console.WriteLine(child);
                }
            }
        }
    }
}