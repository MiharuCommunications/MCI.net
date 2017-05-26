namespace Miharu.Core.Tests.Async.AsyncLocker2Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Miharu.Async;
    using Miharu.Errors;
    using Xunit;

    public class AsyncLockerTimeoutTests
    {
        public async Task Delay(AsyncLocker locker, TimeSpan delay)
        {
            var result = await locker.LockAsync<int>(async () =>
            {
                await Task.Delay(delay);

                return new Right<Error, int>(0);
            });
        }

        public async Task Action(AsyncLocker locker)
        {
            var result = await locker.LockAsync<int>(async () =>
            {
                await Task.Delay(0);

                return new Right<Error, int>(0);
            });

            Assert.True(result.IsLeft);
            Assert.IsType<TimeoutError>(result.Left.Get());
        }

        [Fact]
        public void Timeout()
        {
            var timeout = TimeSpan.FromSeconds(5);
            var delay = TimeSpan.FromSeconds(10);

            var locker = new AsyncLocker(timeout, 100);

            Task.WaitAll(
                Delay(locker, timeout),
                Action(locker));
        }
    }
}
