using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Miharu.Async;
using Xunit;

namespace Miharu.Core.Tests.Async.AsyncLockerTests
{
    public class AsyncLockerExceptionTests
    {
        [Fact]
        public async Task Exception()
        {
            const int N = 100;

            var list = new List<int>(N * 100);
            var locker = new AsyncLocker();


            for (var i = 0; i < N; i++)
            {
                await locker.WithLock(async () =>
                {
                    throw new Exception();
                });
            }

        }
    }
}
