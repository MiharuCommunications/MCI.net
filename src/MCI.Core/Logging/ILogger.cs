//-----------------------------------------------------------------------
// <copyright file="ILogger.cs" company="Miharu Communications Inc.">
//     © 2017 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu.Logging
{
    using Miharu.Errors;
    using System;

    public interface ILogger
    {
        Either<Error, Unit> Fatal(string message);
        Either<Error, Unit> Fatal(string message, Exception error);
        Either<Error, Unit> Fatal(string message, Error error);

        Either<Error, Unit> Error(string message);
        Either<Error, Unit> Error(string message, Exception error);
        Either<Error, Unit> Error(string message, Error error);

        Either<Error, Unit> Warn(string message);
        Either<Error, Unit> Warn(string message, Exception error);
        Either<Error, Unit> Warn(string message, Error error);

        Either<Error, Unit> FixMe(string message);
        Either<Error, Unit> FixMe(string message, Exception error);
        Either<Error, Unit> FixMe(string message, Error error);

        Either<Error, Unit> Info(string message);
        Either<Error, Unit> Info(string message, Exception error);
        Either<Error, Unit> Info(string message, Error error);

        Either<Error, Unit> Debug(string message);
        Either<Error, Unit> Debug(string message, Exception error);
        Either<Error, Unit> Debug(string message, Error error);

        Either<Error, Unit> Trace(string message);
        Either<Error, Unit> Trace(string message, Exception error);
        Either<Error, Unit> Trace(string message, Error error);

        bool IsFatalEnabled { get; }
        bool IsErrorEnabled { get; }
        bool IsWarnEnabled { get; }
        bool IsFixMeEnabled { get; }
        bool IsInfoEnabled { get; }
        bool IsDebugEnabled { get; }
        bool IsTraceEnabled { get; }
    }
}
