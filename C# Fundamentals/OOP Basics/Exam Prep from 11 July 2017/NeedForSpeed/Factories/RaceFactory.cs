using System;

public static class RaceFactory
{
    public static Race CreateRace(string type, int length, string route, int prizePool, int extraParam = 0)
    {

        switch (type)
        {
            case "Casual":
                return new CasualRace(length, route, prizePool);
            case "Drag":
                return new DragRace(length, route, prizePool);
            case "Drift":
                return new DriftRace(length, route, prizePool);
            case "TimeLimit":
                var goldTime = extraParam;
                return new TimeLimitRace(length, route, prizePool, goldTime);
            case "Circuit":
                var laps = extraParam;
                return new CircuitRace(length, route, prizePool, laps);
        }

        throw new NotImplementedException();
    }
}