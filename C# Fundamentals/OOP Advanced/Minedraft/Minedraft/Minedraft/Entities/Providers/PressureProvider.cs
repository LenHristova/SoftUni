public class PressureProvider : Provider
{
    private const int DURABILITY_LOSS = 300;
    private const int ENERGY_OUTPUT_MULTIPLIER = 2;

    public PressureProvider(int id, double energyOutput) 
        : base(id, energyOutput)
    {
        EnergyOutput *= ENERGY_OUTPUT_MULTIPLIER;
        Durability -= DURABILITY_LOSS;
    }
}