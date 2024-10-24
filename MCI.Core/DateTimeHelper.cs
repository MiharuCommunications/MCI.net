//-----------------------------------------------------------------------
// <copyright file="DateTimeHelper.cs" company="Miharu Communications Inc.">
//     © 2016 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Helper methods for DateTime
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        /// Enumerate dates of given year
        /// </summary>
        /// <param name="year">列挙対象となる年</param>
        /// <returns></returns>
        public static IEnumerable<DateTime> EnumerateDatesInYear(int year)
        {
            var current = new DateTime(year, 1, 1);
            var d = TimeSpan.FromDays(1.0);

            while (current.Year == year)
            {
                yield return current.Date;

                current += d;
            }
        }

        /// <summary>
        /// Enumerate dates of given month
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static IEnumerable<DateTime> EnumerateDatesInMonth(int year, int month)
        {
            var current = new DateTime(year, month, 1);
            var d = TimeSpan.FromDays(1.0);

            while (current.Month == month)
            {
                yield return current.Date;

                current += d;
            }
        }
    }
}
