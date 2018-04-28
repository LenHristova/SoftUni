namespace P04_WorkForce.Contracts
{
    public interface IJobFactory
    {
        IJob CreateJob(string name, int requaredHours, IEmployee employee);
    }
}