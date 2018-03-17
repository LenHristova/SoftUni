public class CircuitRace : Race
{
    public int Laps { get; private set; }

    public CircuitRace(int length, string route, int prizePool, int laps) : base(length, route, prizePool)
    {
        Laps = laps;
    }

    public override int PerformancePoints(Car car)
    {
        return (car.Horsepower / car.Acceleration) + (car.Suspension + car.Durability);
    }
}