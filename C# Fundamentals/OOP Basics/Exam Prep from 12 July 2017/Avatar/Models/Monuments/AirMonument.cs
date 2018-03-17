public class AirMonument : Monument
{
    public AirMonument(string name, int airAffinity) : base(name)
    {
        AirAffinity = airAffinity;
    }

    public int AirAffinity { get; private set; }

    public override int Affinity => AirAffinity;

    public override string Type => GetType().Name.Replace(nameof(Monument), "");
}