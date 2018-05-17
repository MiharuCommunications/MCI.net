namespace Miharu.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Xunit;
    using Miharu.Collections;

    public class DateHashTests
    {
        [Fact]
        public void ConvertTest()
        {
            var tody = DateTime.Today;
            for (var i = -30000; i < 30000; i++)
            {
                var date = tody + TimeSpan.FromDays(i);

                Assert.True(date.IsSameDate(DateHash.ToDateTime(DateHash.ToHash(date))));
            }
        }
    }
}
