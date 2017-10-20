using System;
using System.Globalization;

class StartUp
{
    static void Main()
    {
        DateTime startDate = DateTime.ParseExact(Console.ReadLine(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
        DateTime endDate = DateTime.ParseExact(Console.ReadLine(), "dd-MM-yyyy", CultureInfo.InvariantCulture);

        int workDaysCounter = 0;
        for (DateTime currDate = startDate; currDate <= endDate; currDate = currDate.AddDays(1))
        {
            if (!IsHoliday(currDate))
            {
                workDaysCounter++;
            }
        }

        Console.WriteLine(workDaysCounter);
    }

    private static bool IsHoliday(DateTime day)
    {
        if (day.DayOfWeek == DayOfWeek.Saturday ||
            day.DayOfWeek == DayOfWeek.Sunday)
        {
            return true;
        }

        int year = day.Year;
        if (day == new DateTime(year, 01, 01) ||
            day == new DateTime(year, 03, 03) ||
            day == new DateTime(year, 05, 01) ||
            day == new DateTime(year, 05, 06) ||
            day == new DateTime(year, 05, 24) ||
            day == new DateTime(year, 09, 06) ||
            day == new DateTime(year, 09, 22) ||
            day == new DateTime(year, 11, 01) ||
            day == new DateTime(year, 12, 24) ||
            day == new DateTime(year, 12, 25) ||
            day == new DateTime(year, 12, 26))
        {
            return true;
        }

        return false;
    }
}