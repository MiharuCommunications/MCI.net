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
    }

    public interface IUnkownError : IError
    {
        Exception InnerException { get; }
        string Message { get; }
    }

    public class UnkownError : IUnkownError
    {
        public Exception InnerException { get; private set; }

        public string Message { get; private set; }

        public UnkownError(Exception exception)
        {
            this.InnerException = exception;
            this.Message = "原因不明のエラーです。";
        }

        public UnkownError(Exception exception, string message)
        {
            this.InnerException = exception;
            this.Message = message;
        }
    }

    public interface IFormatError : IError
    {
        string Message { get; }
    }

    public class FormatError : IFormatError
    {
        public string Message { get; private set; }

        public FormatError(string message)
        {
            this.Message = message;
        }
    }
}
