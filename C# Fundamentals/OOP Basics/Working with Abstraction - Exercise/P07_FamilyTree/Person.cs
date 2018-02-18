using System;
using System.Collections.Generic;

namespace P07_FamilyTree
{
    public class Person
    {
        public string Name { get; set; }
        public string Birthday { get; set; }
        public List<Person> Parents { get; set; }
        public List<Person> Children { get; set; }

        private Person()
        {
            Children = new List<Person>();
            Parents = new List<Person>();
        }

        public Person(string input):this()
        {
            if (IsBirthday(input))
            {
                Birthday = input;
            }
            else
            {
                Name = input;
            }
        }

        public Person(string name, string birthday) : this()
        {
            Name = name;
            Birthday = birthday;
        }

        private bool IsBirthday(string input)
        {
            return Char.IsDigit(input[0]);
        }

        public override string ToString()
        {
            return $"{Name} {Birthday}";
        }
    }
}
