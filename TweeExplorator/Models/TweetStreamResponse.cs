using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweeExplorator.Interfaces;

namespace TweeExplorator.Classes
{    
    public class TweetStreamResponse : IStreamResponse<TweetData>
    {
        [JsonProperty("data")]
        public TweetData Data { get; set; }
    }

    public class TweetData
    {
        [JsonProperty("author_id")]
        public string AuthorId { get; set; }
        [JsonProperty("created_at")]
        public string CreateTime { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("lang")]
        public string LanguageCode { get; set; }

        public static explicit operator TweetData(List<TweetData> v)
        {
            throw new NotImplementedException();
        }
    }
}
