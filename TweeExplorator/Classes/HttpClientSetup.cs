using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TweeExplorator.Classes
{
    public class HttpClientSetup
    {
        public HttpClient GetHttpClient(string twitterBearerToken)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", twitterBearerToken);
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(Constants.MediaTypeApplicationJson));

            return httpClient;
        }

        public HttpRequestMessage GetHttpGetRequest(string uri)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);            
            httpRequestMessage.Content = new StringContent("", Encoding.UTF8, Constants.MediaTypeApplicationJson);
            return httpRequestMessage;
        }
    }
}
