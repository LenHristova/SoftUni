using System;

namespace P02_PeopleDatabase
{
    public class Person :IEquatable<Person>
    {
        public Person(long id, string username)
        {
            Id = id;
            Username = username;
        }

        public long Id { get; private set; }

        public string Username { get; private set; }

        public bool Equals(Person other)
        {
            return this.Id == other.Id && this.Username == other.Username;
        }
    }
}
