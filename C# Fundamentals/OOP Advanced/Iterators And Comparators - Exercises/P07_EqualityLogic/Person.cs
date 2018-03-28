using System;

namespace P07_EqualityLogic
{
    public class Person : IComparable<Person> 
    {
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public string Name { get; }

        public int Age { get; }

        public int CompareTo(Person other)
        {
            var nameComparisson = Name.CompareTo(other.Name);
            if (nameComparisson == 0)
            {
                return Age.CompareTo(other.Age);
            }

            return nameComparisson;
        }

        public override string ToString()
        {
            return $"{Name} {Age}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Person other)
            {
                return this.CompareTo(other) == 0;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() + Age.GetHashCode();
        }
    }
}