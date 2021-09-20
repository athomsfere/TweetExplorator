using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweeExplorator.Interfaces;

namespace TweeExplorator.Classes
{
    /// <summary>
    /// Console Logs for IProcessorOutput
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public class ConsoleLogging<T1, T2> : BaseProcessorOutput<T1, T2>, IProcessorOutput<T1, T2> where T1 : EventArgs
        where T2 : EventArgs
    {
        public ConsoleLogging() : base()
        {
            
        }
            
        
        private static void logError(object sender, ErrorEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e.Message);

            Console.ResetColor();
        }

        public static void TweetListHandler(object sender, TweetStatisticsEventArgs e)
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            var tweetStats = e.TweetStatistics;
            Console.WriteLine($"Processed {tweetStats.TweetStreamResponses.Count} with {tweetStats.ErrorCount} errors at an average of {tweetStats.GetAverageTweetsPerSecond()} /second");            
        }

        public new void LogError(object sender, T2 e)
        {
            logError(sender, e as ErrorEventArgs);
        }

        public new void LogProgress(object sender, T1 e)
        {
           TweetListHandler(sender, e as TweetStatisticsEventArgs);
        }
    }
}
