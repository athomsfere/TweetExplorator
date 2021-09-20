using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweeExplorator.Interfaces;

namespace TweeExplorator.Classes
{
    class ApiConfiguration : IApiConfig
    {
        public string AppId { get; set; }
        public string AppName { get; set; }
        public string Key { get; set; }
        public string Secret { get; set; }
        public string Token { get; set; }
        public List<IApiEndpoint> Endpoints { get; set; }
    }
}
