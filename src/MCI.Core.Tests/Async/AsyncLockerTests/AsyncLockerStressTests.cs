namespace Miharu.Async.AsyncLocker2Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Miharu.Async;
    using Miharu.Errors;
    using Xunit;

    public class AsyncLockerStressTests
    {
        [Theory,
        InlineData(5, 100),
        InlineData(5, 1000),
        InlineData(5, 10000),
        InlineData(10, 100),
        InlineData(100, 100),
        InlineData(1000, 100)]
        public void ParallelTest(int parallel, int times)
        {
            var locker = new AsyncLocker(TimeSpan.FromMinutes(10.0), parallel * times);
            var results = new List<int>(parallel * times);

            var i = -1;

            Func<Task> action = async () =>
            {
                for (var k = 0; k < times; k++)
                {
                    var result = await locker.LockAsync<int>(async () =>
                    {
                        await Task.Delay(0);

                        i++;

                        results.Add(i);

                        return new Right<Error, int>(i);
                    });

                    Assert.True(result.IsRight);
                }
            };


            Parallel.For(0, parallel, id =>
            {
                action.Invoke().Wait();
            });

            Assert.Equal(parallel * times, i + 1);

            var array = results.ToArray();

            for (var k = 0; k < array.Length; k++)
            {
                Assert.Equal(k, array[k]);
            }

            Assert.Equal(parallel * times, array.Length);
        }
    }
}
