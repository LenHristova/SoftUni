public class SolarProvider : Provider
{
    private const int DURABILITY_INCREASING = 500;

    public SolarProvider(int id, double energyOutput)
        : base(id, energyOutput)
    {
        Durability += DURABILITY_INCREASING;
    }
}