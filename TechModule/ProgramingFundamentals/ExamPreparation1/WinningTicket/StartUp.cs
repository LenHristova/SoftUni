using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace WinningTicket
{
    class StartUp
    {
        static void Main()
        {
            string[] tickets = Console.ReadLine()
                .Split(new[] { ',', ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var ticket in tickets)
            {
                bool isValidTicket = Regex.IsMatch(ticket, @"^.{20}$");
                if (!isValidTicket)
                {
                    Console.WriteLine("invalid ticket");
                    continue;
                }

                string patern = @"(\@){6,10}|(\#){6,10}|(\$){6,10}|(\^){6,10}|(\.){6,10}";
                string ticketLeftPart = ticket.Substring(0, ticket.Length / 2);
                string ticketRightPart = ticket.Substring(ticket.Length / 2);

                Match leftMatch = Regex.Match(ticketLeftPart, patern);
                Match rightMatch = Regex.Match(ticketRightPart, patern);

                if (!leftMatch.Success || !rightMatch.Success ||
                    leftMatch.Value.First() != rightMatch.Value.First())
                {
                    Console.WriteLine($"ticket \"{ticket}\" - no match");
                    continue;
                }

                int winningSymbolsCount = Math.Min(leftMatch.Length, rightMatch.Length);

                Console.Write($"ticket \"{ticket}\" - {winningSymbolsCount}{leftMatch.Value.First()}");
                if (winningSymbolsCount == 10)
                {
                    Console.WriteLine(" Jackpot!");
                }
                else
                {
                    Console.WriteLine();
                }
            }
        }
    }
}
