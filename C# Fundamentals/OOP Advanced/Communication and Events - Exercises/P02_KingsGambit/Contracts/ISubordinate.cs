namespace P02_KingsGambit.Contracts
{
    public interface ISubordinate : INameable, IKillable
    {
        void ReactToAttack();
    }
}