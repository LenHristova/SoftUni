namespace P12_Google
{
    using System.Collections.Generic;
    using System.Linq;

    public class Google
    {
        private List<Person> _people;

        public List<Person> People
        {
            get { return _people; }
            set { _people = value; }
        }

        public Google()
        {
            _people = new List<Person>();
        }

        public Person GetPersonInfo(string name)
        {
            return People.FirstOrDefault(p => p.Name == name);
        }

        public void AddPerson(Person person)
        {
            People.Add(person);
        }
    }
}