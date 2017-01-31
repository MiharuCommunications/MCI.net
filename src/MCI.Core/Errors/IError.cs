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

    public interface INoError : IError
    {
    }

    public class NoError : INoError
    {
        public NoError()
        {
        }

        public string ErrorMessage
        {
            get
            {
                return "エラーはありません";
            }
        }
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

    public interface IUnkownError : IError
    {
        Exception InnerException { get; }
    }

    public class UnkownError : IUnkownError
    {
        public Exception InnerException { get; private set; }

        public UnkownError(Exception exception)
        {
            this.InnerException = exception;
            this.ErrorMessage = "原因不明のエラーです。";
        }

        public UnkownError(Exception exception, string message)
        {
            this.InnerException = exception;
            this.ErrorMessage = message;
        }

        public string ErrorMessage { get; private set; }
    }

}
