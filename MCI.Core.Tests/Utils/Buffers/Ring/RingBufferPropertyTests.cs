namespace Miharu.Utils.Buffers.Ring
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Miharu.Utils;
    using Xunit;

    public class RingBufferPropertyTests
    {
        public static IEnumerable<object[]> GetCountTestSource()
        {
            yield return new object[] { 0, new int[] { } };
            yield return new object[] { 1, new int[] { 0 } };
            yield return new object[] { 2, new int[] { 0, 1 } };
            yield return new object[] { 3, new int[] { 0, 1, 2 } };
            yield return new object[] { 4, new int[] { 0, 1, 2, 3 } };
            yield return new object[] { 5, new int[] { 0, 1, 2, 3, 4 } };
        }

        [Theory, MemberData(nameof(GetCountTestSource))]
        public void CountTest(int expected, int[] source)
        {
            var buffer = new RingBuffer<int>(source.Length * 2, source);

            Assert.Equal(expected, buffer.Count);
        }

        [Theory, MemberData(nameof(GetCountTestSource))]
        public void CountTestWithShift(int expected, int[] source)
        {
            var buffer = new RingBuffer<int>(source.Length * 2, source);

            for (var i = 0; i < source.Length * 3; i++)
            {
                buffer.InsertLast(i);
                buffer.RemoveFirst();

                Assert.Equal(expected, buffer.Count);
            }
        }


    }
}
