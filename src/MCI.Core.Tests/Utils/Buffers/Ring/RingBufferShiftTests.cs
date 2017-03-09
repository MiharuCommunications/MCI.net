namespace Miharu.Core.Tests.Utils.Buffers.Ring
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Miharu.Utils;
    using Xunit;

    public class RingBufferShiftTests
    {
        public static IEnumerable<object> GetShiftTestSource()
        {
            yield return new object[] { new int[] { } };
            yield return new object[] { new int[] { 1 } };
        }

        [Theory, MemberData("GetShiftTestSource")]
        public void ShiftTest(int[] source)
        {
            var buffer = new RingBuffer<int>(source.Length * 2, source);

            for (var i = 0; i < 100; i++)
            {
                buffer.InsertLast(i);
                buffer.RemoveFirst();

                Assert.Equal(source.Length, buffer.Count);
            }

            for (var i = 0; i < 100; i++)
            {
                buffer.InsertLast(i);
                buffer.RemoveFirst(1);

                Assert.Equal(source.Length, buffer.Count);
            }

            for (var i = 0; i < 100; i++)
            {
                buffer.InsertFirst(i);
                buffer.RemoveLast();

                Assert.Equal(source.Length, buffer.Count);
            }

            for (var i = 0; i < 100; i++)
            {
                buffer.InsertFirst(i);
                buffer.RemoveLast(1);

                Assert.Equal(source.Length, buffer.Count);
            }


            for (var i = 0; i < 100; i++)
            {
                buffer.InsertFirst(i);
                buffer.RemoveFirst();

                Assert.Equal(source.Length, buffer.Count);
            }

            for (var i = 0; i < 100; i++)
            {
                buffer.InsertFirst(i);
                buffer.RemoveFirst(1);

                Assert.Equal(source.Length, buffer.Count);
            }

            for (var i = 0; i < 100; i++)
            {
                buffer.InsertLast(i);
                buffer.RemoveLast();

                Assert.Equal(source.Length, buffer.Count);
            }

            for (var i = 0; i < 100; i++)
            {
                buffer.InsertLast(i);
                buffer.RemoveLast(1);

                Assert.Equal(source.Length, buffer.Count);
            }

        }
    }
}
