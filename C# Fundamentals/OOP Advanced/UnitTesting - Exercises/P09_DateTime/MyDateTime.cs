using System;

namespace P09_DateTime
{
    public class MyDateTimeNow : IDaysAddable
    {
        private DateTime _now = DateTime.Now;

        public DateTime AddDays(double daysToAdd) => _now.AddDays(daysToAdd);
    }
}
