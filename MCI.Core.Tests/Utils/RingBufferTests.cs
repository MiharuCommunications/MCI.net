namespace Miharu.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Xunit;
    using Miharu.Utils;

    public class RingBufferTests
    {
        [Theory,
        InlineData(1, 1),
        InlineData(16, 16),
        InlineData(16, 15),
        InlineData(16, 14)]
        public void GetCapacity(int expected, int size)
        {
            Assert.Equal(expected, RingBuffer.GetCapacity(size));
        }


        [Fact]
        public void CapacityTestWithInsertLast()
        {
            var buffer = new RingBuffer<int>(3);

            Assert.Equal(0, buffer.Count);
            Assert.False(buffer.IsFull);

            buffer.InsertLast(1);
            Assert.Equal(1, buffer.Count);
            Assert.False(buffer.IsFull);

            buffer.InsertLast(2);
            Assert.Equal(2, buffer.Count);
            Assert.False(buffer.IsFull);

            buffer.InsertLast(3);
            Assert.Equal(3, buffer.Count);
            Assert.True(buffer.IsFull);

            /*
            Assert.Throws(typeof(OverflowException), () =>
            {
                buffer.InsertLast(0);
            });
            */
        }


        [Fact(Skip = "Either 版への移行に対応できていない")]
        public void CapacityTestWithInsertFirst()
        {
            var buffer = new RingBuffer<int>(3);

            Assert.Equal(0, buffer.Count);
            Assert.False(buffer.IsFull);

            buffer.InsertFirst(1);
            Assert.Equal(1, buffer.Count);
            Assert.False(buffer.IsFull);

            buffer.InsertFirst(2);
            Assert.Equal(2, buffer.Count);
            Assert.False(buffer.IsFull);

            buffer.InsertFirst(3);
            Assert.Equal(3, buffer.Count);
            Assert.True(buffer.IsFull);

            /*
            Assert.Throws(typeof(OverflowException), () =>
            {
                buffer.InsertFirst(0);
            });
            */
        }



        public static IEnumerable<object[]> IEnumeratorTestSource()
        {
            yield return new object[] { new int[] { } };
            yield return new object[] { new int[] { 1 } };
            yield return new object[] { new int[] { 1, 2 } };
            yield return new object[] { new int[] { 1, 2, 3 } };
            yield return new object[] { new int[] { 1, 2, 3, 4 } };
            yield return new object[] { new int[] { 1, 2, 3, 4, 5 } };
            yield return new object[] { Enumerable.Range(0, 1000).ToArray() };
        }

        [Theory, MemberData(nameof(IEnumeratorTestSource))]
        public void IEnumeratorTest(int[] collection)
        {
            var buffer = new RingBuffer<int>(collection.Length);

            foreach (var item in collection)
            {
                buffer.InsertLast(item);
            }

            Assert.Equal(collection, buffer.ToArray());
        }



        [Theory, MemberData(nameof(IEnumeratorTestSource))]
        public void IEnumeratorTestRemoveHead(int[] collection)
        {
            for (var remove = 0; remove < collection.Length; remove++)
            {
                var buffer = new RingBuffer<int>(collection.Length);

                foreach (var item in collection)
                {
                    buffer.InsertLast(item);
                }

                for (var i = 0; i < remove; i++)
                {
                    buffer.RemoveFirst();
                }


                Assert.Equal(collection.Drop(remove).ToArray(), buffer.ToArray());
            }
        }



        [Fact]
        public void StressTest()
        {
            const int N = 100000;
            var buffer = new RingBuffer<int>(N / 100);
            var rand = new Random();

        }
    }
}
