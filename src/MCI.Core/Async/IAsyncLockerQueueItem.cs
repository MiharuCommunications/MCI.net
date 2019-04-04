namespace Miharu.Async
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal interface IAsyncLockerQueueItem
    {
        Task ExecuteAsync();

        void Timeout(TimeSpan timeout);
    }
}
