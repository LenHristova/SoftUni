using System;
using System.Linq;

namespace P05_MordorCruelPlan2
{
    public class StartUp
    {
        static void Main()
        {
            var happinessPoints = Console.ReadLine()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(FoodFactory.ProduceFood)
                .Sum(x => x.HappinessPoints);

            var mood = MoodFactory.CreateMood(happinessPoints);
            Console.WriteLine(mood);
        }
    }
}