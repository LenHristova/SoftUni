using System;
using System.Collections.Generic;

using P04_WorkForce.Contracts;

namespace P04_WorkForce.Core
{
    public class TaskManager : ITaskManager
    {
        private readonly ICollection<IJob> _jobs;
        private readonly IJobFactory _jobFactory;
        private readonly IWriter _writer;

        public TaskManager(ICollection<IJob> jobs, IJobFactory jobFactory, IWriter writer)
        {
            _jobs = jobs;
            _jobFactory = jobFactory;
            _writer = writer;
        }

        public event WeekPassedEventHandler WeekPassedEvent;

        public void AddNewTask(string name, int requaredHours, IEmployee employee)
        {
            var job = _jobFactory.CreateJob(name, requaredHours, employee);
            _jobs.Add(job);
            WeekPassedEvent += job.Update;
            job.JobDoneEvent += OnComplete;
        }

        private void OnComplete(object sender)
        {
            var job = (IJob) sender;
            _writer.WriteLine($"Job {job.Name} done!");
            _jobs.Remove(job);
            WeekPassedEvent -= job.Update;
            job.JobDoneEvent -= OnComplete;
        }

        public void WeekPass()
        {
            WeekPassedEvent?.Invoke();
        }

        public void PrintStatus()
        {
            _writer.WriteLine(string.Join(Environment.NewLine, _jobs));
        }
    }
}
