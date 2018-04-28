public class HammerHarvester : Harvester
{
    private const int ENERGY_REQUIREMENT_MULTIPLIER = 2;
    private const int ORE_OUTPUT_MULTIPLIER = 4;

    public HammerHarvester(int id, double oreOutput, double energyRequirement)
        : base(id, oreOutput, energyRequirement)
    {
        this.OreOutput *= ORE_OUTPUT_MULTIPLIER;
        this.EnergyRequirement *= ENERGY_REQUIREMENT_MULTIPLIER;
    }
}