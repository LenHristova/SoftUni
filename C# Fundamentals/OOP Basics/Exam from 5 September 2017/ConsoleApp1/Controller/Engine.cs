using System;
using System.Linq;

public class Engine
{
    private readonly RaceTower _raceTower;

    public Engine(RaceTower raceTower)
    {
        _raceTower = raceTower;
    }

    public void Start()
    {
        var lapsCount = int.Parse(Console.ReadLine());
        var trackLength = int.Parse(Console.ReadLine());
        _raceTower.SetTrackInfo(lapsCount, trackLength);

        while (!_raceTower.HasFinished)
        {
            try
            {
                ParseCommand();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (_raceTower.HasFinished)
            {
                Console.WriteLine(_raceTower.GetWinner());
            }
        }
    }

    private void ParseCommand()
    {
        var commandArgs = Console.ReadLine().Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

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
                var m = _raceTower.CompleteLaps(raceTowerCommandArgs);
                if (!string.IsNullOrWhiteSpace(m))
                {
                    Console.WriteLine(m);
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