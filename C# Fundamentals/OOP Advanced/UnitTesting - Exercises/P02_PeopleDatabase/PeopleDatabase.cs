using P01_Database;
using System;
using System.Linq;

namespace P02_PeopleDatabase
{
    public class PeopleDatabase : Database<Person>
    {
        public PeopleDatabase(params Person[] persons) : base(persons)
        {
        }

        private static void EsureNotNullPerson(Person person)
        {
            if (person == null)
            {
                throw new ArgumentNullException();
            }
        }

        private static void EnsureNotNullUsername(string username)
        {
            if (username == null)
            {
                throw new ArgumentNullException();
            }
        }

        private static void EnsureValidId(long id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public override void Add(Person person)
        {
            EsureNotNullPerson(person);
            EnsureNotNullUsername(person.Username);
            EnsureValidId(person.Id);
            EnsurePersonNotExist(person);

            base.Add(person);
        }

        private void EnsurePersonNotExist(Person person)
        {
            var hasPersonWithThatUsernameOrId = _values
                .Take(_currentIndex)
                .Any(p => p.Username == person.Username || p.Id == person.Id);

            if (hasPersonWithThatUsernameOrId)
            {
                throw new InvalidOperationException("Invalid username or id.");
            }
        }

        public Person FindByUsername(string username)
        {
            EnsureNotNullUsername(username);

            var person = _values
                .Take(_currentIndex)
                .FirstOrDefault(p => p.Username == username);

            if (person == null)
            {
                throw new InvalidOperationException(
                    $"User with username: \"{username}\" doesn't exist in database.");
            }

            return person;
        }

        public Person FindById(long id)
        {
            EnsureValidId(id);

            var person = _values
                .Take(_currentIndex)
                .FirstOrDefault(p => p.Id == id);

            if (person == null)
            {
                throw new InvalidOperationException(
                    $"User with id: \"{id}\" doesn't exist in database.");
            }

            return person;
        }
    }
}
