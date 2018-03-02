using System;

namespace P06_BirthdayCelebrations.Models
{
    public class Pet : IBirthable
    {
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }

        public Pet(string name, DateTime birthdate)
        {
            Name = name;
            Birthdate = birthdate;
        }
    }
}