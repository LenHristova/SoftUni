using System.Collections.Generic;

public abstract class Race
{
    protected Race(int length, string route, int prizePool)
    {
        Length = length;
        Route = route;
        PrizePool = prizePool;
        Participants = new List<Car>();
    }

    public int Length { get; protected set; }
    public string Route { get; protected set; }
    public int PrizePool { get; protected set; }
    public ICollection<Car> Participants { get; protected set; }

    public abstract int PerformancePoints(Car car);

    public void AddParticipant(Car car)
    {
        Participants.Add(car);
    }
}