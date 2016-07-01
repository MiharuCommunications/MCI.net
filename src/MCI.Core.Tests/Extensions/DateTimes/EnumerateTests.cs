using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace Miharu.Core.Tests.Extensions.DateTimes
{
    public class EnumerateTests
    {
        public static IEnumerable<object[]> GetEnumerateDaysSource()
        {
            yield return new object[]
            {
                new DateTime[] { new DateTime(2016, 3, 25), new DateTime(2016, 3, 26), new DateTime(2016, 3, 27) },
                new DateTime(2016, 3, 25, 12, 11, 10), new DateTime(2016, 3, 27, 11, 10, 10)
            };
        }

        [Theory, MemberData("GetEnumerateDaysSource")]
        public void EnumerateDays(DateTime[] expected, DateTime start, DateTime end)
        {
            var result = start.EnumerateDays(end);

            Assert.True(expected.Length == result.Count());

            for (var i = 0; i < expected.Length; i++)
            {
                Assert.True(expected[i].IsSameDate(result.ElementAt(i)));
            }
        }


        public static IEnumerable<object[]> GetEnumerateMonthsSource()
        {
            yield return new object[]
            {
                new DateTime[] { new DateTime(2016, 3, 1), new DateTime(2016, 4, 1), new DateTime(2016, 5, 1) },
                new DateTime(2016, 3, 25, 12, 11, 10), new DateTime(2016, 5, 27, 11, 10, 10)
            };
        }

        [Theory, MemberData("GetEnumerateMonthsSource")]
        public void EnumerateMonths(DateTime[] expected, DateTime start, DateTime end)
        {
            var result = start.EnumerateMonths(end);

            Assert.True(expected.Length == result.Count());

            for (var i = 0; i < expected.Length; i++)
            {
                Assert.True(expected[i].IsSameDate(result.ElementAt(i)));
            }
        }

    }
}
