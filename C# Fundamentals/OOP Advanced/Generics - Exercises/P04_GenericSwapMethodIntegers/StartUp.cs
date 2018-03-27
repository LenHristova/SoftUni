using System;
using System.Linq;

public class StartUp
{
    static void Main()
    {
        var boxes = new CustomList<GenericBox<int>>();

        int boxesCount = int.Parse(Console.ReadLine());
        for (int i = 0; i < boxesCount; i++)
        {
            var number = int.Parse(Console.ReadLine());
            var genericBox = new GenericBox<int>(number);
            boxes.Add(genericBox);
        }

        var indeces = Console.ReadLine()
            .Split()
            .Select(int.Parse)
            .ToArray();

        var index1 = indeces[0];
        var index2 = indeces[1];

        boxes.Swap(index1, index2);

        Console.WriteLine(string.Join(Environment.NewLine, boxes));
    }
}