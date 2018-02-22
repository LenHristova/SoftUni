using System;
using System.Collections.Generic;
using System.Linq;

namespace P06_FootballTeamGenerator
{
    class StartUp
    {
        private static Dictionary<string, Team> _teams;

        static void Main()
        {
            _teams = new Dictionary<string, Team>();

            string input;
            while ((input = Console.ReadLine()) != "END")
            {
                try
                {
                    ExecuteCommand(input);
                }
                catch (ArgumentException argEx)
                {
                    Console.WriteLine(argEx.Message);
                }
                catch (KeyNotFoundException knfEx)
                {
                    Console.WriteLine(knfEx.Message);
                }
            }
        }

        private static void ExecuteCommand(string input)
        {
            var commandArgs = input.Trim(';').Split(';', ' ');
            var command = commandArgs[0];
            switch (command.ToLower())
            {
                case "team":
                    TryToAddNewTeam(commandArgs);
                    break;
                case "add":
                    TryToAddNewPlayerToTeam(commandArgs);
                    break;
                case "remove":
                    RemovePlayerFromTeam(commandArgs);
                    break;
                case "rating":
                    PrintTeamReting(commandArgs);
                    break;
                default:
                    throw new ArgumentException("Invalid command.");
            }
        }

        private static void PrintTeamReting(string[] commandArgs)
        {
            Validations.ValidateCommandLength(commandArgs.Length, 2);

            var teamName = commandArgs[1];
            Validations.ValidateName(teamName);

            Validations.ValidateTeamExistInCollection(_teams, teamName);

            var team = _teams[teamName];
            Console.WriteLine($"{team.Name} - {team.Rating}");
        }

        private static void RemovePlayerFromTeam(string[] commandArgs)
        {
            Validations.ValidateCommandLength(commandArgs.Length, 3);

            var teamName = commandArgs[1];
            Validations.ValidateName(teamName);
            var playerName = commandArgs[2];
            Validations.ValidateName(playerName);

            Validations.ValidateTeamExistInCollection(_teams, teamName);

            _teams[teamName].TryToRemovePlayer(playerName);
        }

        private static void TryToAddNewPlayerToTeam(string[] commandArgs)
        {
            Validations.ValidateCommandLength(commandArgs.Length, 8);

            var teamName = commandArgs[1];
            Validations.ValidateName(teamName);
            var playerName = commandArgs[2];
            Validations.ValidateName(playerName);
            var playerStats = commandArgs
                .Skip(3)
                .Select(int.Parse)
                .ToList();

            Validations.ValidateTeamExistInCollection(_teams, teamName);

            _teams[teamName].AddNewPlayer(playerName, playerStats);
        }

        private static void TryToAddNewTeam(string[] commandArgs)
        {
            Validations.ValidateCommandLength(commandArgs.Length, 2);

            var teamName = commandArgs[1];
            Validations.ValidateName(teamName);

            Validations.ValidateTeamDoesNotExistInCollection(_teams, teamName);

            _teams.Add(teamName, new Team(teamName));
        }
    }
}