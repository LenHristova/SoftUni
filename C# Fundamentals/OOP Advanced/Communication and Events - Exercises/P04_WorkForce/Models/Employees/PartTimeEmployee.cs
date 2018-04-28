namespace P04_WorkForce.Models.Employees
{
    public class PartTimeEmployee : Employee
    {
        public PartTimeEmployee(string name) : base(name)
        {
        }

        public override int WorkHoursPerWeek => 20;
    }
}
