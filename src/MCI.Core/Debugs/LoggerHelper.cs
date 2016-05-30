namespace Miharu.Debugs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class LoggerHelper
    {
        public static readonly string DateTimeFormat = "yy/MM/dd HH:mm:ss";

        public static IEnumerable<string> CreateMessageLines(string message)
        {
            var datetimeStr = DateTime.Now.ToString(DateTimeFormat);

            foreach (var line in message.SplitLines())
            {
                yield return datetimeStr + "\t" + line.Trim();
            }
        }



        public static IEnumerable<string> CreateExceptionLines(Exception e, bool killed)
        {
            yield return "===========================================================";
            yield return DateTime.Now.ToString(DateTimeFormat);

            if ((object)e == null)
            {
                yield return "Exception が null です。";
            }
            else
            {
                yield return e.GetType().Name;

                if ((object)e.Message != null)
                {
                    foreach (var line in e.Message.SplitLines())
                    {
                        yield return line;
                    }
                }

                yield return killed ? "アプリケーション終了" : "続行";

                if ((object)e.Message != null)
                {
                    foreach (var line in e.Message.SplitLines())
                    {
                        yield return line;
                    }
                }

                if ((object)e.StackTrace != null)
                {
                    foreach (var line in e.StackTrace.SplitLines())
                    {
                        yield return line;
                    }
                }
            }
        }
    }
}
