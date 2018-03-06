public class Bus : Vehicle
{
    public Bus(double fuelQuantity, double baseFuelConsumptionInLitersPerKm, double tankCapacity) 
        : base(fuelQuantity, baseFuelConsumptionInLitersPerKm, tankCapacity)
    {
        ConditionerFuelConsumptionInLitersPerKm = 1.4;
    }

    public string DriveEmpty(double distance)
    {
        var neededFuel = distance * BaseFuelConsumptionInLitersPerKm;

        return TryToDrive(distance, neededFuel);
    }
}