namespace Miharu.Errors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TimeoutError : Error
    {
        public TimeSpan Timeout { get; private set; }

        public TimeoutError(TimeSpan timeout)
        {
            this.Timeout = timeout;

            this.ErrorMessage = "処理がタイムアウトしました Timeout = " + this.Timeout.TotalMilliseconds.ToString() + " [ms]";
        }
    }
}
