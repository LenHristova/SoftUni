using System.Collections.Generic;

public class Dog : Mammal
{
    public Dog(string name, double weight, string livingRegion) 
        : base(name, weight, livingRegion)
    {
    }

    public override HashSet<string> EatableFood => new HashSet<string>
    {
        "Meat"
    };

    public override double WeightIncreasingByEating => 0.40;

    public override string AskForFood()
    {
        return "Woof!";
    }
}