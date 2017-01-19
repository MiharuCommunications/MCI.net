namespace Miharu.Logging
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Miharu.Errors;

    public static class LoggerHelper
    {
        public static string ToLog(DateTime now, LogLevel level, string message)
        {
            var date = now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK");
            var prefix = level.ToStringForLog();

            return prefix + date + " " + message + Environment.NewLine;
        }

        public static string ToLog(DateTime now, LogLevel level, string message, Exception error)
        {
            var date = now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK");
            var prefix = level.ToStringForLog();

            return prefix + date + " " + message + Environment.NewLine
                + error.GetType().Name + Environment.NewLine
                + error.Message + Environment.NewLine
                + error.StackTrace + Environment.NewLine;
        }

        public static string ToLog(DateTime now, LogLevel level, string message, IError error)
        {
            var date = now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK");
            var prefix = level.ToStringForLog();

            return prefix + date + " " + message + Environment.NewLine
                + error.GetType().Name + Environment.NewLine
                + error.ErrorMessage + Environment.NewLine;
        }
    }
}
