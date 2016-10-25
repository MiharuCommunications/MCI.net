namespace Miharu.TestRunner.Async.Schedulers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Miharu.Async.Schedulers;

    public class EveryDayActionTests : IDisposable
    {
        private bool disposed;
        private IDisposable action;

        public EveryDayActionTests(int hour, int minute, int second)
        {
            this.disposed = false;

            this.action = PeriodicScheduler.OnEveryDay(hour, minute, second, (dt) =>
            {
                if (dt.Hour != hour || dt.Minute != minute || dt.Second != second)
                {
                    Logger.AddLog("PeriodicScheduler", "OnEveryDay", "ズレてる");
                }
            });
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
                this.action.Dispose();
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
