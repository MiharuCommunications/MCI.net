//-----------------------------------------------------------------------
// <copyright file="DateTimeExtensions.cs" company="Miharu Communications Inc.">
//     © 2015 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class DateTimeExtensions
    {
        public static bool IsSameYear(this DateTime self, DateTime other)
        {
            return self.Year == other.Year;
        }

        public static bool IsSameMonth(this DateTime self, DateTime other)
        {
            return self.Year == other.Year && self.Month == other.Month;
        }

        public static bool IsSameDate(this DateTime self, DateTime other)
        {
            return self.Year == other.Year && self.Month == other.Month && self.Day == other.Day;
        }

        public static bool IsSameSecond(this DateTime self, DateTime other)
        {
            return self.Year == other.Year && self.Month == other.Month && self.Day == other.Day && self.Hour == other.Hour && self.Minute == other.Minute && self.Second == other.Second;
        }

        public static IEnumerable<DateTime> To(this DateTime from, DateTime to)
        {
            // from と to も含む
            yield return from;

            if (from.IsSameDate(to))
            {
                yield break;
            }

            var inc = from < to ? 1 : -1;
            var temp = from.Date;

            while (true)
            {
                temp = temp.AddDays(inc);
                yield return temp;

                if (temp.IsSameDate(to))
                {
                    yield break;
                }
            }
        }


        public static DateTime Average(this IEnumerable<DateTime> collection)
        {
            var dates = collection.ToArray();
            var len = dates.Length;

            if (len == 0)
            {
                // !!!
                throw new ArgumentOutOfRangeException("DateTimeExtensions.Average");
            }

            if (len == 1)
            {
                return dates[0];
            }

            var cr = dates[0].Ticks;
            var temp = 0L;

            for (var i = 1; i < len; i++)
            {
                temp += dates[i].Ticks - cr;
            }

            return new DateTime(temp / len + cr);
        }

        public static IEnumerable<IEnumerable<T>> GroupedWithTimeSpan<T>(this IEnumerable<T> source, Func<T, DateTime> f, DateTime start, TimeSpan span)
        {
            var from = start;
            var to = start + span;
            var currents = new LinkedList<T>();


            foreach (var item in source.OrderBy(f))
            {
                var date = f(item);

                // from - to に入るまで移動
                while (to < date)
                {
                    if (currents.Count != 0)
                    {
                        yield return currents;

                        currents = new LinkedList<T>();
                    }

                    from = to;
                    to = from + span;
                }

                currents.AddLast(item);
            }

            if (currents.Count != 0)
            {
                yield return currents;
            }
        }

        /// <summary>
        /// 次に来る指定した秒の時刻を返す
        /// </summary>
        /// <param name="from">起点となる時刻</param>
        /// <param name="second">指定する秒</param>
        /// <returns>次の時刻</returns>
        public static DateTime NextEachMinute(this DateTime from, int second)
        {
            var comp = new DateTime(from.Year, from.Month, from.Day, from.Hour, from.Minute, second);

            if (from <= comp)
            {
                // 丁度同じ
                // 切りより前
                return comp;
            }
            else
            {
                // 切りより後
                // 次の時間の
                var next = from.AddMinutes(1.0);

                return new DateTime(next.Year, next.Month, next.Day, next.Hour, next.Minute, second);
            }
        }

        /// <summary>
        /// 次に来る指定した分、秒の時刻を返す
        /// </summary>
        /// <param name="from">起点となる時刻</param>
        /// <param name="minute">指定する分</param>
        /// <param name="second">指定する秒</param>
        /// <returns>次の時刻</returns>
        public static DateTime NextEachHour(this DateTime from, int minute, int second)
        {
            var comp = new DateTime(from.Year, from.Month, from.Day, from.Hour, minute, second);

            if (from <= comp)
            {
                // 丁度同じ
                // 切りより前
                return comp;
            }
            else
            {
                // 切りより後
                // 次の時間の
                var next = from.AddHours(1.0);

                return new DateTime(next.Year, next.Month, next.Day, next.Hour, minute, second);
            }
        }

        /// <summary>
        /// 次に来る指定した時、分、秒の時刻を返す
        /// </summary>
        /// <param name="from">起点となる時刻</param>
        /// <param name="hour">指定する時</param>
        /// <param name="minute">指定する分</param>
        /// <param name="second">指定する秒</param>
        /// <returns>次の時刻</returns>
        public static DateTime NextEachDay(this DateTime from, int hour, int minute, int second)
        {
            var comp = new DateTime(from.Year, from.Month, from.Day, hour, minute, second);

            if (from <= comp)
            {
                // 丁度同じ
                // 切りより前
                return comp;
            }
            else
            {
                // 切りより後
                var next = from.AddDays(1.0);

                return new DateTime(next.Year, next.Month, next.Day, hour, minute, second);
            }
        }


        public static IEnumerable<DateTime> EnumerateDays(this DateTime start, DateTime end)
        {
            var diff = start < end ? 1.0 : -1.0;

            var current = start;

            while (true)
            {
                yield return current.Date;

                if (current.IsSameDate(end))
                {
                    yield break;
                }

                current = current.AddDays(diff);
            }
        }

        public static IEnumerable<DateTime> EnumerateMonths(this DateTime start, DateTime end)
        {
            var diff = start < end ? 1 : -1;

            var current = start;

            while (true)
            {
                yield return new DateTime(current.Year, current.Month, 1);

                if (current.IsSameMonth(end))
                {
                    yield break;
                }

                current = current.AddMonths(diff);
            }
        }
    }
}
