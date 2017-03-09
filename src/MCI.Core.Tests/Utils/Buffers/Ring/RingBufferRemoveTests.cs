namespace Miharu.Core.Tests.Utils.Buffers.Ring
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Miharu.Utils;
    using Xunit;

    public class RingBufferRemoveTests
    {



        public static IEnumerable<object> GetRemoveLastTestSource()
        {
            yield return new object[] { new int[] { 1, 2, 3, 4, 5 }, new int[] { 1, 2, 3, 4, 5 }, 0 };
            yield return new object[] { new int[] { 1, 2, 3, 4 }, new int[] { 1, 2, 3, 4, 5 }, 1 };
            yield return new object[] { new int[] { 1, 2, 3 }, new int[] { 1, 2, 3, 4, 5 }, 2 };
            yield return new object[] { new int[] { 1, 2 }, new int[] { 1, 2, 3, 4, 5 }, 3 };
            yield return new object[] { new int[] { 1 }, new int[] { 1, 2, 3, 4, 5 }, 4 };
            yield return new object[] { new int[] { }, new int[] { 1, 2, 3, 4, 5 }, 5 };
        }

        [Theory, MemberData("GetRemoveLastTestSource")]
        public void RemoveLastTest(int[] expected, int[] source, int length)
        {
            var buffer = new RingBuffer<int>(source.Length * 2, source);

            var result = buffer.RemoveLast(length);

            Assert.True(result.IsRight);
            Assert.Equal(expected, buffer.ToArray());
        }



        public static IEnumerable<object> GetRemoveFirstTestSource()
        {
            yield return new object[] { new int[] { 1, 2, 3, 4, 5 }, new int[] { 1, 2, 3, 4, 5 }, 0 };
            yield return new object[] { new int[] { 2, 3, 4, 5 }, new int[] { 1, 2, 3, 4, 5 }, 1 };
            yield return new object[] { new int[] { 3, 4, 5 }, new int[] { 1, 2, 3, 4, 5 }, 2 };
            yield return new object[] { new int[] { 4, 5 }, new int[] { 1, 2, 3, 4, 5 }, 3 };
            yield return new object[] { new int[] { 5 }, new int[] { 1, 2, 3, 4, 5 }, 4 };
            yield return new object[] { new int[] { }, new int[] { 1, 2, 3, 4, 5 }, 5 };
        }

        [Theory, MemberData("GetRemoveFirstTestSource")]
        public void RemoveFirstTest(int[] expected, int[] source, int length)
        {
            var buffer = new RingBuffer<int>(source.Length * 2, source);

            var result = buffer.RemoveFirst(length);

            Assert.True(result.IsRight);
            Assert.Equal(expected, buffer.ToArray());
        }
    }
}
