using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FootballStandings
{
    class StartUp
    {
        class Team
        {
            public int Points { get; set; }
            public int Goals { get; set; }
        }

        static void Main()
        {
            Dictionary<string, Team> teams = new Dictionary<string, Team>();

            string key = GetKey(Console.ReadLine());


            while (true)
            {
                string input = Console.ReadLine();
                if (input == "final")
                    break;

                string[] gameInfo = input
                    .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                string team1 = string.Join("",
                    Regex.Match(gameInfo[0], $@"(?<={key}).*(?={key})")
                    .Value
                    .ToUpper()
                    .Reverse());
                string team2 = string.Join("",
                    Regex.Match(gameInfo[1], $@"(?<={key}).*(?={key})")
                        .Value
                        .ToUpper()
                        .Reverse());

                string result = Regex.Match(input, @"\d+:\d+").Value;

                string[] results = Regex.Split(result, @"\:");

                int team1Goals = int.Parse(results.First());
                int team2Goals = int.Parse(results.Last());

                int team1Points = 1;
                int team2Points = 1;

                if (team1Goals > team2Goals)
                {
                    team1Points = 3;
                    team2Points = 0;
                }
                else if (team1Goals < team2Goals)
                {
                    team1Points = 0;
                    team2Points = 3;
                }

                AddInfoForTheTeam(teams, team1, team1Goals, team1Points);
                AddInfoForTheTeam(teams, team2, team2Goals, team2Points);
            }

            PrintStandingsTable(teams);
            PrintTop3ScoredGoals(teams);
        }

        private static string GetKey(string readLine)
        {
            return string.Join("", readLine
                .Select(ch => ch.ToString())
                .Select(ch => Regex.IsMatch(ch, @"[A-Za-z0-9]") ? ch : $"\\{ch}"));
        }

        private static void PrintTop3ScoredGoals(Dictionary<string, Team> teams)
        {
            Console.WriteLine("Top 3 scored goals:");

            Dictionary<string, int> top3GoalsTeams = teams
                .OrderByDescending(t => t.Value.Goals)
                .ThenBy(t => t.Key)
                .Take(3)
                .ToDictionary(t => t.Key, t => t.Value.Goals);

            foreach (var team in top3GoalsTeams)
            {
                Console.WriteLine($"- {team.Key} -> {team.Value}");
            }
        }

        private static void PrintStandingsTable(Dictionary<string, Team> teams)
        {
            Console.WriteLine("League standings:");
            int number = 1;
            foreach (var team in teams.OrderByDescending(t => t.Value.Points)
                .ThenBy(t => t.Key))
            {
                Console.WriteLine($"{number}. {team.Key} {team.Value.Points}");
                number++;
            }
        }

        private static void AddInfoForTheTeam(Dictionary<string, Team> teams, string team, int goals, int points)
        {
            if (!teams.ContainsKey(team))
            {
                teams[team] = new Team();
            }

            teams[team].Goals += goals;
            teams[team].Points += points;
        }
    }
}
