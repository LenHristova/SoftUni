namespace P02_KingsGambit.Models
{
    public class RoyalGuard : Subordinate
    {
        private const string REACTION_TO_ATTACK = "defending";

        public RoyalGuard(string name) : base(name, REACTION_TO_ATTACK)
        {
        }

        protected override string GetReaction()
        {
            return $"Royal Guard {Name} is defending!";
        }
    }
}
