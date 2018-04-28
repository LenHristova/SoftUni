using NUnit.Framework;

public class MissionControllerTests
{
    [Test]
    public void MissionControllerDisplaysUnsuccessMessage()
    {
        var army = new Army();
        var wareHouse = new WareHouse();
        var missionController = new MissionController(army, wareHouse);

        var mission = new Easy(1);

        for (int i = 0; i < 3; i++)
        {
            missionController.PerformMission(mission);
        }

        var output = missionController.PerformMission(mission);

        Assert.IsTrue(output.StartsWith("Mission declined - Suppression of civil rebellion"));
    }

    [Test]
    public void PerformMissionFailedMissionCounterWorkCorrectly()
    {
        var army = new Army();
        var wareHouse = new WareHouse();
        var missionController = new MissionController(army, wareHouse);

        var mission = new Easy(1);

        for (int i = 0; i < 4; i++)
        {
            missionController.PerformMission(mission);
        }

        Assert.That(missionController.FailedMissionCounter, Is.EqualTo(1));
    }

    [Test]
    public void FailMissionWorkCorrectly()
    {
        var army = new Army();
        var wareHouse = new WareHouse();
        var missionController = new MissionController(army, wareHouse);

        var mission = new Easy(1);

        for (int i = 0; i < 3; i++)
        {
            missionController.PerformMission(mission);
        }

        missionController.FailMissionsOnHold();
        Assert.That(missionController.Missions.Count, Is.EqualTo(0));
        Assert.That(missionController.FailedMissionCounter, Is.EqualTo(3));
    }

    [Test]
    public void MissionControllerDisplaysSuccessMessage()
    {
        var army = new Army();
        var wareHouse = new WareHouse();
        var missionController = new MissionController(army, wareHouse);

        var mission = new Easy(0);
        string result = missionController.PerformMission(mission);


        Assert.IsTrue(result.StartsWith("Mission completed - Suppression of civil rebellion"));
    }
}