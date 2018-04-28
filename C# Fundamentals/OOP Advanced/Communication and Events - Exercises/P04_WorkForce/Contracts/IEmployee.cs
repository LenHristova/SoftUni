namespace P04_WorkForce.Contracts
{
    public interface IEmployee : INameable
    {
        int WorkHoursPerWeek { get; }
    }
}
