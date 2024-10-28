namespace Miharu.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Xunit;

    public class DateTimeHelperTests
    {

        [Theory,
        InlineData(365, 1000),
        InlineData(366, 2000),
        InlineData(365, 2001),
        InlineData(365, 2002),
        InlineData(365, 2003),
        InlineData(366, 2004),
        InlineData(365, 2015),
        InlineData(366, 2016),
        ]
        public void EnumerateDatesInYearCountTest(int count, int year)
        {
            var results = DateTimeHelper.EnumerateDatesInYear(year).ToArray();

            Assert.Equal(count, results.Length);

            foreach (var actual in results)
            {
                Assert.Equal(year, actual.Year);
            }
        }

        [Theory,
        InlineData(31, 2015, 1),
        InlineData(28, 2015, 2),
        InlineData(31, 2015, 3),
        InlineData(30, 2015, 4),
        InlineData(31, 2015, 5),
        InlineData(30, 2015, 6),
        InlineData(31, 2015, 7),
        InlineData(31, 2015, 8),
        InlineData(30, 2015, 9),
        InlineData(31, 2015, 10),
        InlineData(30, 2015, 11),
        InlineData(31, 2015, 12),
        InlineData(29, 2016, 2),
        ]
        public void EnumerateDatesInMonthCountTest(int count, int year, int month)
        {
            var results = DateTimeHelper.EnumerateDatesInMonth(year, month).ToArray();

            Assert.Equal(count, results.Length);

            foreach (var actual in results)
            {
                Assert.Equal(year, actual.Year);
                Assert.Equal(month, actual.Month);
            }
        }
    }
}
