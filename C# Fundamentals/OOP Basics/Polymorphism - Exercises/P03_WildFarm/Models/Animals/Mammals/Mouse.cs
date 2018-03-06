using System.Collections.Generic;

public class Mouse : Mammal
{
    public Mouse(string name, double weight, string livingRegion)
        : base(name, weight, livingRegion)
    {
    }

    public override HashSet<string> EatableFood => new HashSet<string>
    {
        "Vegetable",
        "Fruit"
    };

    public override double WeightIncreasingByEating => 0.10;

    public override string AskForFood()
    {
        return "Squeak";
    }
}