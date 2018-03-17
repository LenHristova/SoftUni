public class WaterBender : Bender
{
    public WaterBender(string name, int power, double waterClarity) : base(name, power)
    {
        WaterClarity = waterClarity;
    }

    public double WaterClarity { get; private set; }

    public override double TotalPower => Power * WaterClarity;

    public override string Type => GetType().Name.Replace(nameof(Bender), "");

    public override string ToString()
    {
        return $"{base.ToString()}, Water Clarity: {WaterClarity:F2}";
    }
}