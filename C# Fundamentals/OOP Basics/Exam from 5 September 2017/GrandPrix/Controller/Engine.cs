using System;
using System.Linq;

public class Engine
{
    private readonly RaceTower _raceTower;

    public Engine()
    {
        _raceTower = new RaceTower();
    }

    public void Run()
    {
        var numberOfLaps = int.Parse(Console.ReadLine());
        var trackLength = int.Parse(Console.ReadLine());

        _raceTower.SetTrackInfo(numberOfLaps, trackLength);

        while (!_raceTower.HasFinished)
        {
            ParseCommand();
        }

        Console.WriteLine(_raceTower.GetWinner());
    }

    private void ParseCommand()
    {
        var commandArgs = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        var command = commandArgs[0];
        var raceTowerCommandArgs = commandArgs.Skip(1).ToList();
        switch (command)
        {
            case "RegisterDriver":
                _raceTower.RegisterDriver(raceTowerCommandArgs);
                break;

            case "Leaderboard":
                Console.WriteLine(_raceTower.GetLeaderboard());
                break;

            case "CompleteLaps":
                var output = _raceTower.CompleteLaps(raceTowerCommandArgs);
                if (!string.IsNullOrWhiteSpace(output))
                {
                    Console.WriteLine(output);
                }
                break;

            case "Box":
                _raceTower.DriverBoxes(raceTowerCommandArgs);
                break;

            case "ChangeWeather":
                _raceTower.ChangeWeather(raceTowerCommandArgs);
                break;
        }
    }
}