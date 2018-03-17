using System;

public static class OutputWriter
{
    public static void WriteLine(string message)
    {
        Console.WriteLine(message);
    }

    public static void WriteLine()
    {
        Console.WriteLine();
    }

    public static string NewLine => Environment.NewLine;
}