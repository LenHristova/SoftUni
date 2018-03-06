using System;
using System.Collections.Generic;

public class StartUp
{
    static void Main()
    {
        var animals = new List<Animal>();

        string input;
        while ((input = Console.ReadLine()) != "End")
        {
            try
            {
                Animal animal = GetAnimal(input);
                animals.Add(animal);
                Console.WriteLine(animal.AskForFood());
                Food food = GetFood();
                animal.TryEat(food);
            }
            catch (ArgumentException argumentException)
            {
                Console.WriteLine(argumentException.Message);
            }
        }

        Console.WriteLine(string.Join(Environment.NewLine, animals));
    }

    private static Food GetFood()
    {
        var foodInfo = Console.ReadLine().Split();
        var foodType = foodInfo[0];
        var quantity = int.Parse(foodInfo[1]);
        var food = new Food(foodType, quantity);
        return food;
    }

    private static Animal GetAnimal(string input)
    {
        var animalInfo = input.Split();
        var animal = AnimalFactory.CreateAnimal(animalInfo);
        return animal;
    }
}