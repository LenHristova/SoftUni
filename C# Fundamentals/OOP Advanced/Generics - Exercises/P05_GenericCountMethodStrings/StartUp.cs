using System;

public class StartUp
{
    static void Main()
    {
        var boxes = new CustomList<GenericBox<string>>();

        int boxesCount = int.Parse(Console.ReadLine());
        for (int i = 0; i < boxesCount; i++)
        {
            var genericBox = new GenericBox<string>(Console.ReadLine());
            boxes.Add(genericBox);
        }

        var boxToCompare = new GenericBox<string>(Console.ReadLine());
        var bigerElementsCount = boxes.CountGreaterThan(boxToCompare);
        Console.WriteLine(bigerElementsCount);
    }
}