namespace Miharu.Logging
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Miharu.Errors;

    public interface ILogger
    {
        Either<IError, Unit> Fatal(string message);
        Either<IError, Unit> Fatal(string message, Exception error);
        Either<IError, Unit> Fatal(string message, IError error);

        Either<IError, Unit> Error(string message);
        Either<IError, Unit> Error(string message, Exception error);
        Either<IError, Unit> Error(string message, IError error);

        Either<IError, Unit> Warn(string message);
        Either<IError, Unit> Warn(string message, Exception error);
        Either<IError, Unit> Warn(string message, IError error);

        Either<IError, Unit> FixMe(string message);
        Either<IError, Unit> FixMe(string message, Exception error);
        Either<IError, Unit> FixMe(string message, IError error);

        Either<IError, Unit> Info(string message);
        Either<IError, Unit> Info(string message, Exception error);
        Either<IError, Unit> Info(string message, IError error);

        Either<IError, Unit> Debug(string message);
        Either<IError, Unit> Debug(string message, Exception error);
        Either<IError, Unit> Debug(string message, IError error);

        Either<IError, Unit> Trace(string message);
        Either<IError, Unit> Trace(string message, Exception error);
        Either<IError, Unit> Trace(string message, IError error);

        bool IsFatalEnabled { get; }
        bool IsErrorEnabled { get; }
        bool IsWarnEnabled { get; }
        bool IsFixMeEnabled { get; }
        bool IsInfoEnabled { get; }
        bool IsDebugEnabled { get; }
        bool IsTraceEnabled { get; }
    }
}
