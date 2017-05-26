namespace Miharu.Async
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal interface IAsyncLocker2QueueItem
    {
        Task ExecuteAsync();

        void Timeout(TimeSpan timeout);
    }
}
