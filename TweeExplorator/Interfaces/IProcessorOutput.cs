using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweeExplorator.Interfaces
{
    interface IProcessorOutput<T1, T2> where T1 :EventArgs where T2 : EventArgs
    {
        void LogError(object sender, T2 e);

        void LogProgress(object sender, T1 e);
    }
}
