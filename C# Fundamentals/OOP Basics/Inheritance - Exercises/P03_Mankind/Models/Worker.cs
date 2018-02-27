using System;

namespace P03_Mankind.Models
{
    public class Worker:Human
    {
        private const decimal WEEK_SALARY_MIN_VALUE = 10;
        private const int WORK_HOURS_PER_DAY_MIN_VALUE = 1;
        private const int WORK_HOURS_PER_DAY_MAX_VALUE = 12;
        private const int WORK_DAY_PER_WEEK = 5;

        private decimal weekSalary;
        private double workHoursPerDay;

        private decimal WeekSalary
        {
            get => weekSalary;
            set
            {
                Validator.ValidateSalary(value, WEEK_SALARY_MIN_VALUE, nameof(weekSalary));

                weekSalary = value;
            }
        }

        private double WorkHoursPerDay
        {
            get => workHoursPerDay;
            set
            {
                Validator.ValidateWorkHours(value, WORK_HOURS_PER_DAY_MIN_VALUE, WORK_HOURS_PER_DAY_MAX_VALUE, nameof(workHoursPerDay));
                workHoursPerDay = value;
            }
        }

        public decimal SalaryPerHour => weekSalary / WORK_DAY_PER_WEEK / (decimal)workHoursPerDay;


        public Worker(string firstName, string lastName, decimal weekSalary, double workHoursPerDay) : base(firstName, lastName)
        {
            WeekSalary = weekSalary;
            WorkHoursPerDay = workHoursPerDay;
        }

        public override string ToString()
        {
            return $"{base.ToString()}{Environment.NewLine}" +
                   $"Week Salary: {WeekSalary:F2}{Environment.NewLine}" +
                   $"Hours per day: {WorkHoursPerDay:F2}{Environment.NewLine}" +
                   $"Salary per hour: {SalaryPerHour:F2}";
        }
    }
}