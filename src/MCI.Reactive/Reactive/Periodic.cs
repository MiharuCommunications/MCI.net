//-----------------------------------------------------------------------
// <copyright file="Periodic.cs" company="Miharu Communications Inc.">
//     © 2016 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu.Reactive
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Miharu.Async.Schedulers;

    public static class Periodic
    {
        public static IObservable<DateTime> EveryDay(int hour, int minute, int second)
        {
            return Observable.Create<DateTime>(obs =>
            {
                return PeriodicScheduler.OnEveryDay(hour, minute, second, dt =>
                {
                    obs.OnNext(dt);
                });
            });
        }

        public static IObservable<DateTime> EveryHour(int minute, int second)
        {
            return Observable.Create<DateTime>(obs =>
            {
                return PeriodicScheduler.OnEveryHour(minute, second, dt =>
                {
                    obs.OnNext(dt);
                });
            });
        }

        public static IObservable<DateTime> EveryMinute(int second)
        {
            return Observable.Create<DateTime>(obs =>
            {
                return PeriodicScheduler.OnEveryMinute(second, dt =>
                {
                    obs.OnNext(dt);
                });
            });
        }


        public static IObservable<DateTime> Interval(TimeSpan period)
        {
            throw new NotImplementedException();
        }
    }
}
