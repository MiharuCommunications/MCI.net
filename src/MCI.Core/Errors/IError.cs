namespace Miharu.Errors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IError
    {
        string ErrorMessage { get; }
    }


    public interface IMaximumRetryError : IError
    {
        int RetryCount { get; }
    }


    public interface ITimeoutError : IError
    {
        TimeSpan Timeout { get; }
    }

    public interface IFileIOError : IError
    {
        string Target { get; }
    }
}
