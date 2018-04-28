public class SonicHarvester : Harvester
{
    private const int ENERGY_REQUIREMENT_DIVIDER = 2;
    private const int DURABILITY_LOSS = 300;

    public SonicHarvester(int id, double oreOutput, double energyRequirement)
        : base(id, oreOutput, energyRequirement)
    {
        this.EnergyRequirement /= ENERGY_REQUIREMENT_DIVIDER;
        this.Durability -= DURABILITY_LOSS;
    }
}