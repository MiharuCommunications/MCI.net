namespace Miharu.Debugs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ILogger
    {
        void AddError(string message);
        void AddLog(string message);
        void AddObjectToError(object obj);

        void AddException(Exception e);
        void AddException(Exception e, bool killed);
    }
}
