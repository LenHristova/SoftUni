namespace P02_KingsGambit.Models
{
    public class Footman : Subordinate
    {
        private const string REACTION_TO_ATTACK = "panicking";

        public Footman(string name) : base(name, REACTION_TO_ATTACK)
        {
        }
    }
}
