namespace Eventures.Data.Models
{
    using System;

    public class Log : BaseEntity<int>
    {
        public string Message { get; set; }

        public int EventId { get; set; }

        public string LogLevel { get; set; }

        public DateTime CreatedTime { get; set; }
    }
}
