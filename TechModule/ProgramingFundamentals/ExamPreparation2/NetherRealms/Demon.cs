using System.Linq;
using System.Text.RegularExpressions;

namespace NetherRealms
{
    class Demon
    {
        public string Name { get; set; }

        public int Health()
        {
            MatchCollection matches = Regex.Matches(Name, @"[^0-9+\-*\/.]");
            return matches
            .Cast<Match>()
            .Select(m => (int)m.Value.First())
            .Sum();
        } 

        public double Damage()
        {
            MatchCollection matchesNumbers = Regex.Matches(Name, @"((\+?)|(-?))\d+\.?\d*");

            double damage = matchesNumbers
                .Cast<Match>()
                .Select(m => double.Parse(m.Value))
                .Sum();

            //search "*", "/"
            MatchCollection matches = Regex.Matches(Name, @"[\*\/]");

            foreach (Match match in matches)
            {
                damage = match.Value == "*" ? damage * 2 : damage / 2;
            }

            return damage;
        }

        public override string ToString()
        {
            return $"{Name} - {Health()} health, {Damage():F2} damage";
        }
    }
}