namespace P08_PetClinics.Models
{
    public class Pet : INameable
    {
        public Pet(string name, int age, string kind)
        {
            Name = name;
            Age = age;
            Kind = kind;
        }

        public string Name { get; }

        public int Age { get; }

        public string Kind { get; }

        public override string ToString()
        {
            return $"{Name} {Age} {Kind}";
        }
    }
}