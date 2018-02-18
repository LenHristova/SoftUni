namespace P05_DateModifier
{
    using System;

    class StartUp
    {
        static void Main()
        {
            var firstDate = Console.ReadLine();
            var secondDate = Console.ReadLine();

            var dateModifier = new DateModifier();
            var daysBetweenDates = dateModifier.CalcDifference(firstDate, secondDate);
            Console.WriteLine(daysBetweenDates);
        }
    }
}