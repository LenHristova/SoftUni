using System.Collections.Generic;

public class Tiger : Feline
{
    public Tiger(string name, double weight, string livingRegion, string breed) 
        : base(name, weight, livingRegion, breed)
    {
    }

    public override HashSet<string> EatableFood => new HashSet<string>
    {
        "Meat"
    };

    public override double WeightIncreasingByEating => 1.00;

    public override string AskForFood()
    {
        return "ROAR!!!";
    }
}