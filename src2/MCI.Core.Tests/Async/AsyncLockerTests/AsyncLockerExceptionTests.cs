namespace Miharu.Core.Tests.Async.AsyncLockerTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Miharu.Async;
    using Xunit;

    public class AsyncLockerExceptionTests
    {
        [Theory,
        InlineData(1000)]
        public async Task TestExceptionToVoid(int max)
        {
            var locker = new AsyncLocker();


            for (var i = 0; i < max; i++)
            {
                var result = await locker.WithLock(async () =>
                {
                    await Task.Delay(0);

                    throw new Exception();
                });
            }
        }


        [Theory,
        InlineData(1000)]
        public async Task TestExceptionToTyped(int max)
        {
            var locker = new TypedAsyncLocker();


            for (var i = 0; i < max; i++)
            {
                var result = await locker.WithLock<int>(async () =>
                {
                    await Task.Delay(0);

                    throw new Exception();
                });
            }
        }
    }
}
