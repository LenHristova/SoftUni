namespace P04_WorkForce.Contracts
{
    public interface IEmployeeManager
    {
        void CreateEmployee(params string[] args);

        IEmployee GetEmployee(string employeeName);
    }
}