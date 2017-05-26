namespace Miharu.Core.Tests.Async.AsyncLocker2Tests
{
    using System;
    using System.Threading.Tasks;
    using Miharu.Async;
    using Miharu.Errors;
    using Xunit;

    public class AsyncLockerTimeoutTests
    {
        [Fact]
        public void Timeout()
        {
            var timeout = TimeSpan.FromSeconds(5);
            var delay = TimeSpan.FromSeconds(10);

            var locker = new AsyncLocker(timeout, 100);

            Task.WaitAll(
                DelayAsync(locker, timeout),
                ActionAsync(locker));
        }


        private async Task DelayAsync(AsyncLocker locker, TimeSpan delay)
        {
            var result = await locker.LockAsync<int>(async () =>
            {
                await Task.Delay(delay);

                return new Right<Error, int>(0);
            });
        }

        private async Task ActionAsync(AsyncLocker locker)
        {
            var result = await locker.LockAsync<int>(async () =>
            {
                await Task.Delay(0);

                return new Right<Error, int>(0);
            });

            Assert.True(result.IsLeft);
            Assert.IsType<TimeoutError>(result.Left.Get());
        }

    }
}
