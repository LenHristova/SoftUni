namespace P04_WorkForce.Contracts
{
    public delegate void JobDoneEventEventHandler(object sender);

    public interface IJob : INameable
    {
        IEmployee Employee { get; }

        int RequaredHours { get; }

        event JobDoneEventEventHandler JobDoneEvent;

        void Update();
    }
}
