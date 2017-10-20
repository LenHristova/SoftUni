using System;
using System.Collections.Generic;
using System.Linq;

class StartUp
{
    static readonly string[] CardType = { "\0", "C", "D", "H", "S" };
    static readonly string[] CardPower = GetCardPower();

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
                .Split(new[] {' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
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
        string power = string.Join("", argValue.Take(argValue.Length - 1));
        string type = string.Join("", argValue.Last());
        return Array.IndexOf(CardPower, power) * Array.IndexOf(CardType, type);
    }

    static string[] GetCardPower()
    {
        List<string> cardPower = new List<string>();
        for (int i = 0; i <= 10; i++)
        {
            cardPower.Add(i.ToString());
        }
        cardPower.AddRange(new List<string> { "J", "Q", "K", "A" });

        return cardPower.ToArray();
    }
}