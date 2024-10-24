namespace Miharu.Utils.Buffers.Ring
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Miharu.Utils;
    using Xunit;

    public class RingBufferInsertTests
    {
        public static IEnumerable<object[]> GetInsertFirstTestSource()
        {
            yield return new object[] { new int[] { 1, 2, 3, 4 }, 1, new int[] { 2, 3, 4 } };
        }

        [Theory, MemberData(nameof(GetInsertFirstTestSource))]
        public void InsertFirstTest(int[] expected, int item, int[] source)
        {
            var buffer = new RingBuffer<int>(source.Length * 2, source);

            var result = buffer.InsertFirst(item);

            Assert.True(result.IsRight);
            Assert.Equal(expected, buffer.ToArray());
        }



        public static IEnumerable<object[]> GetInsertLastTestSource()
        {
            yield return new object[] { new int[] { 1, 2, 3, 4 }, new int[] { 1, 2, 3 }, 4 };
        }

        [Theory, MemberData(nameof(GetInsertLastTestSource))]
        public void InsertLastTest(int[] expected, int[] source, int item)
        {
            var buffer = new RingBuffer<int>(source.Length * 2, source);

            var result = buffer.InsertLast(item);

            Assert.True(result.IsRight);
            Assert.Equal(expected, buffer.ToArray());
        }



        public static IEnumerable<object[]> GetInsertLastsTestSource()
        {
            yield return new object[] { new int[] { 1, 2, 3, 4, 5 }, new int[] { 1, 2, 3 }, new int[] { 4, 5 } };
            yield return new object[] { new int[] { 1, 2, 3, 4, 5, 6 }, new int[] { 1, 2, 3 }, new int[] { 4, 5, 6 } };
        }

        [Theory, MemberData(nameof(GetInsertLastsTestSource))]
        public void InsertLastsTest(int[] expected, int[] source, int[] inserteds)
        {
            var buffer = new RingBuffer<int>(source.Length * 2, source);

            var result = buffer.InsertLast(inserteds);

            Assert.True(result.IsRight);
            Assert.Equal(expected, buffer.ToArray());
        }

    }
}
