using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweeExplorator.Classes;

namespace TweeExplorator.Interfaces
{
    interface IProcessor
    {
        List<IStreamResponse<TweetData>> GetRawStats();

        void Disconnect();

        TweetData ReadRandom();
    }
}
