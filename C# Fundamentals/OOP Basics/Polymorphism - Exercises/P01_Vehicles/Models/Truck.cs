namespace P01_Vehicles.Models
{
    public class Truck : Vehicle
    {
        public Truck(double fuelQuantity, double baseFuelConsumptionInLitersPerKm, double technicalProblemsInfluenceIndex = 1) : base(fuelQuantity, baseFuelConsumptionInLitersPerKm, technicalProblemsInfluenceIndex)
        {
            ConditionerFuelConsumptionInLitersPerKm = 1.6;
        }
    }
}