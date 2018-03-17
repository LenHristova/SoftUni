public class EarthMonument : Monument
{
    public EarthMonument(string name, int earthAffinity) : base(name)
    {
        EarthAffinity = earthAffinity;
    }

    public int EarthAffinity { get; private set; }

    public override int Affinity => EarthAffinity;

    public override string Type => GetType().Name.Replace(nameof(Monument), "");
}