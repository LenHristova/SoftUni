public class HammerHarvester : Harvester
{
    private const int INCREASE_ORE_OUTPUT_INDEX = 3;
    private const int INCREASE_ENERGY_REQUIREMENT_INDEX = 2;

    public HammerHarvester(string id, double oreOutput, double energyRequirement)
        : base(id, oreOutput * INCREASE_ORE_OUTPUT_INDEX, energyRequirement * INCREASE_ENERGY_REQUIREMENT_INDEX)
    {
    }

    public override string Type => GetType().Name.Replace(base.Type, "");
}