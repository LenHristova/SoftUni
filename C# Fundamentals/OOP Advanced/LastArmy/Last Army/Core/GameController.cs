using System.Linq;
using System.Text;

public class GameController
{
    private IArmy army;
    private IWareHouse wareHouse;
    private MissionController missionController;
    private ISoldierFactory soldierFactory;
    private IAmmunitionFactory ammunitionFactory;
    private IMissionFactory missionFactory;
    private StringBuilder result;

    public GameController()
    {
        army = new Army();
        wareHouse = new WareHouse();
        missionController = new MissionController(army, wareHouse);
        soldierFactory = new SoldierFactory();
        ammunitionFactory = new AmmunitionFactory();
        missionFactory = new MissionFactory();
        result = new StringBuilder();
    }

    public void GiveInputToGameController(string input)
    {
        var data = input.Split();

        if (data[0].Equals("Soldier"))
        {
            if (data[1].Equals("Regenerate"))
            {
                var soldierType = data[2];
                army.RegenerateTeam(soldierType);
            }
            else
            {
                var type = data[1];
                var name = data[2];
                var age = int.Parse(data[3]);
                var experience = int.Parse(data[4]);
                var endurance = double.Parse(data[5]);

                var soldier = soldierFactory.CreateSoldier(type, name, age, experience, endurance);

                if (wareHouse.TryEquipSoldier(soldier))
                {
                    army.AddSoldier(soldier);
                }
                else
                {
                    result.AppendLine(string.Format(OutputMessages.NO_WEAPON, type, name));
                }
            }

        }
        else if (data[0].Equals("WareHouse"))
        {
            string name = data[1];
            int number = int.Parse(data[2]);
            for (int i = 0; i < number; i++)
            {
                var ammunition = ammunitionFactory.CreateAmmunition(name);
                wareHouse.AddAmmunition(ammunition);
            }
        }
        else if (data[0].Equals("Mission"))
        {
            var name = data[1];
            var neededPoints = double.Parse(data[2]);
            var mission = missionFactory.CreateMission(name, neededPoints);
            var missinResult = missionController.PerformMission(mission);
            result.AppendLine(missinResult.Trim());
        }
    }

    public string RequestResult()
    {
        missionController.FailMissionsOnHold();

        result.AppendLine(string.Format(
            OutputMessages.END_RESULT,
            missionController.SuccessMissionCounter,
            missionController.FailedMissionCounter));

        foreach (var soldier in army.Soldiers.OrderByDescending(s => s.OverallSkill))
        {
            result.AppendLine(soldier.ToString());
        }

        return result.ToString().Trim();
    }  
}