using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace Miharu.Core.Tests.Extensions
{
    public class DayOfWeekExtensionsTests
    {
        public static IEnumerable<object[]> GetNextTestSource()
        {
            yield return new object[] { DayOfWeek.Monday, DayOfWeek.Sunday };
            yield return new object[] { DayOfWeek.Tuesday, DayOfWeek.Monday };
            yield return new object[] { DayOfWeek.Wednesday, DayOfWeek.Tuesday };
            yield return new object[] { DayOfWeek.Thursday, DayOfWeek.Wednesday };
            yield return new object[] { DayOfWeek.Friday, DayOfWeek.Thursday };
            yield return new object[] { DayOfWeek.Saturday, DayOfWeek.Friday };
            yield return new object[] { DayOfWeek.Sunday, DayOfWeek.Saturday };
        }

        [Theory, MemberData("GetNextTestSource")]
        public void NextTest(DayOfWeek expected, DayOfWeek source)
        {
            Assert.Equal(expected, source.Next());
        }


        [Theory, MemberData("GetNextTestSource")]
        public void PreviousTest(DayOfWeek source, DayOfWeek expected)
        {
            Assert.Equal(expected, source.Previous());
        }
    }
}
