namespace P05_KingsGambitExtended.Contracts
{
    public delegate void SubordinateDeathEventHandler(object sender);

    public interface ISubordinate : INameable, IKillable
    {
        event SubordinateDeathEventHandler DeathEvent;

        void ReactToAttack();
    }
}