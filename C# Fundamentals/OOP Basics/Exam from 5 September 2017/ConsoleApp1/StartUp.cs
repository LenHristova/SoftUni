public class StartUp
{
    static void Main()
    {
        var raceTower = new RaceTower();
        var engine = new Engine(raceTower);
        engine.Start();
    }
}