//-----------------------------------------------------------------------
// <copyright file="LoggerHelper.cs" company="Miharu Communications Inc.">
//     © 2017 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu.Logging
{
    using System;

    public static class LoggerHelper
    {
        public static string ToLog(DateTime now, LogLevel level, int threadId, string message)
        {
            var date = now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK");
            var prefix = level.ToStringForLog();

            return date + "\t" + prefix + "\t" + threadId.ToString() + "\t" + message + Environment.NewLine;
        }

        public static string ToLog(DateTime now, LogLevel level, int threadId, string message, Exception error)
        {
            var date = now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK");
            var prefix = level.ToStringForLog();

            return date + "\t" + prefix + "\t" + threadId.ToString() + "\t" + message + Environment.NewLine
                + error.GetType().Name + Environment.NewLine
                + error.Message + Environment.NewLine
                + error.StackTrace + Environment.NewLine;
        }

        public static string ToLog(DateTime now, LogLevel level, int threadId, string message, IFailedReason error)
        {
            var date = now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK");
            var prefix = level.ToStringForLog();

            return date + "\t" + prefix + "\t" + threadId.ToString() + "\t" + message + Environment.NewLine
                + error.GetType().Name + Environment.NewLine;
                // + error.ErrorMessage + Environment.NewLine;
        }


        public static bool IsEnabled(this LogLevel border, LogLevel target)
        {
            return (int)border >= (int)target;
        }
    }
}
