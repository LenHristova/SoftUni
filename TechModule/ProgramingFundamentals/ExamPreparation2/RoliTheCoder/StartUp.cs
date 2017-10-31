using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RoliTheCoder
{
    class StartUp
    {
        static void Main()
        {
            Dictionary<string, Event> events = new Dictionary<string, Event>();

            while (true)
            {
                string input = Console.ReadLine();
                if (input == "Time for Code")
                    break;

                if (!Regex.IsMatch(input, @".+\#.+(\@{1}[A-Za-z0-9-']+)*"))
                {
                    continue;
                }

                AddEventInfo(events, input);
            }

            events = UpdateParticipants(events);

            Print(events);
        }

        private static void Print(Dictionary<string, Event> events)
        {
            foreach (var @event in events
                .OrderByDescending(e => e.Value.Participants.Count)
                .ThenBy(e => e.Value.Name))
            {
                string eventName = @event.Value.Name.Remove(0, 1);
                Console.WriteLine($"{ eventName} - {@event.Value.Participants.Count}");
                @event.Value.Participants
                    .OrderBy(p => p)
                    .ToList()
                    .ForEach(Console.WriteLine);
            }
        }

        private static Dictionary<string, Event> UpdateParticipants(Dictionary<string, Event> events)
        {
            events = events
                 .Select(a => new KeyValuePair<string, Event>(a.Key,
                     new Event
                     {
                         Name = a.Value.Name,
                         Id = a.Value.Id,
                         Participants = a.Value.Participants.Distinct().ToList()
                     }))
                 .ToDictionary(d => d.Key, d => d.Value);
            return events;
        }

        private static void AddEventInfo(Dictionary<string, Event> events, string input)
        {
            string[] eventInfo = input
                .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            string id = eventInfo[0];
            string name = eventInfo[1];
            List<string> partisipants = eventInfo
                .Skip(2)
                .ToList();

            if (!events.ContainsKey(id))
            {
                events[id] = new Event
                {
                    Id = id,
                    Name = name,
                    Participants = partisipants
                };
            }
            else
            {
                if (events[id].Name == name)
                {
                    events[id].Participants.AddRange(partisipants);
                }
            }
        }
    }
}