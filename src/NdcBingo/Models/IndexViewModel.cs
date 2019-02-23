using System;
using System.Collections.Generic;
using System.Linq;
using NdcBingo.Data;

namespace NdcBingo.Models
{
    public class IndexViewModel
    {
        public IndexViewModel(IEnumerable<Talk> talks)
        {
            Talks = talks.Select(TalkViewModel.FromData).ToArray();
        }
        
        public TalkViewModel[] Talks { get; set; }
        public DateTimeOffset NextTalkTime { get; set; }
    }
}