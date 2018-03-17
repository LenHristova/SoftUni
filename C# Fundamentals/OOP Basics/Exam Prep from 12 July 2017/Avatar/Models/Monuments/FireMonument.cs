public class FireMonument : Monument
{
    public FireMonument(string name, int fireAffinity) : base(name)
    {
        FireAffinity = fireAffinity;
    }

    public int FireAffinity { get; private set; }

    public override int Affinity => FireAffinity;

    public override string Type => GetType().Name.Replace(nameof(Monument), "");
}