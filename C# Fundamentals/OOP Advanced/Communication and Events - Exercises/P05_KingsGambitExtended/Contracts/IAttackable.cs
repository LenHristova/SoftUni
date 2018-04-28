namespace P05_KingsGambitExtended.Contracts
{
    public delegate void BeAttackedEventHandler();

    public interface IAttackable
    {
        event BeAttackedEventHandler GetAttackedEvent;

        void BeAttacked();
    }
}
