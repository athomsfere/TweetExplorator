using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweeExplorator.Interfaces;

namespace TweeExplorator.Classes
{
    /// <summary>
    /// Passing in endpoint, and arguments returns a valid URI for Http calls.
    /// </summary>
    public class TwitterQueryBuilder : IQueryStringBuilder<List<string>>
    {
        public string EndPoint { get; set; }
        public List<string> QueryArguments { get; set; }


        public TwitterQueryBuilder(string endPoint, List<string> queryArguments)
        {
            this.EndPoint = endPoint;
            this.QueryArguments = queryArguments;
        }

        public string GetQuery()
        {
            if (EndPoint == null || QueryArguments == null)
            {
                throw new NullReferenceException($"{nameof(TwitterQueryBuilder)} requires an endpoint, and query arguments");
            }

            return EndPoint + string.Join("&", QueryArguments);
        }
    }
}
