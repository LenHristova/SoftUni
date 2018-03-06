namespace P01_Vehicles.Models
{
    public class Car : Vehicle
    {
        public Car(double fuelQuantity, double baseFuelConsumptionInLitersPerKm, double technicalProblemsInfluenceIndex = 0) : base(fuelQuantity, baseFuelConsumptionInLitersPerKm, technicalProblemsInfluenceIndex)
        {
            ConditionerFuelConsumptionInLitersPerKm = 0.9;
        }
    }
}