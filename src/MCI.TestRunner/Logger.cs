namespace Miharu.TestRunner
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class Logger
    {
        private static object lockObject;
        private static string filePath;

        static Logger()
        {
            lockObject = new object();
            filePath = "error.log";
        }

        public static void AddLog(string className, string memberName, string message)
        {
            lock (lockObject)
            {
                try
                {
                    var line = DateTime.Now.ToString("yy/MM/dd HH:mm:ss") + "," + className + "," + memberName + "," + message;

                    using (var writer = new StreamWriter(filePath, true, Encoding.UTF8))
                    {
                        writer.WriteLine(line);
                    }

                    Console.WriteLine(line);
                }
                catch (Exception) { }
            }
        }
    }
}
