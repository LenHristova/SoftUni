public class Car : Vehicle
{
    public Car(double fuelQuantity, double baseFuelConsumptionInLitersPerKm, double tankCapacity) : base(fuelQuantity, baseFuelConsumptionInLitersPerKm, tankCapacity)
    {
        ConditionerFuelConsumptionInLitersPerKm = 0.9;
    }
}