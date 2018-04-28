using P04_WorkForce.Contracts;
using P04_WorkForce.Models.Jobs;

namespace P04_WorkForce.Factories
{
    public class JobFactory : IJobFactory
    {
        public IJob CreateJob(string name, int requaredHours, IEmployee employee)
        {
             return new Job(name, requaredHours, employee);
        }
    }
}
