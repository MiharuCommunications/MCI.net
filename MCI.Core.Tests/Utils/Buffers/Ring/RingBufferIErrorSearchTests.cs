namespace Miharu.Utils.Buffers.Ring
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;
    using Miharu.Utils;
    using Miharu.Errors;

    public class RingBufferIErrorSearchTests
    {
        [Fact]
        public void OverflowTest()
        {
            var buffer = new RingBuffer<byte>(200);
            var data = new byte[300];

            var i = 0;
            foreach (var d in data)
            {
                i++;

                var result = buffer.InsertLast(d);

                if (buffer.Capacity < i)
                {
                    Assert.True(result.IsLeft);
                    Assert.True(result.Left.Get() is BufferOverflowError);
                }
                else
                {
                    Assert.True(result.IsRight);
                }
            }
        }
    }
}
