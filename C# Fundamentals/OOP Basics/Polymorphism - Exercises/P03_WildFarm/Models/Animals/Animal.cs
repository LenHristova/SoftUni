using System;
using System.Collections.Generic;

public abstract class Animal
{
    protected Animal(string name, double weight)
    {
        Name = name;
        Weight = weight;
        FoodEaten = 0;
    }

    public string Name { get; }

    public double Weight { get; private set; }

    public int FoodEaten { get; protected set; }

    public abstract HashSet<string> EatableFood { get;}

    public abstract double WeightIncreasingByEating { get; }

    public void TryEat(Food food)
    {
        var isEatableFood = EatableFood.Contains(food.Type);
        if (!isEatableFood)
        {
            throw new ArgumentException($"{GetType()} does not eat {food.Type}!");
        }

        FoodEaten += food.Quantity;
        Weight += food.Quantity * WeightIncreasingByEating;
    }

    public abstract string AskForFood();

    public override string ToString()
    {
        return $"{GetType()} [{Name}";
    }
}