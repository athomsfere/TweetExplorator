using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweeExplorator.Interfaces;

namespace TweeExplorator.Classes
{
    class APIEndpoint : IApiEndpoint
    {
        public string EndpointName { get ; set; }
        public string Uri { get; set; }
    }
}
