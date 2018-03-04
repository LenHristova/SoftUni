public class PressureProvider : Provider
{
    private const double INCREASE_ENERGY_OUTPUT_INDEX = 1.5;

    public PressureProvider(string id, double energyOutput)
        : base(id, energyOutput * INCREASE_ENERGY_OUTPUT_INDEX)
    {
    }
}