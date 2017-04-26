namespace Miharu.Core.Tests.Extensions.DateTimes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    public class CalculatingTests
    {
        [Theory,
        InlineData("2015/08/10 10:31:30", "2015/08/09 10:31:30"),
        InlineData("2015/09/01 10:31:30", "2015/08/31 10:31:30"),
        InlineData("2015/03/01 10:31:30", "2015/02/28 10:31:30"),
        InlineData("2016/03/01 10:31:30", "2016/02/29 10:31:30"),
        InlineData("2016/02/29 10:31:30", "2016/02/28 10:31:30"),
        ]
        public void GetTomorrowTest(string expected, string from)
        {
            var e = DateTime.Parse(expected);
            var f = DateTime.Parse(from);

            Assert.True(e.IsSameSecond(f.GetTomorrow()));
        }


        [Theory,
        InlineData("2015/08/10 10:31:30", "2015/08/11 10:31:30"),
        InlineData("2015/08/31 10:31:30", "2015/09/01 10:31:30"),
        InlineData("2015/02/28 10:31:30", "2015/03/01 10:31:30"),
        InlineData("2016/02/29 10:31:30", "2016/03/01 10:31:30"),
        InlineData("2016/02/28 10:31:30", "2016/02/29 10:31:30"),
        ]
        public void GetYesterdayTest(string expected, string from)
        {
            var e = DateTime.Parse(expected);
            var f = DateTime.Parse(from);

            Assert.True(e.IsSameSecond(f.GetYesterday()));
        }
    }
}
