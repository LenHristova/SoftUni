using System;
using System.Collections.Generic;
using System.Linq;

class StartUp
{
    static void Main()
    {
        Dictionary<string, Dictionary<string, long>> concerts = new Dictionary<string, Dictionary<string, long>>();

        while (true)
        {
            string input = Console.ReadLine();
            if (input == "End")
            {
                break;
            }
            string singer;
            string venue;
            long concertProfit;
            try
            {
                string[] singerAndConcertInfo = input
                    .Split(new[] { " @" }, StringSplitOptions.RemoveEmptyEntries);

                string[] concertInfo = string.Join("", singerAndConcertInfo.Skip(1).Take(1))
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Reverse()
                    .ToArray();
                int[] ticketInfo = concertInfo
                    .Take(2)
                    .Select(int.Parse)
                    .ToArray();

                singer = singerAndConcertInfo.First();
                venue = string.Join(" ", concertInfo
                   .Skip(2)
                   .Reverse());
                concertProfit = (long)ticketInfo[0] * ticketInfo[1];
            }
            catch (Exception)
            {
                continue;
            }

            if (!concerts.ContainsKey(venue))
            {
                concerts[venue] = new Dictionary<string, long>();
            }
            if (!concerts[venue].ContainsKey(singer))
            {
                concerts[venue][singer] = 0L;
            }
            concerts[venue][singer] += concertProfit;
        }

        foreach (var venue in concerts)
        {
            Console.WriteLine(venue.Key);
            foreach (var singerProfit in venue.Value.OrderByDescending(sp => sp.Value))
            {
                Console.WriteLine($"#  {singerProfit.Key} -> {singerProfit.Value}");
            }
        }
    }
}