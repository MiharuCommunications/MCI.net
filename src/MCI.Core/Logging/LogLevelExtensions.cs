//-----------------------------------------------------------------------
// <copyright file="LogLevelExtensions.cs" company="Miharu Communications Inc.">
//     © 2017 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu.Logging
{
    public static class LogLevelExtensions
    {
        public static string ToStringForLog(this LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Fatal:
                    return "[fatal]";

                case LogLevel.Error:
                    return "[error]";

                case LogLevel.Warn:
                    return "[warn]";

                case LogLevel.FixMe:
                    return "[fixme]";

                case LogLevel.Info:
                    return "[info]";

                case LogLevel.Debug:
                    return "[debug]";

                case LogLevel.Trace:
                    return "[trace]";

                default:
                    return "[unkown]";
            }
        }
    }
}
