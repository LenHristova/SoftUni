using System;

public class StartUp
{
    static void Main()
    {
        int boxesCount = int.Parse(Console.ReadLine());
        for (int i = 0; i < boxesCount; i++)
        {
            var genericBox = new GenericBox<string>(Console.ReadLine());
            Console.WriteLine(genericBox);
        }
    }
}