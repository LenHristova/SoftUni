using System;

namespace P06_Animals.ModelsAnimal
{
    public class Animal:ISoundProducable
    {
        private string _name;
        private int _age;
        private string _gender;

        private string Name { get; }
    
        private int Age
        {
            get => _age;
            set
            {
                if (value < 0)
                {
                    throw new InvalidInputException();
                }

                _age = value;
            }
        }
    
        private string Gender
        {
            get => _gender;
            set
            {
                if (value.ToLower() != "female" && value.ToLower() != "male")
                {
                    throw new InvalidInputException();
                }

                _gender = value;
            }
        }

        public Animal(string name, int age, string gender)
        {
            Name = name;
            Age = age;
            Gender = gender;
        }

        public virtual void ProduceSound()
        {
            Console.WriteLine("Rrrrrr!");
        }

        public override string ToString()
        {
            return $"{GetType().Name}{Environment.NewLine}" +
                   $"{Name} {Age} {Gender}";
        }
    }
}