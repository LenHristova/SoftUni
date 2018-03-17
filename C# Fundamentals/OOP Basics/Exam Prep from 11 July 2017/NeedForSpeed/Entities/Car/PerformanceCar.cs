using System.Collections.Generic;
using System.Text;

public class PerformanceCar : Car
{
    private const double INCREASING_HORSPOWER_INDEX = 1.5;
    private const double DECREASING_SUSPENSION_INDEX = 0.75;

    public PerformanceCar(string brand, string model, int yearOfProduction, int horsepower, int acceleration, int suspension, int durability) 
        : base(brand, model, yearOfProduction, (int)(horsepower*INCREASING_HORSPOWER_INDEX), acceleration, (int)(suspension * DECREASING_SUSPENSION_INDEX), durability)
    {
        AddOns = new List<string>();
    }

    private ICollection<string> AddOns { get; set; }

    public override void Tune(int tuneIndex, string addOn)
    {
        base.Tune(tuneIndex, addOn);
        AddOns.Add(addOn);
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        var addOnsResult = AddOns.Count == 0
            ? "None"
            : string.Join(", ", AddOns);

        sb.AppendLine(base.ToString())
            .AppendLine($"Add-ons: {addOnsResult}");

        return sb.ToString().TrimEnd();
    }
}