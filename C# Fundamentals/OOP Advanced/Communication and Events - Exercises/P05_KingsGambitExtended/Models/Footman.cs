namespace P05_KingsGambitExtended.Models
{
    public class Footman : Subordinate
    {
        private const string REACTION_TO_ATTACK = "panicking";
        private const int HIT_POINTS = 2;

        public Footman(string name) : 
            base(name, REACTION_TO_ATTACK, HIT_POINTS)
        {
        }
    }
}
