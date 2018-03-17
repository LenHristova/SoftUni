public class AggressiveDriver : Driver
{
    private const double FUEL_CONSUMPTION_PER_KM = 2.7;

    public AggressiveDriver(string name, Car car) 
        : base(name, car, FUEL_CONSUMPTION_PER_KM)
    {
    }

    public override double Speed => base.Speed * 1.3;
}