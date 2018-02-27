using System;

namespace P06_Animals.ModelsAnimal
{
    public class Tomcat : Cat
    {
        private const string GENDER = "Male";

        public Tomcat(string name, int age) : base(name, age, GENDER)
        {
        }

        public override void ProduceSound()
        {
            Console.WriteLine("MEOW");
        }
    }
}