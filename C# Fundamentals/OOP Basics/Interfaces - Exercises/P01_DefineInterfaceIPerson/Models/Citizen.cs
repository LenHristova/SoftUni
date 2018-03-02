namespace P01_DefineInterfaceIPerson.Models
{
    public class Citizen : Person, IPerson
    {
        public Citizen(string name, int age) : base(name, age)
        {
        }
    }
}