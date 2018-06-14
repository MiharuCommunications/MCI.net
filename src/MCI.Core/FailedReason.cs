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
        string ErrorMessage { get; }
    }

    public class NoError : IFailedReason
    {
        public string ErrorMessage { get; private set; }

        public NoError()
        {
            this.ErrorMessage = "エラーはありません。";
        }
    }

    public class TimeoutError : IFailedReason
    {
        public TimeSpan Timeout { get; private set; }

        public string ErrorMessage { get; private set; }

        public TimeoutError(TimeSpan timeout)
        {
            this.Timeout = timeout;

            this.ErrorMessage = "処理がタイムアウトしました Timeout = " + this.Timeout.TotalMilliseconds.ToString() + " [ms]";
        }
    }

    public class UnresolvedError : IFailedReason
    {
        public Exception SourceException { get; private set; }

        public string ErrorMessage { get; private set; }

        public UnresolvedError(Exception source)
        {
            this.SourceException = source;
            this.ErrorMessage = "未解決の例外です。";
        }

        public UnresolvedError(Exception source, string message)
        {
            this.SourceException = source;
            this.ErrorMessage = message;
        }
    }

    public class BufferOverflowError : IFailedReason
    {
        public int Capacity { get; private set; }

        public string ErrorMessage { get; private set; }

        public BufferOverflowError(int capacity)
        {
            this.Capacity = capacity;
            this.ErrorMessage = "バッファーがオーバーフローしました。 Capacity == " + capacity.ToString();
        }
    }

    public class NotImplementedError : IFailedReason
    {
        public string ErrorMessage { get; private set; }

        public NotImplementedError()
        {
            this.ErrorMessage = "この機能はまだ実装されていません。";
        }
    }

    public class InvalidFormatError : IFailedReason
    {
        public string ErrorMessage { get; private set; }

        public InvalidFormatError(string message)
        {
            this.ErrorMessage = message;
        }
    }

    [Obsolete("他のモノを考えたい")]
    public class ArgumentOutOfRangeError : IFailedReason
    {
        public string ParameterName { get; private set; }

        public string ErrorMessage { get; private set; }

        public ArgumentOutOfRangeError(string parameterName)
        {
            this.ParameterName = parameterName;
            this.ErrorMessage = "引数 \"" + parameterName + "\" に指定された値が範囲外です。";
        }
    }

    public class ReachMaximumRetryError : IFailedReason
    {
        public int RetryCount { get; private set; }

        public string ErrorMessage { get; private set; }

        public ReachMaximumRetryError(int count)
        {
            this.RetryCount = count;
            this.ErrorMessage = "リトライ回数が上限値に達しました RetryCount = " + count.ToString();
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
