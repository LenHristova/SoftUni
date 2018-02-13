using System;
using System.Collections.Generic;
using System.Linq;

namespace NumberWars
{
    class StartUp
    {
        private static Queue<Card> firstPlayerCards;
        private static Queue<Card> secondPlayerCards;

        static void Main(string[] args)
        {
            firstPlayerCards = new Queue<Card>();
            GetCards(firstPlayerCards);
            secondPlayerCards = new Queue<Card>();
            GetCards(secondPlayerCards);

            var turns = PlayGame();

            PrintResult(turns);
        }

        private static void GetCards(Queue<Card> player)
        {
            var alphabet = " abcdefghijklmnopqrstuvwxyz";
            var cards = Console.ReadLine()
                .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var card in cards)
            {
                var letter = card.ToLower().Last();
                var numberPower = int.Parse(card.Substring(0, card.IndexOf(letter)));
                var letterPower = alphabet.IndexOf(letter);

                player.Enqueue(new Card(numberPower, letterPower));
            }
        }

        private static int PlayGame()
        {
            int turns;
            for (turns = 0; turns < 1000000; turns++)
            {
                if (SomeOfThePlayersHaveNoCards())
                {
                    break;
                }

                var cardsOnTable = new List<Card>();

                var firstPlayerCard = firstPlayerCards.Dequeue();
                cardsOnTable.Add(firstPlayerCard);
                var secondPlayerCard = secondPlayerCards.Dequeue();
                cardsOnTable.Add(secondPlayerCard);

                if (firstPlayerCard.NumberScore > secondPlayerCard.NumberScore)
                {
                    TakeCards(firstPlayerCards, cardsOnTable);
                }
                else if (firstPlayerCard.NumberScore < secondPlayerCard.NumberScore)
                {
                    TakeCards(secondPlayerCards, cardsOnTable);
                }
                else
                {
                    PlayDraw(turns, cardsOnTable);
                }
            }

            return turns;
        }

        private static void PlayDraw(int turns, List<Card> cardsOnTable)
        {
            while (firstPlayerCards.Count >= 3 && secondPlayerCards.Count >= 3)
            {
                var firstPlayerScore = 0;
                var secondPlayerScore = 0;
                for (int i = 0; i < 3; i++)
                {
                    var firstPlayerCard = firstPlayerCards.Dequeue();
                    var secondPlayerCard = secondPlayerCards.Dequeue();

                    firstPlayerScore += firstPlayerCard.LetterScore;
                    secondPlayerScore += secondPlayerCard.LetterScore;

                    cardsOnTable.Add(firstPlayerCard);
                    cardsOnTable.Add(secondPlayerCard);
                }

                if (firstPlayerScore > secondPlayerScore)
                {
                    TakeCards(firstPlayerCards, cardsOnTable);
                    return;
                }
                if (firstPlayerScore < secondPlayerScore)
                {
                    TakeCards(secondPlayerCards, cardsOnTable);
                    return;
                }
            }

            if (firstPlayerCards.Count > secondPlayerCards.Count)
            {
                secondPlayerCards.Clear();
            }

            if (firstPlayerCards.Count < secondPlayerCards.Count)
            {
                firstPlayerCards.Clear();
            }

            PrintResult(turns + 1);
        }

        private static void TakeCards(Queue<Card> playerCards, List<Card> cardsOnTable)
        {
            foreach (var card in cardsOnTable
                .OrderByDescending(c => c.NumberScore)
                .ThenByDescending(c => c.LetterScore))
            {
                playerCards.Enqueue(card);
            }
        }

        private static bool SomeOfThePlayersHaveNoCards()
        {
            return firstPlayerCards.Count <= 0 || secondPlayerCards.Count <= 0;
        }

        private static void PrintResult(int turns)
        {
            string result;
            if (firstPlayerCards.Count > secondPlayerCards.Count)
            {
                result = "First player wins";
            }
            else if (firstPlayerCards.Count < secondPlayerCards.Count)
            {
                result = "Second player wins";
            }
            else
            {
                result = "Draw";
            }

            Console.WriteLine($"{result} after {turns} turns");
            Environment.Exit(0);
        }
    }
}
