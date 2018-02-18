using System;

class StartUp
{
    static void Main()
    {
        var drawingTool = new DrawingTool();

        var figureType = Console.ReadLine();
        switch (figureType?.ToLower())
        {
            case "square":
                var side = int.Parse(Console.ReadLine());
                drawingTool.Draw(side, side);
                break;
            case "rectangle":
                var sideA = int.Parse(Console.ReadLine());
                var sideB = int.Parse(Console.ReadLine());
                drawingTool.Draw(sideA, sideB);
                break;
        }
    }
}