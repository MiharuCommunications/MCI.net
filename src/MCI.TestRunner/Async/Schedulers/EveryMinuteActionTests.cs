using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Miharu.Async.Schedulers;

namespace Miharu.TestRunner.Async.Schedulers
{
    public class EveryMinuteActionTests : IDisposable
    {
        private bool disposed;
        private IDisposable action;

        public EveryMinuteActionTests(int second)
        {
            this.disposed = false;

            this.action = PeriodicScheduler.OnEveryMinute(second, (dt) =>
            {
                if (dt.Second != second)
                {
                    Logger.AddLog("PeriodicScheduler", "OnEveryMinute", (dt.Second - second).ToString() + "秒ズレてる");
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
