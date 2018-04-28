using System;
using System.Text;

public class Engine
{
    private IWriter writer;
    private IReader reader;
    private GameController gameController;

    public Engine()
    {
        writer = new ConsoleWriter();
        reader = new ConsoleReader();
        gameController = new GameController();
    }

    public void Run()
    {
        var input = reader.ReadLine();
        var result = new StringBuilder();

        while (!input.Equals("Enough! Pull back!"))
        {
            try
            {
                gameController.GiveInputToGameController(input);
            }
            catch (ArgumentException arg)
            {
                result.AppendLine(arg.Message);
            }
            input = reader.ReadLine();
        }

        result.AppendLine(gameController.RequestResult());
        writer.WriteLine(result.ToString().Trim());
    }
}