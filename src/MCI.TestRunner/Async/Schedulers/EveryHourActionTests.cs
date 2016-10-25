using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Miharu.Async.Schedulers;

namespace Miharu.TestRunner.Async.Schedulers
{
    public class EveryHourActionTests : IDisposable
    {
        private bool disposed;
        private IDisposable action;

        public EveryHourActionTests(int minute, int second)
        {
            this.disposed = false;

            this.action = PeriodicScheduler.OnEveryHour(minute, second, (dt) =>
            {
                if (dt.Minute != minute || dt.Second != second)
                {
                    Logger.AddLog("PeriodicScheduler", "OnEveryHour", "ズレてる");
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
