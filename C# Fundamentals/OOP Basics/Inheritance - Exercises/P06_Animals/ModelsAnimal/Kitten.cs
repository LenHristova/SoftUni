using System;

namespace P06_Animals.ModelsAnimal
{
    public class Kitten : Cat
    {
        private const string GENDER = "Female";

        public Kitten(string name, int age) : base(name, age, GENDER)
        {        
        }

        public override void ProduceSound()
        {
            Console.WriteLine("Meow");
        }
    }
}