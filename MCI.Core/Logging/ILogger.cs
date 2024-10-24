//-----------------------------------------------------------------------
// <copyright file="ILogger.cs" company="Miharu Communications Inc.">
//     © 2017 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu.Logging
{
    using System;

    public interface ILogger
    {
        void Fatal(string message);
        void Fatal(string message, Exception error);
        void Fatal(string message, IFailedReason error);

        void Error(string message);
        void Error(string message, Exception error);
        void Error(string message, IFailedReason error);

        void Warn(string message);
        void Warn(string message, Exception error);
        void Warn(string message, IFailedReason error);

        void FixMe(string message);
        void FixMe(string message, Exception error);
        void FixMe(string message, IFailedReason error);

        void Info(string message);
        void Info(string message, Exception error);
        void Info(string message, IFailedReason error);

        void Debug(string message);
        void Debug(string message, Exception error);
        void Debug(string message, IFailedReason error);

        void Trace(string message);
        void Trace(string message, Exception error);
        void Trace(string message, IFailedReason error);

        bool IsFatalEnabled { get; }
        bool IsErrorEnabled { get; }
        bool IsWarnEnabled { get; }
        bool IsFixMeEnabled { get; }
        bool IsInfoEnabled { get; }
        bool IsDebugEnabled { get; }
        bool IsTraceEnabled { get; }
    }
}
