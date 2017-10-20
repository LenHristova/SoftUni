using System;

class StartUp
{
    static void Main()
    {
        string[] phrases = new string[]
        {
            "Excellent product.",
            "Such a great product.",
            "I always use that product.",
            "Best product of its category.",
            "Exceptional product.",
            "I can’t live without this product."
        };
        string[] events = new string[]
        {
            "Now I feel good.",
            "I have succeeded with this product.",
            "Makes miracles. I am happy of the results!",
            "I cannot believe but now I feel awesome.",
            "Try it yourself, I am very satisfied.",
            "I feel great!"
        };
        string[] authors = new string[]
            {"Diana", "Petya", "Stella", "Elena", "Katya", "Iva", "Annie", "Eva"};
        string[] cities = new string[] {"Burgas", "Sofia", "Plovdiv", "Varna", "Ruse"};

        Random rnd = new Random();

        int advCount = int.Parse(Console.ReadLine());

        for (int currAdv = 0; currAdv < advCount; currAdv++)
        {
            string currPhrase = phrases[rnd.Next(0, phrases.Length)];
            string currEvent = events[rnd.Next(0, events.Length)];
            string currAuthor = authors[rnd.Next(0, authors.Length)];
            string currCity = cities[rnd.Next(0, cities.Length)];
            Console.WriteLine($"{currPhrase} {currEvent} {currAuthor} - {currCity}");
        }
    }
}
