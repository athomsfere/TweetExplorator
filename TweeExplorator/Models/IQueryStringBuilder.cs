using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweeExplorator.Interfaces
{
    interface IQueryStringBuilder<T>
    {
        string EndPoint { get; set; }
        T QueryArguments { get; set; }
        string GetQuery();
    }
}
