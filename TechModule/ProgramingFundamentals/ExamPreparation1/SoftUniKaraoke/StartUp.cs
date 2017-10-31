using System;
using System.Collections.Generic;
using System.Linq;

namespace SoftUniKaraoke
{
    class StartUp
    {
        static void Main()
        {
            string[] listedParticipants = Console.ReadLine()
                .Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

            string[] availableSongs = Console.ReadLine()
                .Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

            Dictionary<string, List<string>> participantsAwards = AddParticipantsAwards(listedParticipants, availableSongs);

            Print(participantsAwards);
        }

        private static void Print(Dictionary<string, List<string>> participantsAwards)
        {
            if (participantsAwards.Count == 0)
            {
                Console.WriteLine("No awards");
                return;
            }
            foreach (var participant in participantsAwards.OrderByDescending(p => p.Value.Count).ThenBy(p => p.Key))
            {
                string participantName = participant.Key;
                List<string> awards = participant.Value
                    .OrderBy(a => a)
                    .ToList();
                double awardsCount = awards.Count;

                Console.WriteLine($"{participantName}: {awardsCount} awards");
                awards.ForEach(a => Console.WriteLine($"--{a}"));
            }
        }

        private static Dictionary<string, List<string>> AddParticipantsAwards(string[] participants, string[] songs)
        {
            Dictionary<string, List<string>> participantsAwards = new Dictionary<string, List<string>>();
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "dawn")
                    break;

                string[] participantInfo = input
                    .Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                string participant = participantInfo[0];
                string song = participantInfo[1];
                string award = participantInfo[2];


                if (!participants.Contains(participant) || !songs.Contains(song))
                {
                    continue;
                }

                if (!participantsAwards.ContainsKey(participant))
                {
                    participantsAwards[participant] = new List<string>();
                }

                if (!participantsAwards[participant].Contains(award))
                {
                    participantsAwards[participant].Add(award);
                }               
            }

            return participantsAwards;
        }
    }
}
