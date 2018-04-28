using P04_WorkForce.Contracts;

namespace P04_WorkForce.Models.Jobs
{
    public class Job : IJob
    {
        private int _requaredHours;

        public Job(string name, int requaredHours, IEmployee employee)
        {
            Name = name;
            RequaredHours = requaredHours;
            Employee = employee;
        }

        public string Name { get; }

        public int RequaredHours
        {
            get => _requaredHours;
            private set
            {
                _requaredHours = value;

                if (_requaredHours <= 0)
                {
                    JobDoneEvent?.Invoke(this);
                }
            }
        }

        public IEmployee Employee { get; }


        public event JobDoneEventEventHandler JobDoneEvent;

        public void Update()
        {
            RequaredHours -= Employee.WorkHoursPerWeek;
        }

        public override string ToString()
        {
            return $"Job: {Name} Hours Remaining: {RequaredHours}";
        }
    }
}
