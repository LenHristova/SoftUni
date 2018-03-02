using System;

namespace P02_MultipleImplementation.Models
{
    public class Citizen : Person, IPerson, IBirthable, IIdentifiable
    {
        public DateTime Birthdate { get; private set; }
        public string Id { get; private set; }

        public Citizen(string name, int age, string id, DateTime birthdate) : base(name, age)
        {
            Birthdate = birthdate;
            Id = id;
        }
    }
}