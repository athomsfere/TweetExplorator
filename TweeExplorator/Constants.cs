using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweeExplorator
{
    internal class Constants
    {
        //This is sort of a hot mess for construction query strings. 
        internal const string TwitterStream = "stream";
        internal const string TwitterStreamRules = "rules";

        internal const string MediaTypeApplicationJson = "application/json";
        internal const string CreatedAtTwitterField = "created_at";
        internal const string ExpandUserIdTwitterField = "expansions=author_id,referenced_tweets.id";
        internal const string UserFieldCreatedAtTwitterField = "user.fields=created_at";
        internal const string LanguageCodeTweetField = "tweet.fields=lang";
        internal const string TweetFieldsQueryString = "?tweet.fields=lang,";
    }
}
