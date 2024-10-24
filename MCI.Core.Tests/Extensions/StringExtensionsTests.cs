namespace Miharu.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    public class StringExtensionsTests
    {
        public static IEnumerable<object[]> GetConcatSeparatedWithStringSource()
        {
            yield return new object[] { "", new string[] { }, "bb" };
            yield return new object[] { "", new string[] { "" }, "bb" };
            yield return new object[] { "aa", new string[] { "aa" }, "bb" };
            yield return new object[] { "aabbaabbaa", new string[] { "aa", "aa", "aa" }, "bb" };
        }

        [Theory, MemberData(nameof(GetConcatSeparatedWithStringSource))]
        public void ConcatTest(string expected, string[] strs, string separator)
        {
            Assert.Equal(expected, strs.Concat(separator));

            IEnumerable<string> em = strs;
            Assert.Equal(expected, em.Concat(separator));
        }


        public static IEnumerable<object[]> GetDivideSource()
        {
            yield return new object[] { new string[] { "123", "123", "123", "123" }, "123123123123", 3 };
        }


        [Theory, MemberData(nameof(GetDivideSource))]
        public void DivideTest(string[] expected, string source, int length)
        {
            Assert.Equal(expected, source.Divide(length));
        }


        public static IEnumerable<object[]> GetIntercalateSource()
        {
            yield return new object[] { string.Empty, new string[] { }, string.Empty };
            yield return new object[] { "a", new string[] { "a" }, string.Empty };
        }

        [Theory, MemberData(nameof(GetIntercalateSource))]
        public void Intercalate(string expected, string[] source, string separator)
        {
            Assert.Equal(expected, source.Intercalate(separator));
        }
    }
}
