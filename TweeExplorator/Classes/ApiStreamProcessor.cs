using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TweeExplorator.Interfaces;
using TweeExplorator.Models;

namespace TweeExplorator.Classes
{
    public class ApiStreamProcessor : IProcessor
    {      
        public ApiStreamProcessor()
        {
            this.SetUpdateIntervalSeconds(6);
        }
        private TimeSpan? UpdateInterval { get; set; }
        public HttpClient HttpClient { get; set; }
        public HttpRequestMessage HttpRequestMessage { get; set; }
        private TweetStatistics TweetStatistics { get; set; }
        private Stopwatch Stopwatch { get; set; }

        public event EventHandler<TweetStatisticsEventArgs> StreamEmitter;
        public event EventHandler<ErrorEventArgs> ErrorEmitter;
        protected virtual void OnTweetEmit(TweetStatisticsEventArgs e)
        {
            EventHandler<TweetStatisticsEventArgs> eventHandler = StreamEmitter;
            if (eventHandler != null)
            {
                eventHandler(this, e);
            }
        }

        protected virtual void OnError(ErrorEventArgs e)
        {
            EventHandler<ErrorEventArgs> eventHandler = ErrorEmitter;
            if (eventHandler != null)
            {
                eventHandler(this, e);
            }
        }

        public void Disconnect()
        {
            HttpClient.Dispose();
            EmitCurrentStatistics(Stopwatch.Elapsed);
        }        

        public List<IStreamResponse<TweetData>> GetRawStats()
        {
            return TweetStatistics.TweetStreamResponses;
        }        

        public void SetUpdateIntervalSeconds(int seconds)
        {
            UpdateInterval = TimeSpan.FromSeconds(seconds);
        }           

        public void PurgeExistingTweets()
        {
            var currentStats = TweetStatistics;

            TweetStatistics = new TweetStatistics();

            EmitCurrentStatistics(Stopwatch.Elapsed, currentStats);
            Stopwatch.Restart();
        }
        
        public TweetData ReadRandom()
        {
            Random random = new Random();
            int randomTweetIndex = random.Next(0, TweetStatistics.TweetStreamResponses.Count);
            return (TweetData)TweetStatistics.TweetStreamResponses[randomTweetIndex].Data;
        }        

        public void ProcesssApiStreamAsync(CancellationToken cancellationToken)
        {
            var result = HttpClient.Send(HttpRequestMessage, HttpCompletionOption.ResponseHeadersRead);

            if (!result.IsSuccessStatusCode)
            {
                throw new Exception($"Processing API could not be completed, {result.StatusCode}, {result.Content}"); // Custom Exception here would probably be better
            }

            var streamResult = result.Content.ReadAsStreamAsync();

            Stopwatch = new Stopwatch();
            Stopwatch.Start();
            TweetStatistics = new TweetStatistics();
            using (var stream = new StreamReader(streamResult.Result))
            {
                while (stream.ReadLine() != null && !cancellationToken.IsCancellationRequested)
                {
                    var line = stream.ReadLine();

                    if (!String.IsNullOrEmpty(line))
                    {
                        TweetStreamResponse tweet = JsonConvert.DeserializeObject<TweetStreamResponse>(line);
                        TweetStatistics.TweetStreamResponses.Add(tweet);
                    }
                    else
                    {
                        TweetStatistics.ErrorCount++;
                        var eventInfo = new ErrorEventArgs();
                        eventInfo.Message = "Tweet was Empty";
                        OnError(eventInfo);
                    }

                    if(Stopwatch.Elapsed.TotalSeconds >= (UpdateInterval?.TotalSeconds))
                    {
                        Stopwatch.Stop();

                        EmitCurrentStatistics(Stopwatch.Elapsed);
                        Stopwatch.Reset();
                        Stopwatch.Start();
                    }

                }
            }            
        }

        private void EmitCurrentStatistics(TimeSpan Time, TweetStatistics tweetStatistics = null)
        {
            TweetStatisticsEventArgs eventArgs = new TweetStatisticsEventArgs();
            eventArgs.TweetStatistics = tweetStatistics ?? TweetStatistics;
            eventArgs.TweetStatistics.TotalConnectionTime += Time;
            OnTweetEmit(eventArgs);
        }

        
    }
}
