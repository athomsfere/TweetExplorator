using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweeExplorator.Models;

namespace TweeExplorator.Classes
{
    public class TweetStatisticsEventArgs : EventArgs
    {
        public TweetStatistics TweetStatistics { get; set; }
    }

    public class ErrorEventArgs : EventArgs
    {
        public string Message { get; set; }
    }
}
