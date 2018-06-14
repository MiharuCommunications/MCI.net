namespace Miharu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Error is the root class for all Error classes in MCI.net
    /// </summary>
    public interface IFailedReason
    {
    }

    public class NoError : IFailedReason
    {
        public NoError()
        {
        }
    }

    public class TimeoutError : IFailedReason
    {
        public TimeSpan Timeout { get; private set; }

        public TimeoutError(TimeSpan timeout)
        {
            this.Timeout = timeout;
        }
    }

    public class UnresolvedError : IFailedReason
    {
        public Exception SourceException { get; private set; }

        public UnresolvedError(Exception source)
        {
            this.SourceException = source;
        }

        public UnresolvedError(Exception source, string message)
        {
            this.SourceException = source;
        }
    }

    public class BufferOverflowError : IFailedReason
    {
        public int Capacity { get; private set; }

        public BufferOverflowError(int capacity)
        {
            this.Capacity = capacity;
        }
    }

    public class NotImplementedError : IFailedReason
    {
        public NotImplementedError()
        {
        }
    }

    public class InvalidFormatError : IFailedReason
    {
        public InvalidFormatError(string message)
        {
        }
    }

    [Obsolete("他のモノを考えたい")]
    public class ArgumentOutOfRangeError : IFailedReason
    {
        public string ParameterName { get; private set; }

        public ArgumentOutOfRangeError(string parameterName)
        {
            this.ParameterName = parameterName;
        }
    }

    public class ReachMaximumRetryError : IFailedReason
    {
        public int RetryCount { get; private set; }

        public ReachMaximumRetryError(int count)
        {
            this.RetryCount = count;
        }
    }

    public class UnkownError : IFailedReason
    {
        public string ErrorMessage { get; private set; }

        public UnkownError(string message)
        {
            this.ErrorMessage = message;
        }
    }
}
