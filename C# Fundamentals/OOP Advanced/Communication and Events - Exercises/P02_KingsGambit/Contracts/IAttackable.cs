namespace P02_KingsGambit.Contracts
{
    public delegate void BeAttackedEventHandler();

    public interface IAttackable
    {
        event BeAttackedEventHandler GetAttackedEvent;

        void BeAttacked();
    }
}
