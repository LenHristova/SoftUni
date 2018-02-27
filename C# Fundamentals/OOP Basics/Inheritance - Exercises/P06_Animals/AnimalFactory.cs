using System;
using P06_Animals.ModelsAnimal;

namespace P06_Animals
{
    public static class AnimalFactory
    {
        public static Animal CreateAnimal(string animal, string[] animalCharacteristics)
        {
            try
            {
                var name = animalCharacteristics[0];
                var age = int.Parse(animalCharacteristics[1]);
                string gender;
                switch (animal.ToLower())
                {
                    case "dog":
                        gender = animalCharacteristics[2];
                        return new Dog(name, age, gender);
                    case "cat":
                        gender = animalCharacteristics[2];
                        return new Cat(name, age, gender);
                    case "frog":
                        gender = animalCharacteristics[2];
                        return new Frog(name, age, gender);
                    case "kitten":
                        return new Kitten(name, age);
                    case "tomcat":
                        return new Tomcat(name, age);
                    default:
                        throw new InvalidInputException();
                }
            }
            catch (IndexOutOfRangeException)
            {
                throw new InvalidInputException();
            }
            catch (FormatException)
            {
                throw new InvalidInputException();
            }
        }

    }
}