using System;

public class TrafficLight
{
    public TrafficLight(string light)
    {
        SetCurrentLight(light);
    }

    private void SetCurrentLight(string light)
    {
        if (!Enum.TryParse(typeof(Light), light, out var currentLight))
        {
            throw new ArgumentException($"Invalid trafic light: {light}");
        }

        CurrentLight = (Light)currentLight;
    }

    public Light CurrentLight { get; private set; }

    public void NextLight()
    {
        CurrentLight = (Light)(((int)CurrentLight + 1) % 3);
    }

    public enum Light
    {
        Red,
        Green,
        Yellow
    }
}