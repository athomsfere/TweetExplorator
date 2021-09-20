using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweeExplorator.Interfaces
{
    public interface IApiEndpoint
    {
        string EndpointName { get; set; }
        string Uri { get; set; }
    }
}
