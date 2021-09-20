using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweeExplorator.Interfaces
{
    public interface IApiConfig
    {
        string AppId { get; set; }
        string AppName { get; set; }
        string Key { get; set; }
        string Secret { get; set; }
        string Token { get; set; }        
        List<IApiEndpoint> Endpoints { get; set; }
    }
}
