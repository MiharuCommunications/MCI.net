namespace Miharu.Collections
{
    using System;

    internal static class DateHash
    {
        public static int ToHash(DateTime date)
        {
            return ((date.Year * 12 + date.Month - 1) * 31) + date.Day - 1;
        }

        public static DateTime ToDateTime(int hash)
        {
            var d = hash % 31 + 1;
            var m = ((hash / 31) % 12) + 1;
            var y = hash / (31 * 12);

            return new DateTime(y, m, d);
        }
    }
}
