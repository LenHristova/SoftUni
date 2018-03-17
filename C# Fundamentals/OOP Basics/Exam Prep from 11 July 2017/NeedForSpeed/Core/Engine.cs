using System;
using System.Text;

public class Engine
{
    private StringBuilder Sb { get; set; }
    private CarManager CarManager { get; set; }

    public Engine()
    {
        Sb = new StringBuilder();
        CarManager = new CarManager();
    }

    public void Run()
    {
        string input;
        while ((input = Console.ReadLine()) != "Cops Are Here")
        {
            ParseCommand(input);
        }

        Console.WriteLine(Sb.ToString().TrimEnd(
            ));
    }

    private void ParseCommand(string input)
    {
        var commandArgs = input
            .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        var command = commandArgs[0];
        switch (command)
        {
            case "register":
                RegisterCar(commandArgs);
                return;
            case "check":
                CheckCar(commandArgs);
                return;
            case "open":
                OpenRace(commandArgs);
                return;
            case "participate":
                AddCarToRace(commandArgs);
                return;
            case "start":
                StartRace(commandArgs);
                return;
            case "park":
                ParkCar(commandArgs);
                return;
            case "unpark":
                UnparkCar(commandArgs);
                return;
            case "tune":
                TuneCars(commandArgs);
                return;
        }

        throw new NotImplementedException();
    }

    private void TuneCars(string[] commandArgs)
    {
        var tuneIndex = int.Parse(commandArgs[1]);
        var addOn = commandArgs[2];
        CarManager.Tune(tuneIndex, addOn);
    }

    private void UnparkCar(string[] commandArgs)
    {
        var carId = int.Parse(commandArgs[1]);
        CarManager.Unpark(carId);
    }

    private void ParkCar(string[] commandArgs)
    {
        var carId = int.Parse(commandArgs[1]);
        CarManager.Park(carId);
    }

    private void StartRace(string[] commandArgs)
    {
        var id = int.Parse(commandArgs[1]);
        TryAppendLine(CarManager.Start(id));
    }

    private void AddCarToRace(string[] commandArgs)
    {
        var carId = int.Parse(commandArgs[1]);
        var raceId = int.Parse(commandArgs[2]);
        CarManager.Participate(carId, raceId);
    }

    private void OpenRace(string[] commandArgs)
    {
        var id = int.Parse(commandArgs[1]);
        var type = commandArgs[2];
        var length = int.Parse(commandArgs[3]);
        var route = commandArgs[4];
        var prizePool = int.Parse(commandArgs[5]);

        if (commandArgs.Length == 6)
        {
            CarManager.Open(id, type, length, route, prizePool);
        }
        else
        {
            var extraParam = int.Parse(commandArgs[6]);
            CarManager.Open(id, type, length, route, prizePool, extraParam);
        }
    }

    private void CheckCar(string[] commandArgs)
    {
        var id = int.Parse(commandArgs[1]);
        TryAppendLine(CarManager.Check(id));
    }

    private void TryAppendLine(string resultFromCommand)
    {
        if (!string.IsNullOrWhiteSpace(resultFromCommand))
        {
            Sb.AppendLine(resultFromCommand);
        }
    }

    private void RegisterCar(string[] commandArgs)
    {
        var id = int.Parse(commandArgs[1]);
        var type = commandArgs[2];
        var brand = commandArgs[3];
        var model = commandArgs[4];
        var yearOfProduction = int.Parse(commandArgs[5]);
        var horsepower = int.Parse(commandArgs[6]);
        var acceleration = int.Parse(commandArgs[7]);
        var suspension = int.Parse(commandArgs[8]);
        var durability = int.Parse(commandArgs[9]);
        CarManager.Register(id, type, brand, model, yearOfProduction, horsepower, acceleration, suspension, durability);
    }
}