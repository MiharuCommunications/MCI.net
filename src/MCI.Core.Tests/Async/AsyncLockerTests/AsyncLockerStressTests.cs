using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Miharu.Async;
using Xunit;

namespace Miharu.Core.Tests.Async.AsyncLockerTests
{
    public class AsyncLockerStressTests
    {
        [Fact]
        public void StressTestAsync()
        {
            const int N = 100;

            var list = new List<int>(N * 100);
            var locker = new AsyncLocker();


            Func<Task> action = async () =>
            {
                for (var i = 0; i < 100; i++)
                {
                    await locker.WithLock(async () =>
                    {
                        var result = 0;
                        foreach (var item in list)
                        {
                            result = item;
                        }

                        list.Add(i);
                    });
                }
            };


            Parallel.For(0, N, id => // こう書くだけで、並行して処理が行われる
            {
                action.Invoke().Wait();
            });
        }
    }
}
