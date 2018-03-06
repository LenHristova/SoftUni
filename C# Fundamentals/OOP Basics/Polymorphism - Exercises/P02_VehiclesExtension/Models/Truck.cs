public class Truck : Vehicle
{
    public Truck(double fuelQuantity, double baseFuelConsumptionInLitersPerKm, double tankCapacity) : base(fuelQuantity, baseFuelConsumptionInLitersPerKm, tankCapacity)
    {
        ConditionerFuelConsumptionInLitersPerKm = 1.6;
    }
}