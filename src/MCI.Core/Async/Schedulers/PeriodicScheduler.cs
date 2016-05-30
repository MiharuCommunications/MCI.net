//-----------------------------------------------------------------------
// <copyright file="PeriodicScheduler.cs" company="Miharu Communications Inc.">
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

    public static class PeriodicScheduler
    {
        public static IDisposable OnEveryDay(int hour, int minute, int second, Action<DateTime> action)
        {
            return new EveryDayAction(hour, minute, second, action);
        }

        public static IDisposable OnEveryHour(int minute, int second, Action<DateTime> action)
        {
            return new EveryHourAction(minute, second, action);
        }

        public static IDisposable OnEveryMinute(int second, Action<DateTime> action)
        {
            return new EveryMinuteAction(second, action);
        }
    }
}
