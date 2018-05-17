namespace Miharu.Errors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BufferOverflowError : Error
    {
        public BufferOverflowError(int capacity)
        {
            this.Capacity = capacity;
            this.ErrorMessage = "バッファーがオーバーフローしました。 Capacity == " + capacity.ToString();
        }

        public int Capacity { get; private set; }
    }
}
