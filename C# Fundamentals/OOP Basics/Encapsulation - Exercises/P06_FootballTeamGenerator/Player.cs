using System.Collections.Generic;
using System.Linq;

namespace P06_FootballTeamGenerator
{
    public class Player
    {
        private string _name;
        private List<int> _stats;

        public string Name
        {
            get => _name;
            private set
            {
                Validations.ValidateName(value);
                _name = value;
            }
        }

        public double SkillLevel => Stats.Average();

        private List<int> Stats
        {
            get => _stats;
            set
            {
                Validations.ValidateStats(value);
                _stats = value;
            }
        }

        public Player(string name, List<int> stats)
        {
            Name = name;
            Stats = stats;
        }
    }
}