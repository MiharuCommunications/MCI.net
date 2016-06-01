using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace Miharu.Core.Tests.Extensions.DateTimes
{
    public class NextEachTests
    {
        public static object[] NextEachMinuteTestSource
        {
            get
            {
                return new object[]
                {
                    new object[] { "2016/5/1 0:1:0", "2016/5/1 0:0:1", 0 },
                    new object[] { "2016/5/1 0:0:0", "2016/5/1 0:0:0", 0 }
                };
            }
        }


        public static object[] NextEachHourTestSource
        {
            get
            {
                return new object[]
                {
                    new object[] { "2016/5/1 1:0:0", "2016/5/1 0:1:1", 0, 0 },
                    new object[] { "2016/5/1 0:0:0", "2016/5/1 0:0:0", 0, 0 }
                };
            }
        }



        [Theory,
        MemberData("NextEachMinuteTestSource")]
        public void NextEachMinuteTest(string expected, string source, int second)
        {
            var e = DateTime.Parse(expected);
            var s = DateTime.Parse(source);

            Assert.Equal(e.Second, second);

            Assert.True(e.IsSameSecond(s.NextEachMinute(second)));
        }


        [Theory,
        MemberData("NextEachHourTestSource")]
        public void NextEachHourTest(string expected, string source, int minute, int second)
        {
            var e = DateTime.Parse(expected);
            var s = DateTime.Parse(source);

            Assert.Equal(e.Second, second);

            Assert.True(e.IsSameSecond(s.NextEachHour(minute, second)));
        }




    }
}
