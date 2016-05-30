namespace Miharu.Core.Tests.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    public class StringExtensionsTests
    {
        public static object[] ConcatSourceSeparatedString
        {
            get
            {
                return new object[]
                {
                    new object[] { new string[] { }, "bb", "" },
                    new object[] { new string[] { "" }, "bb", "" },
                    new object[] { new string[] { "aa" }, "bb", "aa" },
                    new object[] { new string[] { "aa", "aa", "aa" }, "bb", "aabbaabbaa" }
                };
            }
        }


        [Theory, MemberData("ConcatSourceSeparatedString")]
        public void ConcatTest(string[] strs, string separator, string expected)
        {
            Assert.Equal(expected, strs.Concat(separator));

            IEnumerable<string> em = strs;
            Assert.Equal(expected, em.Concat(separator));
        }



        public static object[] DivideSource =
        {
            new object[]{ "123123123123", 3, new string[] { "123", "123", "123", "123" } },
        };


        [Theory, MemberData("DivideSource")]
        public void DivideTest(string source, int count, string[] dest)
        {
            Assert.Equal(dest, source.Divide(count));
        }



        public static object[] IntercalateSource =
        {
            new object[] { string.Empty, new string[] {  }, string.Empty },
            new object[] { "a", new string[] { "a" }, string.Empty },
        };

        [Theory, MemberData("IntercalateSource")]
        public void Intercalate(string expected, string[] source, string separator)
        {
            Assert.Equal(expected, source.Intercalate(separator));
        }
    }
}
