public class FireBender : Bender
{
    public FireBender(string name, int power, double heatAggression) : base(name, power)
    {
        HeatAggression = heatAggression;
    }

    public double HeatAggression { get; private set; }

    public override double TotalPower => Power * HeatAggression;

    public override string Type => GetType().Name.Replace(nameof(Bender), "");

    public override string ToString()
    {
        return $"{base.ToString()}, Heat Aggression: {HeatAggression:F2}";
    }
}