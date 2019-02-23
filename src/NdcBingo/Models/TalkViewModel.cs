using System;
using NdcBingo.Data;

namespace NdcBingo.Models
{
    public class TalkViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }

        public static TalkViewModel FromData(Talk talk)
        {
            return new TalkViewModel
            {
                Id = talk.Id,
                Name = talk.Name,
                StartTime = talk.StartTime,
                EndTime = talk.EndTime
            };
        }
    }
}