namespace Miharu.Errors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IBufferOverflowError : IError
    {
        int Capacity { get; }
    }

    public class BufferOverflowError : IBufferOverflowError
    {
        public BufferOverflowError(int capacity)
        {
            this.Capacity = capacity;
        }

        public int Capacity { get; private set; }

        public string ErrorMessage
        {
            get
            {
                return "バッファーがオーバーフローしました。 Capacity == " + this.Capacity.ToString();
            }
        }
    }
}
