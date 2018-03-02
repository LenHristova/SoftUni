using System;
using System.Collections.Generic;
using System.Linq;

class StartUp
{
    private static readonly List<ISoldier> Soldiers = new List<ISoldier>();

    static void Main()
    {
        string input;
        while ((input = Console.ReadLine()) != "End")
        {
            try
            {
                Soldiers.Add(CreateSoldier(input, Soldiers));
            }
            catch (ArgumentException) { continue; }
        }

        Console.WriteLine(string.Join(Environment.NewLine, Soldiers));
    }

    public static ISoldier CreateSoldier(string input, IReadOnlyCollection<ISoldier> soldiers)
    {
        var soldierInfo = input.Split();
        var soldierType = soldierInfo[0];
        var id = soldierInfo[1];
        var firstName = soldierInfo[2];
        var lastName = soldierInfo[3];

        switch (soldierType)
        {
            case nameof(Private):
                return CreatePrivate(soldierInfo, id, firstName, lastName);

            case nameof(LeutenantGeneral):
                return CreateLeutenantGeneral(soldierInfo, id, firstName, lastName);

            case nameof(Engineer):
                return TryToCreateEngineer(soldierInfo, id, firstName, lastName);

            case nameof(Commando):
                return TryToCreateCommando(soldierInfo, id, firstName, lastName);

            case nameof(Spy):
                var codeNumber = int.Parse(soldierInfo[4]);
                return new Spy(id, firstName, lastName, codeNumber);
        }

        throw new ArgumentException("Invalid input!");
    }

    private static ISoldier TryToCreateCommando(string[] soldierInfo, string id, string firstName, string lastName)
    {
        var salary = decimal.Parse(soldierInfo[4]);
        var corps = soldierInfo[5];
        var currentCommando = new Commando(id, firstName, lastName, salary, new Corps(corps));

        var missionsInfo = soldierInfo.Skip(6).ToList();
        for (int i = 0; i < missionsInfo.Count; i += 2)
        {
            var missionCodeName = missionsInfo[i];
            var missionState = missionsInfo[i + 1];
            try
            {
                currentCommando.AddMission(new Mission(missionCodeName, missionState));
            }
            catch (ArgumentException) { }
        }

        return currentCommando;
    }

    private static ISoldier TryToCreateEngineer(string[] soldierInfo, string id, string firstName, string lastName)
    {
        var salary = decimal.Parse(soldierInfo[4]);
        var corps = soldierInfo[5];
        var currentEngineer = new Engineer(id, firstName, lastName, salary, new Corps(corps));

        var repairsInfo = soldierInfo.Skip(6).ToList();
        for (int i = 0; i < repairsInfo.Count; i += 2)
        {
            var repairPart = repairsInfo[i];
            var repairHours = int.Parse(repairsInfo[i + 1]);
            currentEngineer.AddRapair(new Repair(repairPart, repairHours));
        }

        return currentEngineer;
    }

    private static ISoldier CreateLeutenantGeneral(string[] soldierInfo, string id, string firstName, string lastName)
    {
        var salary = decimal.Parse(soldierInfo[4]);
        var currentLeutenantGeneral = new LeutenantGeneral(id, firstName, lastName, salary);
        foreach (var privateId in soldierInfo.Skip(5))
        {
            var soldier = Soldiers.FirstOrDefault(s => s.Id == privateId);

            if (soldier is IPrivate @private)
            {
                currentLeutenantGeneral.AddPrivate(@private);
            }
        }

        return currentLeutenantGeneral;
    }

    private static ISoldier CreatePrivate(string[] soldierInfo, string id, string firstName, string lastName)
    {
        var salary = decimal.Parse(soldierInfo[4]);
        return new Private(id, firstName, lastName, salary);
    }
}