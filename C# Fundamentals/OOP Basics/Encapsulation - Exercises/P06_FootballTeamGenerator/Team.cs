using System;
using System.Collections.Generic;
using System.Linq;

namespace P06_FootballTeamGenerator
{
    public class Team
    {
        private string _name;
        private readonly Dictionary<string, Player> _players;
        public int Rating => _players.Values.Count == 0
            ? 0 : (int)Math.Round(_players.Values.Select(p => p.SkillLevel).Average());

        public string Name
        {
            get => _name;
            private set
            {
                Validations.ValidateName(value);
                _name = value;
            }
        }

        public Team(string name)
        {
            Name = name;
            _players = new Dictionary<string, Player>();
        }

        public void AddNewPlayer(string playerName, List<int> stats)
        {
            Validations.ValidatePlayerIsNotInTeam(playerName, this.Name, this._players);

            _players[playerName] = new Player(playerName, stats);
        }

        public void TryToRemovePlayer(string playerName)
        {
            Validations.ValidatePlayerIsInTeam(playerName, this.Name, this._players);

            _players.Remove(playerName);
        }
    }
}