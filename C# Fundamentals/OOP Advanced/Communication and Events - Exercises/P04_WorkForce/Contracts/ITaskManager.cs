namespace P04_WorkForce.Contracts
{
    public delegate void WeekPassedEventHandler();

    public interface ITaskManager
    {
        event WeekPassedEventHandler WeekPassedEvent;

        void AddNewTask(string name, int requaredHours, IEmployee employee);

        void WeekPass();

        void PrintStatus();
    }
}