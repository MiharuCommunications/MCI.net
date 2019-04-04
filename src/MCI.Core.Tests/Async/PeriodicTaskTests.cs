using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Miharu.Async;
using Xunit;

namespace Miharu.Async
{
    public class PeriodicTaskTests
    {
        [Fact(Skip = "required too long time.")]
        public async Task Execute()
        {
            var list = new List<int>();

            var task = new PeriodicTask(TimeSpan.FromMilliseconds(500), async () =>
            {
                list.Add(0);

                await Task.Delay(500);
            });

            await task.StartAsync();

            await Task.Delay(TimeSpan.FromSeconds(10));

            Assert.True(9 < list.Count);
        }




        [Fact(Skip = "required too long time.")]
        public async Task Interval()
        {
            var interval = TimeSpan.FromSeconds(1.0);
            var error = TimeSpan.FromMilliseconds(100);

            var previous = DateTime.Now;

            var task = new PeriodicTask(interval, async () =>
            {
                var now = DateTime.Now;
                var diff = now - previous;

                if (interval + error < diff)
                {
                    // 広すぎ
                    Assert.True(false);
                }

                if (diff < interval - error)
                {
                    // 狭すぎ
                    Assert.True(false);
                }

                previous = now;
            });

            await task.StartAsync();

            await Task.Delay(TimeSpan.FromSeconds(60));

            await task.StopAndWaitForFinishAsync();

            Assert.True(true);

        }



        [Fact(Skip = "required too long time.")]
        public async Task StopAsync()
        {
            var task = new PeriodicTask(TimeSpan.FromSeconds(2), async () =>
            {
            });


        }
    }
}
