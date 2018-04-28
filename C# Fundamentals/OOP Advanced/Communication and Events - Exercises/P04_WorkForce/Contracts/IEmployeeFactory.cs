namespace P04_WorkForce.Contracts
{
    public interface IEmployeeFactory
    {
        IEmployee CreateEmployee(string employeeType, string name);
    }
}