using System;
using System.Linq;

public class SequenceOfCommands_broken
{
    private const char ArgumentsDelimiter = ' ';

    public static void Main()
    {
        int sizeOfArray = int.Parse(Console.ReadLine());

        long[] array = Console.ReadLine()
            .Split(ArgumentsDelimiter)
            .Select(long.Parse)
            .ToArray();

        string line = Console.ReadLine();

        while (!line.Equals("stop"))
        {
            string[] stringParams = line.Split(ArgumentsDelimiter);
            string command = stringParams[0];


            if (command.Equals("add") ||
                command.Equals("subtract") ||
                command.Equals("multiply"))
            {
                int[] args = new int[2];
                args[0] = int.Parse(string.Join("", stringParams[1]));
                args[1] = int.Parse(stringParams[2]);

                array = PerformAction(array, command, args);
            }
            else if (command.Equals("lshift"))
            {
                ArrayShiftLeft(array);
            }
            else if (command.Equals("rshift"))
            {
                ArrayShiftRight(array);
            }

            PrintArray(array);
            Console.WriteLine();
            line = Console.ReadLine();
        }
    }

    static long[] PerformAction(long[] arr, string action, int[] args)
    {
        long[] array = arr.Clone() as long[];
        int pos = args[0] - 1;
        int value = args[1];

        switch (action)
        {
            case "multiply":
                array[pos] *= value;
                break;
            case "add":
                array[pos] += value;
                break;
            case "subtract":
                array[pos] -= value;
                break;
        }
        return array;
    }

    private static void ArrayShiftRight(long[] array)
    {
        long oldNum = array[array.Length - 1];
        for (int i = array.Length - 1; i > 0; i--)
        {
            array[i] = array[i - 1];
        }
        array[0] = oldNum;
    }

    private static void ArrayShiftLeft(long[] array)
    {
        long oldNum = array[0];
        for (int i = 0; i < array.Length - 1; i++)
        {
            array[i] = array[i + 1];
        }
        array[array.Length - 1] = oldNum;
    }

    private static void PrintArray(long[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            Console.Write(array[i] + " ");
        }
    }
}
