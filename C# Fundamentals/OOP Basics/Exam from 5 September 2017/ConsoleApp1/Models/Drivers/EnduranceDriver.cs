public class EnduranceDriver : Driver
{
    private const double FUEL_CONSUMPTION_PER_KM = 1.5;

    public EnduranceDriver(string name, Car car) 
        : base(name, car, FUEL_CONSUMPTION_PER_KM)
    {
    }
}