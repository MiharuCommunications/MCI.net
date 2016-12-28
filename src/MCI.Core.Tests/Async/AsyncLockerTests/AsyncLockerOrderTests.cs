namespace Miharu.Core.Tests.Async.AsyncLockerTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Miharu.Async;
    using Xunit;

    public class AsyncLockerOrderTests
    {
        [Theory,
        InlineData(1000)]
        public async Task TestOrderingToVoid(int max)
        {
            var locker = new AsyncLocker();
            var list = new List<int>(max);

            for (var i = 0; i < max; i++)
            {
                var j = i;
                await locker.WithLock(async () =>
                {
                    await Task.Delay(0);

                    list.Add(j);

                    return Try.Success();
                });
            }

            for (var i = 0; i < max; i++)
            {
                if (i != 0)
                {
                    Assert.True(list[i - 1] < list[i]);
                }
            }
        }

        [Theory,
        InlineData(1000)]
        public async Task TestOrderingToTyped(int max)
        {
            var locker = new TypedAsyncLocker();
            var list = new List<int>(max);

            for (var i = 0; i < max; i++)
            {
                var j = i;
                await locker.WithLock<int>(async () =>
                {
                    await Task.Delay(0);

                    list.Add(j);

                    return Try<int>.Success(j);
                });
            }

            for (var i = 0; i < max; i++)
            {
                if (i != 0)
                {
                    Assert.True(list[i - 1] < list[i]);
                }
            }
        }
    }
}
