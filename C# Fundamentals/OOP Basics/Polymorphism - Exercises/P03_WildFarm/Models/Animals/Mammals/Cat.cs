using System.Collections.Generic;

public class Cat : Feline
{
    public Cat(string name, double weight, string livingRegion, string breed) 
        : base(name, weight, livingRegion, breed)
    {
    }

    public override HashSet<string> EatableFood => new HashSet<string>
    {
        "Vegetable",
        "Meat"
    };

    public override double WeightIncreasingByEating => 0.30;

    public override string AskForFood()
    {
        return "Meow";
    }
}