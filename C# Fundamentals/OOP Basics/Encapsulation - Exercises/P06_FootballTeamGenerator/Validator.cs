using System;
using System.Collections.Generic;

namespace P06_FootballTeamGenerator
{
    public static class Validator
    {
        private const string EmptyName = "A name should not be empty.";
        private const string InvalidStatsRange = "{0} should be between 0 and 100.";
        private const string TeamNotExist = "Team {0} does not exist.";
        private const string TeamExist = "Team {0} already exist.";
        private const string PlayerNotExist = "Player {0} is not in {1} team.";
        private const string PlayerExist = "Player {0} already is in {1} team.";


        public static void ValidateName(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(EmptyName);
            }
        }

        public static void ValidateStats(List<int> stats)
        {
            for (int pos = 0; pos < stats.Count; pos++)
            {
                if (stats[pos] < 0 || stats[pos] > 100)
                {
                    throw new ArgumentException(string.Format(InvalidStatsRange, Enum.ToObject(typeof(Stats), pos)));
                }
            }
        }

        public static void ValidateCommandLength(int commandArgsCount, int neededLength)
        {
            if (commandArgsCount < neededLength)
                throw new ArgumentException(EmptyName);
        }

        public static void ValidateTeamExistInCollection(Dictionary<string, Team> teams, string teamName)
        {
            if (!teams.ContainsKey(teamName))
                throw new KeyNotFoundException(string.Format(TeamNotExist, teamName));
        }

        public static void ValidateTeamDoesNotExistInCollection(IReadOnlyDictionary<string, Team> teams, string teamName)
        {
            if (teams.ContainsKey(teamName))
                throw new ArgumentException (string.Format(TeamExist, teamName));
        }

        public static void ValidatePlayerIsInTeam(string playerName, string teamName, Dictionary<string, Player> playersInTeam)
        {
            if (!playersInTeam.ContainsKey(playerName))
                throw new KeyNotFoundException(string.Format(PlayerNotExist, playerName, teamName));
        }

        public static void ValidatePlayerIsNotInTeam(string playerName, string teamName, Dictionary<string, Player> playersInTeam)
        {
            if (playersInTeam.ContainsKey(playerName))
                throw new ArgumentException (string.Format(PlayerExist, playerName, teamName));
        }

        private enum Stats
        {
            Endurance,
            Sprint,
            Dribble,
            Passing,
            Shooting
        }
    }
}