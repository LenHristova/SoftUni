using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class RaceTower
{
    private readonly List<Driver> _activeDrivers;
    private readonly Stack<Driver> _failedDrivers;
    private Track _track;
    private TyreFactory _tyreFactory;
    private DriverFactory _driverFactory;

    public RaceTower()
    {
        _activeDrivers = new List<Driver>();
        _failedDrivers = new Stack<Driver>();
        _tyreFactory = new TyreFactory();
        _driverFactory = new DriverFactory();
    }

    public bool HasFinished => _track.LapsNumber == _track.CurrentLap;

    public void SetTrackInfo(int lapsNumber, int trackLength)
    {
        _track = new Track(lapsNumber, trackLength);
    }

    public void RegisterDriver(List<string> commandArgs)
    {
        try
        {
            var type = commandArgs[0];
            var name = commandArgs[1];
            var hp = int.Parse(commandArgs[2]);
            var fuelAmount = double.Parse(commandArgs[3]);

            var tyreParameters = commandArgs.Skip(4).ToList();
            var tyre = _tyreFactory.CreateTyre(tyreParameters);

            var car = new Car(hp, fuelAmount, tyre);
            var driver = _driverFactory.CreateDriver(type, name, car);
            _activeDrivers.Add(driver);
        }
        catch {}
    }

    public void DriverBoxes(List<string> commandArgs)
    {
        var reasonToBox = commandArgs[0];
        var driverName = commandArgs[1];

        var driver = _activeDrivers
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
                var tyre = _tyreFactory.CreateTyre(tyreParameters);
                driver.Car.ChangeTyres(tyre);
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
        var currentLapsCount = int.Parse(commandArgs[0]);

        if (currentLapsCount > _track.RemainingLaps)
        {
            return $"There is no time! On lap {_track.CurrentLap}.";
        }

        var output = CarsDrive(currentLapsCount);

        return output.Trim();
    }

    private string CarsDrive(int lapsCount)
    {
        var sb = new StringBuilder();

        for (int lap = 0; lap < lapsCount; lap++)
        {
            for (int i = 0; i < _activeDrivers.Count; i++)
            {
                var driver = _activeDrivers[i];
                try
                {
                    driver.Drive(_track.Length);
                }
                catch (ArgumentException argEx)
                {
                    DeactivatedDriver(driver, argEx.Message);
                    i--;
                }
            }

            _track.PassLap();
            DriversTriesForOvertaking(sb);
        }

        return sb.ToString().TrimEnd();
    }

    private void DriversTriesForOvertaking(StringBuilder sb)
    {
        var orderedDriversByTime = _activeDrivers
            .OrderByDescending(d => d.TotalTime)
            .ToList();

        for (int i = 0; i < orderedDriversByTime.Count - 1; i++)
        {
            var currentDriver = orderedDriversByTime[i];
            var nextDriver = orderedDriversByTime[i + 1];

            var timeDiff = currentDriver.TotalTime - nextDriver.TotalTime;

            if (timeDiff >= 0 && timeDiff <= 3)
            {
                bool hasOvertaking = TryToOvertake(currentDriver, nextDriver, timeDiff);
                if (hasOvertaking)
                {
                    i++;
                    sb.AppendLine($"{currentDriver.Name} has overtaken {nextDriver.Name} on lap {_track.CurrentLap}.");
                }
            }
        }
    }

    private void DeactivatedDriver(Driver driver, string failureReason)
    {
        _activeDrivers.Remove(driver);
        driver.Status = failureReason;
        _failedDrivers.Push(driver);
    }

    public string GetLeaderboard()
    {
        var sb = new StringBuilder();

        sb.AppendLine($"Lap {_track.CurrentLap}/{_track.LapsNumber}");

        var orderedDrivers = _activeDrivers
            .OrderBy(d => d.TotalTime)
            .Concat(_failedDrivers);

        var position = 1;
        foreach (var driver in orderedDrivers)
        {
            sb.AppendLine($"{position++} {driver}");
        }

        return sb.ToString().TrimEnd();
    }

    public string GetWinner()
    {
        var winner = _activeDrivers
            .OrderBy(d => d.TotalTime)
            .FirstOrDefault();

        return $"{winner?.Name} wins the race for {winner?.TotalTime:F3} seconds.";
    }

    public void ChangeWeather(List<string> commandArgs)
    {
        var weatherType = commandArgs[0];
        var isValidWeather = Enum.TryParse(typeof(Weather), weatherType, out var weather);
        if (!isValidWeather)
        {
            throw new ArgumentException("Invalid weather type.");
        }

        _track.ChangeWeather((Weather)weather);
    }

    private bool TryToOvertake(Driver currentDriver, Driver nextDriver, double timeDiff)
    {
        var overtakenTime = 3;
        switch (currentDriver)
        {
            case AggressiveDriver _ when currentDriver.Car.Tyre is UltrasoftTyre:
                    if (_track.Weather == Weather.Foggy)
                    {
                        DeactivatedDriver(currentDriver, "Crashed");
                        return false;
                    }
                    break;
            case EnduranceDriver _ when currentDriver.Car.Tyre is HardTyre:
                    if (_track.Weather == Weather.Rainy)
                    {
                        DeactivatedDriver(currentDriver, "Crashed");
                        return false;
                    }
                    break;
            default:
                if (timeDiff > 2)
                {
                    return false;
                }

                overtakenTime = 2;
                break;
        }

        Overtake(currentDriver, nextDriver, overtakenTime);
        return true;
    }

    private void Overtake(Driver currentDriver, Driver nextDriver, int overtakenTime)
    {
        currentDriver.Overtaking(overtakenTime);
        nextDriver.BeOvertaked(overtakenTime);
    }
}