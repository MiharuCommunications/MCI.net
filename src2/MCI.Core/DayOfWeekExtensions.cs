//-----------------------------------------------------------------------
// <copyright file="DayOfWeekExtensions.cs" company="Miharu Communications Inc.">
//     © 2015 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu
{
    using System;

    /// <summary>
    /// DayOfWeek 型を拡張します
    /// </summary>
    public static class DayOfWeekExtensions
    {
        /// <summary>
        /// 次に来る曜日
        /// </summary>
        /// <param name="current">対象となる DayOfWeek</param>
        /// <returns>次に来る曜日</returns>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        public static DayOfWeek Next(this DayOfWeek current)
        {
            switch (current)
            {
                case DayOfWeek.Sunday:
                    return DayOfWeek.Monday;

                case DayOfWeek.Monday:
                    return DayOfWeek.Tuesday;

                case DayOfWeek.Tuesday:
                    return DayOfWeek.Wednesday;

                case DayOfWeek.Wednesday:
                    return DayOfWeek.Thursday;

                case DayOfWeek.Thursday:
                    return DayOfWeek.Friday;

                case DayOfWeek.Friday:
                    return DayOfWeek.Saturday;

                case DayOfWeek.Saturday:
                    return DayOfWeek.Sunday;

                default:
                    throw new ArgumentOutOfRangeException("current");
            }
        }

        /// <summary>
        /// 前日の曜日
        /// </summary>
        /// <param name="current">対象となる DayOfWeek</param>
        /// <returns>前日の曜日</returns>
        public static DayOfWeek Previous(this DayOfWeek current)
        {
            switch (current)
            {
                case DayOfWeek.Sunday:
                    return DayOfWeek.Saturday;

                case DayOfWeek.Monday:
                    return DayOfWeek.Sunday;

                case DayOfWeek.Tuesday:
                    return DayOfWeek.Monday;

                case DayOfWeek.Wednesday:
                    return DayOfWeek.Tuesday;

                case DayOfWeek.Thursday:
                    return DayOfWeek.Wednesday;

                case DayOfWeek.Friday:
                    return DayOfWeek.Thursday;

                case DayOfWeek.Saturday:
                    return DayOfWeek.Friday;

                default:
                    throw new ArgumentOutOfRangeException("current");
            }
        }
    }
}
