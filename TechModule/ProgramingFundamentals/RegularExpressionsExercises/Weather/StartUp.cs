using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Weather
{
    class StartUp
    {
        static void Main()
        {
            Dictionary<string, WeatherForecast> citiesWeathers = new Dictionary<string, WeatherForecast>();

            string pattern = @"(?<city>[A-Z]{2})(?<avarageTemperature>\d+\.\d+)(?<weather>[A-Za-z]+)\|";
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "end")
                    break;

                bool isValidForecast = Regex.IsMatch(input, pattern);

                if (!isValidForecast)
                {
                    continue;
                }

                Match forecastMath = Regex.Match(input, pattern);
                string city = forecastMath.Groups["city"].Value;
                double avarageTemperature = double.Parse(forecastMath.Groups["avarageTemperature"].Value);
                string weather = forecastMath.Groups["weather"].Value.TrimEnd('|');

                if (!citiesWeathers.ContainsKey(city))
                {
                    citiesWeathers[city] = new WeatherForecast();
                }

                citiesWeathers[city].City = city;
                citiesWeathers[city].AvarageTemperature = avarageTemperature;
                citiesWeathers[city].Weather = weather;
            }

            citiesWeathers.Values
                .OrderBy(c => c.AvarageTemperature)
                .ToList()
                .ForEach(c => Console.WriteLine(c));
        }
    }
}