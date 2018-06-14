namespace Miharu.Async.Schedulers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Miharu.Async.Schedulers;
    using Xunit;

    public class EveryMinuteActionTests
    {
        [Fact(Skip = "required too long time.")]
        public async Task Run()
        {
            var list = new List<DateTime>();
            var task = new EveryMinuteAction(0, dt =>
            {
                list.Add(dt);
            });


            await Task.Delay(TimeSpan.FromMinutes(60));

            return;
        }
    }
}
