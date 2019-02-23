using System;

namespace NdcBingo.Data
{
    public class Talk
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public int TimeZone { get; set; }
    }
}