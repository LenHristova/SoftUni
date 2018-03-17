using System.Collections.Generic;

public class Track
{
    public Track(int lapsNumber, int length)
    {
        LapsNumber = lapsNumber;
        Length = length;
        CurrentLap = 0;
        Weather = Weather.Sunny;
    }

    public int LapsNumber { get; }

    public int CurrentLap { get; private set; }

    public int RemainingLaps => LapsNumber - CurrentLap;

    public int Length { get; }

    public Weather Weather { get; private set; }

    public void PassLap()
    {
        CurrentLap++;
    }

    public void ChangeWeather(Weather weather)
    {
        Weather = weather;
    }
}
