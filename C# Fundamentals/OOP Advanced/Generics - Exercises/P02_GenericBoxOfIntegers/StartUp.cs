using System;

public class StartUp
{
    static void Main()
    {
        int boxesCount = int.Parse(Console.ReadLine());
        for (int i = 0; i < boxesCount; i++)
        {
            var number = int.Parse(Console.ReadLine());
            var genericBox = new GenericBox<int>(number);
            Console.WriteLine(genericBox);
        }
    }
}