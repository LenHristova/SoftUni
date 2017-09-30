using System;

namespace p10_CenturiesToNanoseconds
{
    class CenturiesToNanoseconds
    {
        static void Main(string[] args)
        {
            byte centuries = byte.Parse(Console.ReadLine());
            ushort years = (ushort)(centuries * 100);
            int days = (int)(years * 365.2422);
            int hours = days * 24;
            long minutes = hours * 60;
            long seconds = minutes * 60;
            decimal milliseconds = seconds * 1000;
            decimal microseconds = milliseconds * 1000;
            decimal nanoseconds = microseconds * 1000;

            Console.WriteLine($"{centuries} centuries = {years} years = {days} " +
                $"days = {hours} hours = {minutes} minutes = {seconds} " +
                $"seconds = {milliseconds} milliseconds = {microseconds} " +
                $"microseconds = {nanoseconds} nanoseconds");
        }
    }
}
