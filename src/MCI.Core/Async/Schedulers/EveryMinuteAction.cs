//-----------------------------------------------------------------------
// <copyright file="EveryMinuteAction.cs" company="Miharu Communications Inc.">
//     © 2016 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu.Async.Schedulers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public sealed class EveryMinuteAction : PeriodicScheduledAction
    {
        private int second;


        internal EveryMinuteAction(int second, Action<DateTime> action)
            : base()
        {
            this.second = second;

            this.Margin = TimeSpan.FromMilliseconds(500);
            this.DelayMargin = TimeSpan.FromMilliseconds(200);

            this.Start(action);
        }


        public TimeSpan Margin { get; private set; }
        public TimeSpan DelayMargin { get; private set; }


        protected override DateTime GetNext(DateTime now)
        {
            return now.NextEachMinute(this.second);
        }

        protected override bool HasBecameTime(DateTime now, DateTime next)
        {
            return next < now || (next - now) < this.Margin;
        }

        protected override TimeSpan GetDelay(DateTime now, DateTime next)
        {
            return next - now - this.DelayMargin;
        }

        protected override Task Skip()
        {
            return Task.Delay(this.DelayMargin);
        }

        protected override async Task DelayToNext(DateTime now, DateTime next)
        {
            var delay = next - now - this.DelayMargin;

            if (delay < this.DelayMargin)
            {
                return;
            }
            else
            {
                await Task.Delay(delay);
            }
        }
    }
}
