namespace Miharu.Errors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ReachMaximumRetryError : Error
    {
        public int RetryCount { get; private set; }

        public ReachMaximumRetryError(int count)
        {
            this.RetryCount = count;
            this.ErrorMessage = "リトライ回数が上限値に達しました RetryCount = " + count.ToString();
        }
    }
}
