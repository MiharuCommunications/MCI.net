using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Miharu.Async;
using Xunit;

namespace Miharu.Core.Tests.Async.AsyncLockerTests
{
    public class AsyncLockerOrderTests
    {
        // [Fact]
        public async Task OrderTest()
        {
            var locker = new AsyncLocker();
            var list = new List<int>();

            for (var i = 0; i < 1000; i++)
            {
                var j = i;
                await locker.WithLock(async () =>
                {
                    list.Add(j);

                    return Try.Success();
                });
            }

            for (var i = 0; i < 999; i++)
            {
                Assert.True(list[i] < list[i + 1]);
            }
        }
    }
}
