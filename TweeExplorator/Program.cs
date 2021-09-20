using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TweeExplorator.Classes;
using TweeExplorator.Interfaces;
using TweeExplorator.Models;
using ErrorEventArgs = TweeExplorator.Classes.ErrorEventArgs;

namespace TweeExplorator
{
    class Program
    {        
        static void Main(string[] args)
        {
            IApiConfig apiConfig = Startup.ConsoleConfigStartup(args);
            Console.OutputEncoding = Encoding.UTF8;  // Assumes a font used that handles at least some UTF characters

            WriteInstructions();

            // Setup query
            List<string> twitterApiFields = new List<string>()
            {
                Constants.CreatedAtTwitterField,
                Constants.ExpandUserIdTwitterField,
                Constants.UserFieldCreatedAtTwitterField,              
            };

            string streamApiEndpoint = apiConfig.GetApiEndpoint(Constants.TwitterStream);
            var twitterQuery = new TwitterQueryBuilder(streamApiEndpoint + Constants.TweetFieldsQueryString, twitterApiFields).GetQuery();

            try
            {
                var twitterProcessor = new ApiStreamProcessor();
                twitterProcessor.SetUpdateIntervalSeconds(10);
                twitterProcessor.HttpClient = new HttpClientSetup().GetHttpClient(apiConfig.Token);
                twitterProcessor.HttpRequestMessage = new HttpClientSetup().GetHttpGetRequest(twitterQuery);

                IProcessorOutput<TweetStatisticsEventArgs, ErrorEventArgs> processHandler = new ConsoleLogging<TweetStatisticsEventArgs, ErrorEventArgs>();
                twitterProcessor.StreamEmitter += processHandler.LogProgress;
                twitterProcessor.ErrorEmitter += processHandler.LogError;

                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                new Thread(() =>
                {
                    twitterProcessor.ProcesssApiStreamAsync(cancellationTokenSource.Token);


                }).Start();

                while (!cancellationTokenSource.IsCancellationRequested)
                {
                    var readKey = Console.ReadKey(true).KeyChar;
                    if (readKey == 'e')
                    {

                        twitterProcessor.Disconnect();
                        cancellationTokenSource.Cancel();

                    }

                    if (readKey == 't')
                    {
                        Console.WriteLine($"Last Tweet: {twitterProcessor.ReadRandom().Text}");
                    }

                    if (readKey == 'p')
                    {
                        twitterProcessor.PurgeExistingTweets();
                    }
                    if (readKey == 'l')
                    {
                        GetLanguageInfo(twitterProcessor);
                    }
                    if (readKey == 'c')
                    {
                        Console.Clear();
                        WriteInstructions();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }


        }

        private static void WriteInstructions()
        {
            Console.WriteLine("Press e at any time to exit; Press 't' for reading random tweet; Press 'p' to purge existing records");
            Console.WriteLine("Press 'c' to clear console; 'l' for language stats");
        }

        private static void GetLanguageInfo(ApiStreamProcessor twitterProcessor)
        {
            var stats = twitterProcessor.GetRawStats();
            var languages = stats.GroupBy(g => g.Data.LanguageCode, (K, V) => new
            {
                Usages = V.Count(),
                Language = K,
                Percentage = (int)((decimal)V.Count() / stats.Count() * 100)
            });

            foreach (var language in languages)
            {

                Console.WriteLine($"'{language.Language}' was used {language.Usages} times, or {language.Percentage}% of the time");
            }
        }




    }
}
