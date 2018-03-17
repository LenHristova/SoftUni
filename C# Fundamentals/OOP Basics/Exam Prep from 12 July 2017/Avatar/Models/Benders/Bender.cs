public abstract class Bender
{
    protected Bender(string name, int power)
    {
        Name = name;
        Power = power;
    }

    public string Name { get; private set; }

    public int Power { get; private set; }

    public abstract double TotalPower { get; }

    public abstract string Type { get; }

    public override string ToString()
    {
        return $"{Type} Bender: {Name}, Power: {Power}";
    }
}