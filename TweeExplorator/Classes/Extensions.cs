using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweeExplorator.Interfaces;

namespace TweeExplorator.Classes
{
    public static class Extensions
    {
        public static string GetApiEndpoint(this IApiConfig apiConfig, string settingKey)
        {
            return apiConfig.Endpoints.Where(w => w.EndpointName == settingKey).Select(s => s.Uri).FirstOrDefault();
        }
    }
}
