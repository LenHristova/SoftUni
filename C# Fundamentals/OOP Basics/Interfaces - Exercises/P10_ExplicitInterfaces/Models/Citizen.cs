using P10_ExplicitInterfaces.Contracts;

namespace P10_ExplicitInterfaces.Models
{
    public class Citizen : IResident, IPerson
    {
        private readonly string _name;

        public Citizen(string name, string country, int age)
        {
            _name = name;
            Country = country;
            Age = age;
        }

        public string Country { get; }

        public int Age { get; }

        string IPerson.GetName()
        {
            return _name;
        }

        string IResident.GetName()
        {
            return $"Mr/Ms/Mrs {_name}";
        }
    }
}