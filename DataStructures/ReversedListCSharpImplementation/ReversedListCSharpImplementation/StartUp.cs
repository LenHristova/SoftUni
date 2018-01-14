using System;

class StartUp
{
    static void Main()
    {
        var reversedList = new ReversedList<int>();

        reversedList.Add(1);
        reversedList.Add(2);
        reversedList.Add(3);

        foreach (var i in reversedList)
        {
            Console.Write(i + " ");
        }

        Console.WriteLine();
        Console.WriteLine("------------------------------");

        reversedList.RemoveAt(0);

        foreach (var i in reversedList)
        {
            Console.Write(i + " ");
        }

        Console.WriteLine();
        Console.WriteLine("------------------------------");

        for (int i = 0; i < 100; i++)
        {
            reversedList.Add(i);
        }

        foreach (var i in reversedList)
        {
            Console.Write(i + " ");
        }

        Console.WriteLine();
        Console.WriteLine("------------------------------");

        for (int i = 0; i < 90; i++)
        {
            reversedList.RemoveAt(0);
        }

        foreach (var i in reversedList)
        {
            Console.Write(i + " ");
        }

        Console.WriteLine();
        Console.WriteLine("------------------------------");

        reversedList[0] = 9;
        Console.WriteLine(reversedList[0]);
        Console.WriteLine("------------------------------");

        reversedList.Add(8);
        reversedList.Add(7);
        reversedList.Add(18);
        Console.WriteLine(string.Join(" ", reversedList));
        Console.WriteLine("------------------------------");

        //reversedList[15] = 0;
    }
}