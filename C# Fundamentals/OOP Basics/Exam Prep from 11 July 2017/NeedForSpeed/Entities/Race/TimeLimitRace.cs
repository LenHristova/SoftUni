public class TimeLimitRace : Race
{
    public int GoldTime { get; private set; }

    public TimeLimitRace(int length, string route, int prizePool, int goldTime) : base(length, route, prizePool)
    {
        GoldTime = goldTime;
    }

    public override int PerformancePoints(Car car)
    {
        return Length * ((car.Horsepower / 100) * car.Acceleration);
    }
}