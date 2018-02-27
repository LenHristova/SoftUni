using System;
using System.Globalization;

class StartUp
{
    static void Main()
    {
        var foods = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

        var moodPoints = 0;
        foreach (var food in foods)
        {
            var foodFormated = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(food.ToLower());
            if (Enum.TryParse(typeof(FoodHeppiness), foodFormated, out var foodHeppiness))
            {
                moodPoints += (int)foodHeppiness;
            }
            else
            {
                moodPoints -= 1;
            }
        }

        Console.WriteLine(moodPoints);
        Console.WriteLine(GetMood(moodPoints));
    }

    public static string GetMood(int moodPoints)
    {
        if (moodPoints <= -5)
        {
            return "Angry";
        }

        if (moodPoints <= 0)
        {
            return "Sad";
        }

        if (moodPoints <= 15)
        {
            return "Happy";
        }

        return "JavaScript";
    }

    enum FoodHeppiness
    {
        Cram = 2,
        Lembas = 3,
        Apple = 1,
        Melon = 1,
        Honeycake = 5,
        Mushrooms = -10
    }
}