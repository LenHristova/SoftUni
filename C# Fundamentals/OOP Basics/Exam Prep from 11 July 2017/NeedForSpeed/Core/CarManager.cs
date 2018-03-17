using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CarManager
{
    private Dictionary<int, Race> Races { get; set; }
    private Dictionary<int, Car> Cars { get; set; }
    private Garage Garage { get; set; }
    private Dictionary<int, HashSet<int>> OpenedRaces { get; set; }
    private ICollection<int> CarsInGarage { get; set; }

    public CarManager()
    {
        Races = new Dictionary<int, Race>();
        Cars = new Dictionary<int, Car>();
        Garage = new Garage();
        OpenedRaces = new Dictionary<int, HashSet<int>>();
        CarsInGarage = new HashSet<int>();
    }

    public void Register(int id, string type, string brand, string model, int yearOfProduction, int horsepower,
        int acceleration, int suspension, int durability)
    {
        if (!Cars.ContainsKey(id))
        {
            var car = CarFactory.CreateCar(type, brand, model, yearOfProduction, horsepower,
                acceleration, suspension, durability);
            Cars.Add(id, car);
        }
    }

    public string Check(int id)
    {
        return Cars.ContainsKey(id)
            ? Cars[id].ToString()
            : null;
    }

    public void Open(int id, string type, int length, string route, int prizePool)
    {
        if (!Races.ContainsKey(id))
        {
            var race = RaceFactory.CreateRace(type, length, route, prizePool);
            Races.Add(id, race);

            OpenedRaces.Add(id, new HashSet<int>());
        }
    }

    public void Open(int id, string type, int length, string route, int prizePool, int extraParam)
    {
        if (!Races.ContainsKey(id))
        {
            var race = RaceFactory.CreateRace(type, length, route, prizePool, extraParam);
            Races.Add(id, race);

            OpenedRaces.Add(id, new HashSet<int>());
        }
    }

    public void Participate(int carId, int raceId)
    {
        if (OpenedRaces.ContainsKey(raceId) &&
            Cars.ContainsKey(carId) && !IsInGarage(carId))
        {
            var race = Races[raceId];
            if (race is TimeLimitRace && race.Participants.Count > 0)
            {
                return;
            }

            race.AddParticipant(Cars[carId]);
            OpenedRaces[raceId].Add(carId);
        }
    }

    private bool IsInGarage(int carId)
    {
        return CarsInGarage.Contains(carId);
    }

    public string Start(int id)
    {
        if (!OpenedRaces.ContainsKey(id))
        {
            return null;
        }

        var race = Races[id];
        if (race.Participants.Count == 0)
        {
            return "Cannot start the race with zero participants.";
        }

        var sb = new StringBuilder();

        switch (race)
        {
            case TimeLimitRace timeLimitRace:
                DriveTimeLimit(sb, race, timeLimitRace);
                break;
            case CircuitRace circuitRace:
                DriveCircuitRace(sb, race, circuitRace);
                break;
            default:
                DriveRace(sb, race);
                break;
        }

        OpenedRaces.Remove(id);

        return sb.ToString().TrimEnd();
    }

    private void DriveRace(StringBuilder sb, Race race)
    {
        sb.AppendLine($"{race.Route} - {race.Length}");

        const int winnersCount = 3;
        var participants = GetWinners(race, winnersCount);

        var position = 1;
        foreach (var participant in participants)
        {
            var car = participant.Key;
            var performancePoints = participant.Value;
            Enum performance = (RacePerformance)position;
            var prize = Prize(race.PrizePool, performance);

            sb.AppendLine($"{position}. {car.Brand} {car.Model} {performancePoints}PP - ${prize}");
            position++;
        }
    }

    private void DriveCircuitRace(StringBuilder sb, Race race, CircuitRace circuitRace)
    {
        for (int lap = 0; lap < circuitRace.Laps; lap++)
        {
            foreach (var car in race.Participants)
            {
                car.DecreaseDurability(circuitRace.Length);
            }
        }

        var circuitRaceLength = circuitRace.Length * circuitRace.Laps;
        sb.AppendLine($"{circuitRace.Route} - {circuitRaceLength}");

        const int winnersCount = 4;
        var participants = GetWinners(race, winnersCount);

        var position = 1;
        foreach (var participant in participants)
        {
            var car = participant.Key;
            var performancePoints = participant.Value;
            Enum circuitRacePerformance = (CircuitRacePerformance)position;

            var prize = Prize(race.PrizePool, circuitRacePerformance);

            sb.AppendLine($"{position}. {car.Brand} {car.Model} {performancePoints}PP - ${prize}");
            position++;
        }
    }

    private static IEnumerable<KeyValuePair<Car, int>> GetWinners(Race race, int winnersCount)
    {
        return race.Participants
            .ToDictionary(p => p, race.PerformancePoints)
            .OrderByDescending(kvp => kvp.Value)
            .Take(winnersCount);
    }

    private void DriveTimeLimit(StringBuilder sb, Race race, TimeLimitRace timeLimitRace)
    {
        sb.AppendLine($"{race.Route} - {race.Length}");
        var car = race.Participants.First();
        var performancePoints = race.PerformancePoints(car);
        var participantEarnedTime = PrizeTimeLimitRace(performancePoints, timeLimitRace);
        var timeLimirPerformance = (Enum)Enum.Parse(typeof(TimeLimitPerformance), participantEarnedTime);
        var prize = Prize(race.PrizePool, timeLimirPerformance);

        sb.AppendLine($"{car.Brand} {car.Model} - {performancePoints} s.")
            .AppendLine($"{participantEarnedTime} Time, ${prize}.");
    }

    public void Park(int id)
    {
        if (Cars.ContainsKey(id) && OpenedRaces.Values.All(c => c.All(carId => carId != id)))
        {
            Garage.ParkedCars.Add(Cars[id]);
            CarsInGarage.Add(id);
        }
    }

    public void Unpark(int id)
    {
        if (Cars.ContainsKey(id) && CarsInGarage.Contains(id))
        {
            Garage.ParkedCars.Remove(Cars[id]);
            CarsInGarage.Remove(id);
        }
    }

    public void Tune(int tuneIndex, string addOn)
    {
        Garage.TuneParkedCars(tuneIndex, addOn);
    }

    private double Prize(int prizePool, Enum enumType)
    {
        switch (enumType)
        {
            case TimeLimitPerformance.Gold:
                return prizePool;
            case RacePerformance.First:
            case TimeLimitPerformance.Silver:
                return prizePool * 0.5;
            case CircuitRacePerformance.First:
                return prizePool * 0.4;
            case RacePerformance.Second:
            case TimeLimitPerformance.Bronze:
            case CircuitRacePerformance.Second:
                return prizePool * 0.3;
            case RacePerformance.Third:
            case CircuitRacePerformance.Third:
                return prizePool * 0.2;
            case CircuitRacePerformance.Forth:
                return prizePool * 0.1;
        }

        throw new NotImplementedException();
    }

    private string PrizeTimeLimitRace(int timePerformance, TimeLimitRace timeLimitRace)
    {
        if (timePerformance <= timeLimitRace.GoldTime)
        {
            return "Gold";
        }
        if (timePerformance <= timeLimitRace.GoldTime + 15)
        {
            return "Silver";
        }

        return "Bronze";
    }

    private enum RacePerformance
    {
        First = 1,
        Second = 2,
        Third = 3
    }
    private enum TimeLimitPerformance
    {
        Gold,
        Silver,
        Bronze
    }
    private enum CircuitRacePerformance
    {
        First = 1,
        Second = 2,
        Third = 3,
        Forth = 4
    }
}