using System.Collections.Generic;

public class Owl : Bird
{
    public Owl(string name, double weight, double wingSize) 
        : base(name, weight, wingSize)
    {
    }

    public override HashSet<string> EatableFood => new HashSet<string>
    {
        "Meat"
    };

    public override double WeightIncreasingByEating => 0.25;

    public override string AskForFood()
    {
        return "Hoot Hoot";
    }
}