namespace Miharu.Extensions.DateTimes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Xunit;

    public class NextEachTests
    {
        public static IEnumerable<object[]> GetNextEachMinuteTestSource()
        {
            yield return new object[] { "2016/5/1 0:1:0", "2016/5/1 0:0:1", 0 };
            yield return new object[] { "2016/5/1 0:0:0", "2016/5/1 0:0:0", 0 };

            yield return new object[] { "2015/8/10 10:30:30", "2015/8/10 10:30:20", 30 };
            yield return new object[] { "2015/8/10 10:30:30", "2015/8/10 10:30:30", 30 };
            yield return new object[] { "2015/8/10 10:31:30", "2015/8/10 10:30:40", 30 };
        }

        [Theory,
        MemberData(nameof(GetNextEachMinuteTestSource))]
        public void NextEachMinuteTest(string expected, string source, int second)
        {
            var e = DateTime.Parse(expected);
            var s = DateTime.Parse(source);

            Assert.Equal(e.Second, second);

            Assert.True(e.IsSameSecond(s.NextEachMinute(second)));
        }



        public static IEnumerable<object[]> GetNextEachHourTestSource()
        {
            yield return new object[] { "2016/5/1 1:0:0", "2016/5/1 0:1:1", 0, 0 };
            yield return new object[] { "2016/5/1 0:0:0", "2016/5/1 0:0:0", 0, 0 };

            yield return new object[] { "2015/8/10 10:30:30", "2015/8/10 10:30:20", 30, 30 };
            yield return new object[] { "2015/8/10 10:30:30", "2015/8/10 10:30:30", 30, 30 };
            yield return new object[] { "2015/8/10 11:30:30", "2015/8/10 10:30:40", 30, 30 };
        }

        [Theory,
        MemberData(nameof(GetNextEachHourTestSource))]
        public void NextEachHourTest(string expected, string source, int minute, int second)
        {
            var e = DateTime.Parse(expected);
            var s = DateTime.Parse(source);

            Assert.Equal(e.Second, second);

            Assert.True(e.IsSameSecond(s.NextEachHour(minute, second)));
        }




        public static IEnumerable<object[]> GetNextEachDaySource()
        {
            yield return new object[] { new DateTime(2015, 8, 10, 10, 30, 30), new DateTime(2015, 8, 10, 10, 30, 20), 10, 30, 30 };
            yield return new object[] { new DateTime(2015, 8, 10, 10, 30, 30), new DateTime(2015, 8, 10, 10, 30, 30), 10, 30, 30 };
            yield return new object[] { new DateTime(2015, 8, 11, 10, 30, 30), new DateTime(2015, 8, 10, 10, 30, 40), 10, 30, 30 };
        }

        [Theory, MemberData(nameof(GetNextEachDaySource))]
        public void NextEachDay(DateTime expected, DateTime from, int hour, int minute, int second)
        {
            Assert.True(expected.IsSameSecond(from.NextEachDay(hour, minute, second)));
        }


    }
}
