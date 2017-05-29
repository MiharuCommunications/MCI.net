//-----------------------------------------------------------------------
// <copyright file="ILogger.cs" company="Miharu Communications Inc.">
//     © 2017 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu.Debugs
{
    using System;

    [Obsolete("Please use utilities on Miharu.Logging.  Deprecated since 0.9.9.")]
    public interface ILogger
    {
        void AddError(string message);
        void AddLog(string message);
        void AddObjectToError(object obj);

        void AddException(Exception e);
        void AddException(Exception e, bool killed);
    }
}
