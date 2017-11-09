using System;
using System.Collections.Generic;
using System.Linq;

namespace HornetComm
{
    class StartUp
    {
        static void Main()
        {
            List<string> privateMessages = new List<string>();
            List<string> broadcasts = new List<string>();

            while (true)
            {
                string input = Console.ReadLine();
                if (input == "Hornet is Green")
                    break;

                string[] queries = input
                    .Split(new[] { " <-> " }, StringSplitOptions.RemoveEmptyEntries);
                if (queries.Length != 2)
                {
                    continue;
                }

                string firstQuery = queries[0];
                string secondQuery = queries[1];

                bool isPrivateMessage = CheckIsPrivateMessage(firstQuery, secondQuery);
                if (isPrivateMessage)
                {
                    AddRecipientAndMessage(privateMessages, firstQuery, secondQuery);
                    continue;
                }

                bool isBroadcast = CheckIsBroadcast(firstQuery, secondQuery);
                if (isBroadcast)
                {
                    AddBroadcast(broadcasts, firstQuery, secondQuery);
                }
            }

            Console.WriteLine("Broadcasts:");
            Console.WriteLine(broadcasts.Count == 0
                ? "None"
                : string.Join(Environment.NewLine, broadcasts));

            Console.WriteLine("Messages:");
            Console.WriteLine(privateMessages.Count == 0
                ? "None"
                : string.Join(Environment.NewLine, privateMessages));

        }

        private static void AddBroadcast(List<string> broadcasts, string firstQuery, string secondQuery)
        {
            string message = firstQuery;
            string frequency = string.Join("", secondQuery
                .Select(ch => char.IsUpper(ch)
                                ? ch.ToString().ToLower()
                                : ch.ToString().ToUpper()));

            broadcasts.Add($"{frequency} -> {message}");
        }

        private static void AddRecipientAndMessage(List<string> privateMessages, string firstQuery, string secondQuery)
        {
            string recipientCode = string.Join("", firstQuery.Reverse());
            string message = secondQuery;
            privateMessages.Add($"{recipientCode} -> {message}");
        }

        private static bool CheckIsBroadcast(string firstQuery, string secondQuery)
        {
            return firstQuery.All(ch => !char.IsDigit(ch))
                   && secondQuery.All(char.IsLetterOrDigit);
        }

        private static bool CheckIsPrivateMessage(string firstQuery, string secondQuery)
        {
            return firstQuery.All(char.IsDigit)
                && secondQuery.All(char.IsLetterOrDigit);
        }
    }
}