namespace Miharu
{
    using System.Collections.Generic;
    using Xunit;

    public class ArrayExtensionsTests
    {
        [Fact]
        public void Divide()
        {
            var result = (new int[5] { 1, 2, 3, 4, 5 }).Divide(2);

            Assert.True(result[0].Length == 2);
            Assert.True(result[1].Length == 2);
            Assert.True(result[2].Length == 1);

            Assert.True(result[2][0] == 5);


            result = (new int[4] { 1, 2, 3, 4 }).Divide(2);

            Assert.True(result[0].Length == 2);
            Assert.True(result[1].Length == 2);

            Assert.True(result[1][1] == 4);
        }


        public static IEnumerable<object[]> GetIsSameSource()
        {
            yield return new object[] { true, new int[] { }, new int[] { } };
            yield return new object[] { true, new int[] { 1 }, new int[] { 1 } };
            yield return new object[] { true, new int[] { 1, 2, 3, 4, 5 }, new int[] { 1, 2, 3, 4, 5 } };

            yield return new object[] { false, new int[] { 1 }, new int[] { } };
            yield return new object[] { false, new int[] { }, new int[] { 1 } };
            yield return new object[] { false, new int[] { 1, 2, 3, 4 }, new int[] { 1, 2, 3, 4, 5 } };
            yield return new object[] { false, new int[] { 1, 2, 3, 4, 5 }, new int[] { 1, 2, 3, 4 } };
        }

        [Theory, MemberData(nameof(GetIsSameSource))]
        public void IsSameTest(bool expected, int[] source1, int[] source2)
        {
            Assert.Equal(expected, source1.IsSame(source2));
        }
    }
}
