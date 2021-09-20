using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweeExplorator.Classes;
using TweeExplorator.Interfaces;

namespace TweeExplorator.Models
{
    public class TweetStatistics
    {
        public List<IStreamResponse<TweetData>> TweetStreamResponses { get; set; }
        public double ErrorCount { get; set; }                
        public TimeSpan TotalConnectionTime { get; set; }

        public TweetStatistics()
        {
            TweetStreamResponses = new List<IStreamResponse<TweetData>>();
        }

        public int GetAverageTweetsPerSecond()
        {
            return TweetStreamResponses.Count / (int)(TotalConnectionTime.TotalSeconds);
        }

    }
}
