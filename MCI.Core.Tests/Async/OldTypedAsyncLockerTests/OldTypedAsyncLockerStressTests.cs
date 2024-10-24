using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Miharu.Async;
using Xunit;

namespace Miharu.Async.OldTypedAsyncLockerTests
{
    public class OldTypedAsyncLockerStressTests
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
            var locker = new OldTypedAsyncLocker();
            var results = new List<int>(parallel * times);

            var i = -1;

            Func<Task> action = async () =>
            {
                for (var k = 0; k < times; k++)
                {
                    var result = await locker.WithLock<int>(async () =>
                    {
                        await Task.Delay(0);

                        i++;

                        results.Add(i);

                        return Try<int>.Success(i);
                    });

                    Assert.True(result.IsSuccess);
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
