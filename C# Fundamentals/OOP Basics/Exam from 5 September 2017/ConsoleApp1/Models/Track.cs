public class Track
{
    public Track(int totalLaps, int length)
    {
        TotalLaps = totalLaps;
        Length = length;
        PassedLaps = 0;
    }

    public int TotalLaps { get; }

    public int PassedLaps { get; private set; }

    public int Length { get; }

    public void PassLap()
    {
        PassedLaps++;
    }
}
