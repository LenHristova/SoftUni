using System;
using System.Collections.Generic;

public class StartUp
{
    static void Main()
    {
        var boxes = new List<GenericBox<double>>();

        int boxesCount = int.Parse(Console.ReadLine());
        for (int i = 0; i < boxesCount; i++)
        {
            var number = double.Parse(Console.ReadLine());
            var genericBox = new GenericBox<double>(number);
            boxes.Add(genericBox);
        }
        var numberToCompare = double.Parse(Console.ReadLine());
        var boxToCompare = new GenericBox<double>(numberToCompare);
        var bigerElementsCount = CountOfElementsGreaterThen(boxToCompare, boxes);
        Console.WriteLine(bigerElementsCount);
    }

    static int CountOfElementsGreaterThen<T>(T element, IEnumerable<T> elements)
        where T : IComparable<T>
    {
        var biggerNumbersCount = 0;

        foreach (var el in elements)
        {
            if (element.CompareTo(el) < 0)
            {
                biggerNumbersCount++;
            }
        }

        return biggerNumbersCount;
    }
}