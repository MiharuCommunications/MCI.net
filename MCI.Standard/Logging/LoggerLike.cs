namespace Miharu.Logging
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;

    public abstract class LoggerLike : ILogger
    {
        protected LoggerLike(LogLevel level)
        {
            this.IsFatalEnabled = level.IsEnabled(LogLevel.Fatal);
            this.IsErrorEnabled = level.IsEnabled(LogLevel.Error);
            this.IsWarnEnabled = level.IsEnabled(LogLevel.Warn);
            this.IsFixMeEnabled = level.IsEnabled(LogLevel.FixMe);
            this.IsInfoEnabled = level.IsEnabled(LogLevel.Info);
            this.IsDebugEnabled = level.IsEnabled(LogLevel.Debug);
            this.IsTraceEnabled = level.IsEnabled(LogLevel.Trace);
        }

        protected abstract void Write(string row);


        public bool IsFatalEnabled { get; protected set; }

        public bool IsErrorEnabled { get; protected set; }

        public bool IsWarnEnabled { get; protected set; }

        public bool IsFixMeEnabled { get; protected set; }

        public bool IsInfoEnabled { get; protected set; }

        public bool IsDebugEnabled { get; protected set; }

        public bool IsTraceEnabled { get; protected set; }

        public void Debug(string message)
        {
            if (this.IsDebugEnabled)
            {
                this.Write(LoggerHelper.ToLog(DateTime.Now, LogLevel.Debug, Thread.CurrentThread.ManagedThreadId, message));
            }
        }

        public void Debug(string message, Exception error)
        {
            if (this.IsDebugEnabled)
            {
                this.Write(LoggerHelper.ToLog(DateTime.Now, LogLevel.Debug, Thread.CurrentThread.ManagedThreadId, message, error));
            }
        }

        public void Debug(string message, IFailedReason error)
        {
            if (this.IsDebugEnabled)
            {
                this.Write(LoggerHelper.ToLog(DateTime.Now, LogLevel.Debug, Thread.CurrentThread.ManagedThreadId, message, error));
            }
        }

        public void Error(string message)
        {
            if (this.IsErrorEnabled)
            {
                this.Write(LoggerHelper.ToLog(DateTime.Now, LogLevel.Error, Thread.CurrentThread.ManagedThreadId, message));
            }
        }

        public void Error(string message, Exception error)
        {
            if (this.IsErrorEnabled)
            {
                this.Write(LoggerHelper.ToLog(DateTime.Now, LogLevel.Error, Thread.CurrentThread.ManagedThreadId, message, error));
            }
        }

        public void Error(string message, IFailedReason error)
        {
            if (this.IsErrorEnabled)
            {
                this.Write(LoggerHelper.ToLog(DateTime.Now, LogLevel.Error, Thread.CurrentThread.ManagedThreadId, message, error));
            }
        }

        public void Fatal(string message)
        {
            if (this.IsFatalEnabled)
            {
                this.Write(LoggerHelper.ToLog(DateTime.Now, LogLevel.Fatal, Thread.CurrentThread.ManagedThreadId, message));
            }
        }

        public void Fatal(string message, Exception error)
        {
            if (this.IsFatalEnabled)
            {
                this.Write(LoggerHelper.ToLog(DateTime.Now, LogLevel.Fatal, Thread.CurrentThread.ManagedThreadId, message, error));
            }
        }

        public void Fatal(string message, IFailedReason error)
        {
            if (this.IsFatalEnabled)
            {
                this.Write(LoggerHelper.ToLog(DateTime.Now, LogLevel.Fatal, Thread.CurrentThread.ManagedThreadId, message, error));
            }
        }

        public void FixMe(string message)
        {
            if (this.IsFixMeEnabled)
            {
                this.Write(LoggerHelper.ToLog(DateTime.Now, LogLevel.FixMe, Thread.CurrentThread.ManagedThreadId, message));
            }
        }

        public void FixMe(string message, Exception error)
        {
            if (this.IsFixMeEnabled)
            {
                this.Write(LoggerHelper.ToLog(DateTime.Now, LogLevel.FixMe, Thread.CurrentThread.ManagedThreadId, message, error));
            }
        }

        public void FixMe(string message, IFailedReason error)
        {
            if (this.IsFixMeEnabled)
            {
                this.Write(LoggerHelper.ToLog(DateTime.Now, LogLevel.FixMe, Thread.CurrentThread.ManagedThreadId, message, error));
            }
        }

        public void Info(string message)
        {
            if (this.IsInfoEnabled)
            {
                this.Write(LoggerHelper.ToLog(DateTime.Now, LogLevel.Info, Thread.CurrentThread.ManagedThreadId, message));
            }
        }

        public void Info(string message, Exception error)
        {
            if (this.IsInfoEnabled)
            {
                this.Write(LoggerHelper.ToLog(DateTime.Now, LogLevel.Info, Thread.CurrentThread.ManagedThreadId, message, error));
            }
        }

        public void Info(string message, IFailedReason error)
        {
            if (this.IsInfoEnabled)
            {
                this.Write(LoggerHelper.ToLog(DateTime.Now, LogLevel.Info, Thread.CurrentThread.ManagedThreadId, message, error));
            }
        }

        public void Trace(string message)
        {
            if (this.IsTraceEnabled)
            {
                this.Write(LoggerHelper.ToLog(DateTime.Now, LogLevel.Trace, Thread.CurrentThread.ManagedThreadId, message));
            }
        }

        public void Trace(string message, Exception error)
        {
            if (this.IsTraceEnabled)
            {
                this.Write(LoggerHelper.ToLog(DateTime.Now, LogLevel.Trace, Thread.CurrentThread.ManagedThreadId, message, error));
            }
        }

        public void Trace(string message, IFailedReason error)
        {
            if (this.IsTraceEnabled)
            {
                this.Write(LoggerHelper.ToLog(DateTime.Now, LogLevel.Trace, Thread.CurrentThread.ManagedThreadId, message, error));
            }
        }

        public void Warn(string message)
        {
            if (this.IsWarnEnabled)
            {
                this.Write(LoggerHelper.ToLog(DateTime.Now, LogLevel.Warn, Thread.CurrentThread.ManagedThreadId, message));
            }
        }

        public void Warn(string message, Exception error)
        {
            if (this.IsWarnEnabled)
            {
                this.Write(LoggerHelper.ToLog(DateTime.Now, LogLevel.Warn, Thread.CurrentThread.ManagedThreadId, message, error));
            }
        }

        public void Warn(string message, IFailedReason error)
        {
            if (this.IsWarnEnabled)
            {
                this.Write(LoggerHelper.ToLog(DateTime.Now, LogLevel.Warn, Thread.CurrentThread.ManagedThreadId, message, error));
            }
        }
    }
}
