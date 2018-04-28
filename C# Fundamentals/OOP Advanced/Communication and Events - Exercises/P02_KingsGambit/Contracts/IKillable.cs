namespace P02_KingsGambit.Contracts
{
    public interface IKillable
    {
        bool IsAlive { get; }

        void BeKilled();
    }
}
