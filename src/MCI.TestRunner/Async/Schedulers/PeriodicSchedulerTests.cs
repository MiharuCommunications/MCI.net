using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miharu.TestRunner.Async.Schedulers
{
    public class PeriodicSchedulerTests : IDisposable
    {
        private bool disposed;
        private DisposableCollection collection;

        public PeriodicSchedulerTests()
        {
            this.disposed = false;
            this.collection = new DisposableCollection();

            this.collection.Add(new EveryDayActionTests(0, 0, 0));
            this.collection.Add(new EveryDayActionTests(1, 0, 0));
            this.collection.Add(new EveryDayActionTests(2, 0, 0));
            this.collection.Add(new EveryDayActionTests(3, 0, 0));
            this.collection.Add(new EveryDayActionTests(4, 0, 0));
            this.collection.Add(new EveryDayActionTests(5, 0, 0));
            this.collection.Add(new EveryDayActionTests(6, 0, 0));
            this.collection.Add(new EveryDayActionTests(7, 0, 0));
            this.collection.Add(new EveryDayActionTests(8, 0, 0));
            this.collection.Add(new EveryDayActionTests(9, 0, 0));
            this.collection.Add(new EveryDayActionTests(10, 0, 0));
            this.collection.Add(new EveryDayActionTests(11, 0, 0));
            this.collection.Add(new EveryDayActionTests(12, 0, 0));
            this.collection.Add(new EveryDayActionTests(13, 0, 0));
            this.collection.Add(new EveryDayActionTests(14, 0, 0));
            this.collection.Add(new EveryDayActionTests(15, 0, 0));
            this.collection.Add(new EveryDayActionTests(16, 0, 0));
            this.collection.Add(new EveryDayActionTests(17, 0, 0));
            this.collection.Add(new EveryDayActionTests(18, 0, 0));
            this.collection.Add(new EveryDayActionTests(19, 0, 0));
            this.collection.Add(new EveryDayActionTests(20, 0, 0));
            this.collection.Add(new EveryDayActionTests(21, 0, 0));
            this.collection.Add(new EveryDayActionTests(22, 0, 0));
            this.collection.Add(new EveryDayActionTests(23, 0, 0));


            this.collection.Add(new EveryHourActionTests(0, 0));
            this.collection.Add(new EveryHourActionTests(5, 0));
            this.collection.Add(new EveryHourActionTests(10, 0));
            this.collection.Add(new EveryHourActionTests(15, 0));
            this.collection.Add(new EveryHourActionTests(20, 0));
            this.collection.Add(new EveryHourActionTests(25, 0));
            this.collection.Add(new EveryHourActionTests(30, 0));
            this.collection.Add(new EveryHourActionTests(35, 0));
            this.collection.Add(new EveryHourActionTests(40, 0));
            this.collection.Add(new EveryHourActionTests(45, 0));
            this.collection.Add(new EveryHourActionTests(50, 0));
            this.collection.Add(new EveryHourActionTests(55, 0));

            this.collection.Add(new EveryMinuteActionTests(0));
            this.collection.Add(new EveryMinuteActionTests(5));
            this.collection.Add(new EveryMinuteActionTests(10));
            this.collection.Add(new EveryMinuteActionTests(15));
            this.collection.Add(new EveryMinuteActionTests(20));
            this.collection.Add(new EveryMinuteActionTests(25));
            this.collection.Add(new EveryMinuteActionTests(30));
            this.collection.Add(new EveryMinuteActionTests(35));
            this.collection.Add(new EveryMinuteActionTests(40));
            this.collection.Add(new EveryMinuteActionTests(45));
            this.collection.Add(new EveryMinuteActionTests(50));
            this.collection.Add(new EveryMinuteActionTests(55));
        }


        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            this.disposed = true;

            if (disposing)
            {
                // Dispose 処理
                this.collection.Dispose();
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
