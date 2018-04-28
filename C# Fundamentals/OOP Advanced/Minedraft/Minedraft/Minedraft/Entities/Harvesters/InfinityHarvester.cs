public class InfinityHarvester : Harvester
{
    private const int ORE_OUTPUT_DIVIDER = 10;

    private double _durability;

    public InfinityHarvester(int id, double oreOutput, double energyRequirement) 
        : base(id, oreOutput, energyRequirement)
    {
        this.OreOutput /= ORE_OUTPUT_DIVIDER;
    }

    public override double Durability
    {
        get => _durability;
        protected set => _durability = INITIAL_DURABILITY;
    }
}