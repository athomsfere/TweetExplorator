using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweeExplorator.Interfaces
{
    public interface IStreamResponse<T>
    {
        T Data { get; set; }
    }
}
