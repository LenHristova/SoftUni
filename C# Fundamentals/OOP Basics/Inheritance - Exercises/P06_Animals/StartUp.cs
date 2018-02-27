using System;
using System.Collections.Generic;
using P06_Animals.ModelsAnimal;

namespace P06_Animals
{
    public class StartUp
    {
        static void Main()
        {
            var animals = new List<Animal>();

            string input;
            while ((input = Console.ReadLine()) != "Beast!")
            {
                TryAddAnimal(animals, input);
            }

            foreach (var animal in animals)
            {
                Console.WriteLine(animal);
                animal.ProduceSound();
            }
        }

        private static void TryAddAnimal(List<Animal> animals, string input)
        {
            try
            {
                var animal = input;
                var animalCharacteristics = Console.ReadLine()
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries);
                animals.Add(AnimalFactory.CreateAnimal(animal, animalCharacteristics));
            }
            catch (InvalidInputException iie)
            {
                Console.WriteLine(iie.Message);
            }
        }
    }
}