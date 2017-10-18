using System;
using System.Globalization;

class StartUp
{
    static void Main()
    {
        DateTime date = DateTime.ParseExact(Console.ReadLine(),
            "d-M-yyyy", CultureInfo.InvariantCulture);
        Console.WriteLine(date.DayOfWeek);
    }
}