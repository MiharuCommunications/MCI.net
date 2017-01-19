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
        void Fatal(string message);
        void Fatal(string message, Exception error);
        void Fatal(string message, IError error);

        void Error(string message);
        void Error(string message, Exception error);
        void Error(string message, IError error);

        void Warn(string message);
        void Warn(string message, Exception error);
        void Warn(string message, IError error);

        void FixMe(string message);
        void FixMe(string message, Exception error);
        void FixMe(string message, IError error);

        void Info(string message);
        void Info(string message, Exception error);
        void Info(string message, IError error);

        void Debug(string message);
        void Debug(string message, Exception error);
        void Debug(string message, IError error);

        void Trace(string message);
        void Trace(string message, Exception error);
        void Trace(string message, IError error);

        bool IsFatalEnabled { get; }
        bool IsErrorEnabled { get; }
        bool IsWarnEnabled { get; }
        bool IsFixMeEnabled { get; }
        bool IsInfoEnabled { get; }
        bool IsDebugEnabled { get; }
        bool IsTraceEnabled { get; }
    }
}
