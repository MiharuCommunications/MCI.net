namespace Miharu.Logging
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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
