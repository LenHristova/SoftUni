using System;
using System.Collections.Generic;
using System.Linq;

namespace P07_FamilyTree
{
    static class FamilyTreeBulder
    {
        private const string StopCommand = "End";

        public static List<Person> FamilyTree { get; set; } = new List<Person>();
        public static Person MainPerson { get; private set; }

        public static void AddMainPerson(string personInfo)
        {
            MainPerson = new Person(personInfo);
            FamilyTree.Add(MainPerson);
        }

        public static void Build()
        {
            string input;
            while ((input = Console.ReadLine()) != StopCommand)
            {
                if (input != null && input.Contains('-'))
                {
                    AddRelationships(input);
                }
                else
                {
                    AddPersonalInfo(input);
                }
            }
        }

        public static void DisplayInfo()
        {
            Console.WriteLine(MainPerson);
            Console.WriteLine("Parents:");
            foreach (var parent in MainPerson.Parents)
            {
                Console.WriteLine(parent);
            }
            Console.WriteLine("Children:");
            foreach (var child in MainPerson.Children)
            {
                Console.WriteLine(child);
            }
        }

        private static void AddPersonalInfo(string input)
        {
            var tokens = input.Split();
            var name = $"{tokens[0]} {tokens[1]}";
            var birthday = tokens[2];

            var personCopies = FamilyTree.Where(p => p.Name == name || p.Birthday == birthday).ToList();
            Person originalPerson;
            if (personCopies.Count == 0)
            {
                originalPerson = new Person(name, birthday);
                FamilyTree.Add(originalPerson);
                return;
            }

            originalPerson = personCopies.First();
            originalPerson.Name = name;
            originalPerson.Birthday = birthday;

            if (personCopies.Count == 2)
            {
                MergeCopies(personCopies, originalPerson);
            }
        }

        private static void MergeCopies(List<Person> personCopies, Person originalPerson)
        {
            var copyPerson = personCopies.Last();
            foreach (var parent in copyPerson.Parents)
            {
                var index = parent.Children.IndexOf(copyPerson);
                parent.Children[index] = originalPerson;
            }

            foreach (var child in copyPerson.Children)
            {
                var index = child.Parents.IndexOf(copyPerson);
                child.Parents[index] = originalPerson;
            }

            originalPerson.Parents.AddRange(copyPerson.Parents);
            originalPerson.Parents = originalPerson.Parents.Distinct().ToList();

            originalPerson.Children.AddRange(copyPerson.Children);
            originalPerson.Children = originalPerson.Children.Distinct().ToList();

            FamilyTree.Remove(copyPerson);
        }

        private static void AddRelationships(string input)
        {
            var tokens = input.Split(" - ");
            var parenInfo = tokens[0];
            var childInfo = tokens[1];

            var parent = GetPerson(FamilyTree, parenInfo);
            var child = GetPerson(FamilyTree, childInfo);

            parent.Children.Add(child);
            child.Parents.Add(parent);
        }

        private static Person GetPerson(List<Person> familyTree, string personInfo)
        {
            var person = familyTree.FirstOrDefault(p => p.Name == personInfo || p.Birthday == personInfo);
            if (person == null)
            {
                person = new Person(personInfo);
                familyTree.Add(person);
            }

            return person;
        }
    }
}
