using P04_WorkForce.Contracts;

namespace P04_WorkForce.Models.Employees
{
    public abstract class Employee : IEmployee
    {
        protected Employee(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public virtual int WorkHoursPerWeek => 40;
    }
}
