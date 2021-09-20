using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweeExplorator.Interfaces;

namespace TweeExplorator.Classes
{
    public  class BaseProcessorOutput<T1, T2> : IProcessorOutput<T1, T2> where T1 : EventArgs
        where T2 : EventArgs
    {
        public void LogError(object sender, T2 e)
        {
            logError(sender, e);
        }

        static void logError(object sender, T2 e)
        {
            throw new NotImplementedException();
        }

        public void LogProgress(object sender, T1 e)
        {
            logProgress(sender, e);
        }

        static void logProgress(object sender, T1 e)
        {
            throw new NotImplementedException();
        }
    }
}
