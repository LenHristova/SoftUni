namespace P13_FamilyTree
{
    using System.Collections.Generic;
    using System.Linq;

    public class Person
    {
        public string Name { get; set; }

        public string Birthday { get; set; }

        public List<Person> Parents { get; set; }

        public List<Person> Children { get; set; }

        private Person()
        {
            Name = null;
            Birthday = null;
            Parents = new List<Person>();
            Children = new List<Person>();
        }

        public Person(string info) : this()
        {
            if (IsBirthday(info))
            {
                Birthday = info;
            }
            else
            {
                Name = info;
            }
        }

        public Person(string name, string birthday) : this()
        {
            Name = name;
            Birthday = birthday;
        }

        public override string ToString()
        {
            return $"{Name} {Birthday}";
        }

        private bool IsBirthday(string info)
        {
            return int.TryParse(info.First().ToString(), out int _);
        }
    }
}