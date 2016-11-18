namespace Miharu.Errors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IError
    {
    }


    public interface IMaximumRetryError : IError
    {
        int RetryCount { get; }
    }


    public interface ITimeoutError : IError
    {
        TimeSpan Timeout { get; }
    }

    public interface IUnkownError : IError
    {
        Exception InnerException { get; }
    }

    public class UnkownError : IUnkownError
    {
        public Exception InnerException { get; private set; }

        public string ErrorMessage { get; private set; }

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
    }
}
