public abstract class Harvester : Entity, IHarvester
{
    public double OreOutput { get; protected set; }

    public double EnergyRequirement { get; protected set; }

    protected Harvester(int id, double oreOutput, double energyRequirement)
    : base(id)
    {
        OreOutput = oreOutput;
        EnergyRequirement = energyRequirement;
    }

    public override double Produce() => OreOutput;
}