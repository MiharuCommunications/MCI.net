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
        Either<IFailedReason, Unit> Fatal(string message);
        Either<IFailedReason, Unit> Fatal(string message, Exception error);
        Either<IFailedReason, Unit> Fatal(string message, IFailedReason error);

        Either<IFailedReason, Unit> Error(string message);
        Either<IFailedReason, Unit> Error(string message, Exception error);
        Either<IFailedReason, Unit> Error(string message, IFailedReason error);

        Either<IFailedReason, Unit> Warn(string message);
        Either<IFailedReason, Unit> Warn(string message, Exception error);
        Either<IFailedReason, Unit> Warn(string message, IFailedReason error);

        Either<IFailedReason, Unit> FixMe(string message);
        Either<IFailedReason, Unit> FixMe(string message, Exception error);
        Either<IFailedReason, Unit> FixMe(string message, IFailedReason error);

        Either<IFailedReason, Unit> Info(string message);
        Either<IFailedReason, Unit> Info(string message, Exception error);
        Either<IFailedReason, Unit> Info(string message, IFailedReason error);

        Either<IFailedReason, Unit> Debug(string message);
        Either<IFailedReason, Unit> Debug(string message, Exception error);
        Either<IFailedReason, Unit> Debug(string message, IFailedReason error);

        Either<IFailedReason, Unit> Trace(string message);
        Either<IFailedReason, Unit> Trace(string message, Exception error);
        Either<IFailedReason, Unit> Trace(string message, IFailedReason error);

        bool IsFatalEnabled { get; }
        bool IsErrorEnabled { get; }
        bool IsWarnEnabled { get; }
        bool IsFixMeEnabled { get; }
        bool IsInfoEnabled { get; }
        bool IsDebugEnabled { get; }
        bool IsTraceEnabled { get; }
    }
}
