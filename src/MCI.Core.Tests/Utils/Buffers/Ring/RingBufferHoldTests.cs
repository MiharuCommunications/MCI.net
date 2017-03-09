namespace Miharu.Core.Tests.Utils.Buffers.Ring
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;
    using Miharu.Utils;

    public class RingBufferHoldTests
    {
        public static IEnumerable<object> GetEnumerateSource()
        {
            yield return new object[] { new int[] { } };
            yield return new object[] { new int[] { 1 } };
            yield return new object[] { new int[] { 1, 2 } };
            yield return new object[] { new int[] { 1, 2, 3 } };
            yield return new object[] { new int[] { 1, 2, 3, 4 } };
            yield return new object[] { new int[] { 1, 2, 3, 4, 5 } };
        }

        [Theory, MemberData("GetEnumerateSource")]
        public void EnumerateTest(int[] source)
        {
            var buffer = new RingBuffer<int>(source.Length);

            foreach (var item in source)
            {
                buffer.InsertLast(item);
            }

            Assert.Equal(source, buffer.ToArray());
        }

        [Theory, MemberData("GetEnumerateSource")]
        public void HoldWithConstructorTest(int[] source)
        {
            var buffer = new RingBuffer<int>(source.Length, source);

            Assert.Equal(source, buffer.ToArray());
        }


    }
}
