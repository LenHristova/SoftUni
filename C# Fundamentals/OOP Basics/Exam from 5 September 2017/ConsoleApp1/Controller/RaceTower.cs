using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class RaceTower
{
    private readonly List<Driver> _activeDrivers;

    public RaceTower()
    {
        _activeDrivers = new List<Driver>();
        DisqualifyDrivers = new Stack<Driver>();
        Weather = Weather.Sunny;
        HasFinished = false;
    }

    public IEnumerable<Driver> ActiveDrivers => _activeDrivers.OrderBy(d => d.TotalTime);

    public Stack<Driver> DisqualifyDrivers { get; }

    public bool HasFinished { get; private set; }

    public Track Track { get; private set; }

    public Weather Weather { get; private set; }

    public void SetTrackInfo(int lapsNumber, int trackLength)
    {
        Track = new Track(lapsNumber, trackLength);
    }

    public void RegisterDriver(List<string> commandArgs)
    {
        try
        {
            _activeDrivers.Add(DriverFactory.CreateDriver(commandArgs));
        }
        catch (Exception ex)
        {
            //Console.WriteLine(ex.Message);
        }
    }

    public void DriverBoxes(List<string> commandArgs)
    {
        var reasonToBox = commandArgs[0];
        var driverName = commandArgs[1];

        var driver = ActiveDrivers
            .FirstOrDefault(d => d.Name == driverName);

        if (driver == null)
        {
            return;
        }

        driver.AddBoxTime();
        switch (reasonToBox)
        {
            case "ChangeTyres":
                var tyreParameters = commandArgs.Skip(2).ToList();
                driver.Car.ChangeTyres(tyreParameters);
                break;

            case "Refuel":
                var fuelAmount = double.Parse(commandArgs[2]);
                driver.Car.Refuel(fuelAmount);
                break;

            default:
                throw new NotImplementedException();
        }
    }

    public string CompleteLaps(List<string> commandArgs)
    {
        var lapsCount = int.Parse(commandArgs[0]);

        if (lapsCount > Track.TotalLaps - Track.PassedLaps)
        {
            throw new ArgumentException($"There is no time! On lap {Track.PassedLaps}.");
        }

        string output = CarsDrive(lapsCount);

        if (Track.PassedLaps == Track.TotalLaps)
        {
            HasFinished = true;
        }

        return output.Trim();
    }

    private string CarsDrive(int lapsCount)
    {
        var sb = new StringBuilder();

        for (int lap = 0; lap < lapsCount; lap++)
        {
            Track.PassLap();
            for (int i = 0; i < _activeDrivers.Count; i++)
            {
                var driver = _activeDrivers[i];
                try
                {
                    driver.Drive(Track);
                }
                catch (ArgumentException argEx)
                {
                    Disqualify(driver, argEx.Message);
                    i--;
                }
            }

            DriversTriedsForOvertaking(sb);
        }

        return sb.ToString().TrimEnd();
    }

    private void Disqualify(Driver driver, string failureReason)
    {
        _activeDrivers.Remove(driver);
        driver.Status = failureReason;
        DisqualifyDrivers.Push(driver);
    }

    public string GetLeaderboard()
    {
        var sb = new StringBuilder();

        sb.AppendLine($"Lap {Track.PassedLaps}/{Track.TotalLaps}");
        var position = 1;
        foreach (var driver in ActiveDrivers.Concat(DisqualifyDrivers))
        {
            sb.AppendLine($"{position++} {driver}");
        }

        return sb.ToString().TrimEnd();
    }

    public string GetWinner()
    {
        var winner = ActiveDrivers.FirstOrDefault();

        return $"{winner?.Name} wins the race for {winner?.TotalTime:F3} seconds.";
    }

    public void ChangeWeather(List<string> commandArgs)
    {
        Weather = (Weather)Enum.Parse(typeof(Weather), commandArgs[0]);
    }

    private void DriversTriedsForOvertaking(StringBuilder sb)
    {
        var orderedDriversByTime = _activeDrivers.OrderByDescending(d => d.TotalTime).ToList();

        for (int i = 0; i < orderedDriversByTime.Count - 1; i++)
        {
            var currentDriver = orderedDriversByTime[i];
            var nextDriver = orderedDriversByTime[i + 1];

            var timeDiff = Math.Abs(currentDriver.TotalTime - nextDriver.TotalTime);

            if (timeDiff > 3)
            {
                continue;
            }

            var driverType = currentDriver.GetType().Name;
            var tyreType = currentDriver.Car.Tyre.GetType().Name;

            var hasOvertaking = false;
            if (driverType == "AggressiveDriver"
                && tyreType == "UltrasoftTyre")

            {
                var dangerousWeather = Weather.Foggy;
                hasOvertaking = TryToOvertake(dangerousWeather, currentDriver, nextDriver);
            }
            else if (driverType == "EnduranceDriver"
                 && tyreType == "HardTyre")
            {
                var dangerousWeather = Weather.Rainy;
                hasOvertaking = TryToOvertake(dangerousWeather, currentDriver, nextDriver);
            }
            else if (timeDiff <= 2)
            {
                hasOvertaking = true;
                currentDriver.Overtaking(2);
                nextDriver.BeOvertaked(2);
            }

            if (hasOvertaking)
            {
                i++;
                sb.AppendLine($"{currentDriver.Name} has overtaken {nextDriver.Name} on lap {Track.PassedLaps}.");
            }
        }
    }

    private bool TryToOvertake(Weather dangerousWeather, Driver currentDriver, Driver nextDriver)
    {
        if (Weather == dangerousWeather)
        {
            Disqualify(currentDriver, "Crashed");
            return false;
        }

        currentDriver.Overtaking(3);
        nextDriver.BeOvertaked(3);
        return true;
    }
}