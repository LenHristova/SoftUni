using System;

namespace P05_ComparingObjects
{
    public class Person : IComparable<Person>
    {
        public Person(string name, int age, string town)
        {
            Name = name;
            Age = age;
            Town = town;
        }

        public string Name { get; }

        public int Age { get; }

        public string Town { get; }

        public int CompareTo(Person other)
        {
            var nameComparisson = this.Name.CompareTo(other.Name);
            if (nameComparisson != 0)
            {
                return nameComparisson;
            }

            var ageComparisson = this.Age.CompareTo(other.Age);
            if (ageComparisson != 0)
            {
                return ageComparisson;
            }

            return this.Town.CompareTo(other.Town);
        }
    }
}