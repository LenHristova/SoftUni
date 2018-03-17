public abstract class Monument
{
    protected Monument(string name)
    {
        Name = name;
    }

    public string Name { get; private set; }

    public abstract int Affinity { get; }

    public abstract string Type { get; }

    public override string ToString()
    {
        return $"{Type} Monument: {Name}, {Type} Affinity: {Affinity}";
    }
}