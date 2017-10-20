using System;
using System.Collections.Generic;
using System.Linq;

class StartUp
{
    static readonly Dictionary<char, int> CardsPower = new Dictionary<char, int>{
        {'2', 2 },
        {'3', 3 },
        {'4', 4 },
        {'5', 5 },
        {'6', 6 },
        {'7', 7 },
        {'8', 8 },
        {'9', 9 },
        {'1', 10 },
        {'J', 11 },
        {'Q', 12 },
        {'K', 13 },
        {'A', 14 },
    };
    static readonly Dictionary<char, int> CardsType = new Dictionary<char, int>
    {
        { 'C', 1 },
        { 'D', 2 },
        { 'H', 3 },
        { 'S', 4 }
    };
    static void Main()
    {
        Dictionary<string, List<string>> playersCards = new Dictionary<string, List<string>>();
        HandOutCards(playersCards);

        PrintPlayersPoints(playersCards);
    }

    static void PrintPlayersPoints(Dictionary<string, List<string>> playersCards)
    {
        foreach (var playerCards in playersCards)
        {
            string player = playerCards.Key;
            int cardsPoints = playerCards.Value.Distinct()
                .Select(CalcPoints)
                .Sum();
            Console.WriteLine($"{player}: {cardsPoints}");
        }
    }

    static void HandOutCards(Dictionary<string, List<string>> playersCards)
    {
        string input = Console.ReadLine();

        while (input != "JOKER")
        {
            string[] tokens = input
                .Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            string name = tokens[0];
            List<string> cards = string.Join("", tokens.Skip(1))
                .Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            if (!playersCards.ContainsKey(name))
            {
                playersCards.Add(name, new List<string>());
            }
            playersCards[name].AddRange(cards);

            input = Console.ReadLine();
        }
    }

    static int CalcPoints(string argValue)
    {
        char power = argValue.First();
        char type = argValue.Last();

        return CardsPower[power] * CardsType[type];
    }
}