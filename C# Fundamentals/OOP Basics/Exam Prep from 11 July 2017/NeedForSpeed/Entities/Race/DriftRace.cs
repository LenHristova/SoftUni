public class DriftRace : Race
{
    public DriftRace(int length, string route, int prizePool) 
        : base(length, route, prizePool)
    {
    }

    public override int PerformancePoints(Car car)
    {
        return car.Suspension + car.Durability;
    }
}