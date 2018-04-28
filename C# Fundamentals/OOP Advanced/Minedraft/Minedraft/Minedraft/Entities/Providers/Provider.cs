public abstract class Provider : Entity, IProvider
{
    protected Provider(int id, double energyOutput) 
        : base(id)
    {
        EnergyOutput = energyOutput;
    }

    public double EnergyOutput { get; protected set; }

    public override double Produce() => EnergyOutput;

    public void Repair(double val)
    {
        Durability += val;
    }
}