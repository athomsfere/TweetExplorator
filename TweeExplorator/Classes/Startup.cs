using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweeExplorator.Interfaces;

namespace TweeExplorator.Classes
{
    internal static class Startup
    {
        internal static IApiConfig ConsoleConfigStartup(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddEnvironmentVariables()
                            .AddCommandLine(args)
                            .Build();

            IApiConfig apiConfig = new ApiConfiguration(); // 
            List<APIEndpoint> twitterApiEndpoints = new List<APIEndpoint>();
            configuration.GetSection("twitterApiSettings").Bind(apiConfig);
            configuration.GetSection("twitterEndpoints").Bind(twitterApiEndpoints);

            apiConfig.Endpoints = twitterApiEndpoints.ToList<IApiEndpoint>();
         
            return apiConfig;
        }
    }
}
