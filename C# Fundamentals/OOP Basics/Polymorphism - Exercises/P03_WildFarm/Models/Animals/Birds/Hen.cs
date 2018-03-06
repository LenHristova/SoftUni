using System.Collections.Generic;

public class Hen : Bird
{
    public Hen(string name, double weight, double wingSize) 
        : base(name, weight, wingSize)
    {
    }

    public override HashSet<string> EatableFood => new HashSet<string>
    {
        "Vegetable",
        "Fruit",
        "Meat",
        "Seeds"
    };

    public override double WeightIncreasingByEating => 0.35;

    public override string AskForFood()
    {
        return "Cluck";
    }
}