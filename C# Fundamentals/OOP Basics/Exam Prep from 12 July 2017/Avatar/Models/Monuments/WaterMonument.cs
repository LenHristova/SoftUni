public class WaterMonument : Monument
{
    public WaterMonument(string name, int waterAffinity) : base(name)
    {
        WaterAffinity = waterAffinity;
    }

    public int WaterAffinity { get; private set; }

    public override int Affinity => WaterAffinity;

    public override string Type => GetType().Name.Replace(nameof(Monument), "");
}