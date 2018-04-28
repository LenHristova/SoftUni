public abstract class Ammunition : IAmmunition
{
    private const double WEAR_LEVEL_INDEX = 100;

    protected Ammunition(double weight)
    {
        Weight = weight;
        WearLevel = weight * WEAR_LEVEL_INDEX;
    }

    public string Name => GetType().Name;

    public double Weight { get; }

    public double WearLevel { get; private set; }

    public void DecreaseWearLevel(double wearAmount)
    {
        WearLevel -= wearAmount;
    }
}