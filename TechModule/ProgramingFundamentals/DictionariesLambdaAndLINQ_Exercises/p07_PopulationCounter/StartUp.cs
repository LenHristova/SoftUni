using System;
using System.Collections.Generic;
using System.Linq;

class StartUp
{
    static void Main()
    {
        Dictionary<string, Dictionary<string, long>> cityCountryPopulation = new Dictionary<string, Dictionary<string, long>>();

        string input = Console.ReadLine();

        while (input != "report")
        {
            string[] statisticArgs = input
                .Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

            string city = statisticArgs.First();
            string country = string.Join("", statisticArgs.Skip(1).Take(1));
            long population = long.Parse(statisticArgs.Last());

            if (!cityCountryPopulation.ContainsKey(country))
            {
                cityCountryPopulation[country] = new Dictionary<string, long>();
            }
            cityCountryPopulation[country][city] = population;

            input = Console.ReadLine();
        }

        foreach (var country in cityCountryPopulation.OrderByDescending(d => d.Value.Values.Sum()))
        {
            long countryPopulation = country.Value.Values.Sum();
            Console.WriteLine($"{country.Key} (total population: {countryPopulation})");

            foreach (var city in country.Value.OrderByDescending(d => d.Value))
            {
                Console.WriteLine($"=>{city.Key}: {city.Value}");
            }
        }
    }
}