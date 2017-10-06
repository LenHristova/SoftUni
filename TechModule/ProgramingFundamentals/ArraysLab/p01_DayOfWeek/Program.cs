using System;

class Program
{
    static void Main(string[] args)
    {
        string[] dayOfWeek =
            {
                "Monday", "Tuesday", "Wednesday", "Thursday",
                "Friday", "Saturday", "Sunday"
            };
        int day = int.Parse(Console.ReadLine());
        try
        {
            Console.WriteLine(dayOfWeek[day - 1]);
        }
        catch (Exception)
        {

            Console.WriteLine("Invalid Day!");
        }
    }
}

