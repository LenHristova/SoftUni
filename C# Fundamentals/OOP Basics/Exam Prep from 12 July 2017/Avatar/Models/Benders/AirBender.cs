public class AirBender : Bender
{
    public AirBender(string name, int power, double aerialIntegrity) : base(name, power)
    {
        AerialIntegrity = aerialIntegrity;
    }

    public double AerialIntegrity { get; private set; }

    public override double TotalPower => Power * AerialIntegrity;

    public override string Type => GetType().Name.Replace(nameof(Bender), "");

    public override string ToString()
    {
        return $"{base.ToString()}, Aerial Integrity: {AerialIntegrity:F2}";
    }
}