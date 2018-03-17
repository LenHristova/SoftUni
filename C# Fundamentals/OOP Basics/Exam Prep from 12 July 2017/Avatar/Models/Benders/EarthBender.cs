public class EarthBender:Bender
{
    public EarthBender(string name, int power, double groundSaturation) : base(name, power)
    {
        GroundSaturation = groundSaturation;
    }

    public double GroundSaturation { get; private set; }

    public override double TotalPower => Power * GroundSaturation;

    public override string Type => GetType().Name.Replace(nameof(Bender), "");

    public override string ToString()
    {
        return $"{base.ToString()}, Ground Saturation: {GroundSaturation:F2}";
    }
}