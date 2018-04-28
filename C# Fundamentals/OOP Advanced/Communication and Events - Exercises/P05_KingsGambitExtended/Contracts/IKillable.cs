namespace P05_KingsGambitExtended.Contracts
{
    public interface IKillable
    {
        bool IsAlive { get; }

        void TakeDamage();
    }
}
