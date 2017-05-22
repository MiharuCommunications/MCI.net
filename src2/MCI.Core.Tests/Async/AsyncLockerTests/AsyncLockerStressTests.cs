namespace Miharu.Core.Tests.Async.AsyncLockerTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Miharu.Async;
    using Xunit;

    public class AsyncLockerStressTests
    {
        [Theory,
        InlineData(5, 50),
        InlineData(10, 100),
        InlineData(100, 10)]
        public void AddStressToVoidLocker(int parallel, int times)
        {
            var locker = new AsyncLocker();
            var results = new List<int>(parallel * times);
            var i = 0;

            Func<Task> action = async () =>
            {
                for (var j = 0; j < times; j++)
                {
                    var result = await locker.WithLock(async () =>
                    {
                        await Task.Delay(0);

                        results.Add(i);

                        i++;

                        return Try.Success();
                    });

                    Assert.True(result.IsSuccess);
                }
            };

            Parallel.For(0, parallel, id =>
            {
                action.Invoke().Wait();
            });

            var array = results.ToArray();

            for (var j = 0; j < array.Length; j++)
            {
                if (j != 0)
                {
                    Assert.True(array[j - 1] < array[j]);
                }
            }
        }

        [Theory,
        InlineData(5, 50),
        InlineData(10, 100),
        InlineData(100, 10)]
        public void AddStressToTypedLocker(int parallel, int times)
        {
            var locker = new TypedAsyncLocker();
            var results = new List<int>(parallel * times);
            var i = 0;

            Func<Task> action = async () =>
            {
                for (var j = 0; j < times; j++)
                {
                    var result = await locker.WithLock<int>(async () =>
                    {
                        await Task.Delay(0);

                        results.Add(i);

                        i++;

                        return Try<int>.Success(i);
                    });

                    Assert.True(result.IsSuccess);
                }
            };

            Parallel.For(0, parallel, id =>
            {
                action.Invoke().Wait();
            });

            var array = results.ToArray();

            for (var j = 0; j < array.Length; j++)
            {
                if (j != 0)
                {
                    Assert.True(array[j - 1] < array[j]);
                }
            }
        }
    }
}
