using System;

namespace P06_BirthdayCelebrations.Models
{
    public class Citizen : IIdentifiable, IBirthable
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime Birthdate { get; set; }

        public Citizen(string id, string name, int age, DateTime birthdate)
        {
            Id = id;
            Name = name;
            Age = age;
            Birthdate = birthdate;
        }
    }
}