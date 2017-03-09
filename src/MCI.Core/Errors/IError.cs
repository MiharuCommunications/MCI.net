namespace Miharu.Errors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// The IError is the root interface for all Error classes in Miharu Libraries.
    /// </summary>
    public interface IError
    {
        string ErrorMessage { get; }
    }




    public interface IMaximumRetryError : IError
    {
        int RetryCount { get; }
    }

    public class MaximumRetryError : IMaximumRetryError
    {
        public MaximumRetryError(int count)
        {
            this.RetryCount = count;
        }

        public int RetryCount { get; private set; }

        public string ErrorMessage
        {
            get
            {
                return "リトライ回数が上限値に達しました RetryCount = " + this.RetryCount.ToString();
            }
        }
    }


    public interface ITimeoutError : IError
    {
        TimeSpan Timeout { get; }
    }

    public class TimeoutError : ITimeoutError
    {
        public TimeSpan Timeout { get; private set; }

        public TimeoutError(TimeSpan timeout)
        {
            this.Timeout = timeout;
        }

        public string ErrorMessage
        {
            get
            {
                return "処理がタイムアウトしました Timeout = " + this.Timeout.TotalMilliseconds.ToString() + " [ms]";
            }
        }
    }
}
