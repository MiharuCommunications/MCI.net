//-----------------------------------------------------------------------
// <copyright file="TimeoutError.cs" company="Miharu Communications Inc.">
//     © 2017 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu.Errors
{
    using System;

    public class TimeoutError : Error
    {
        public TimeSpan Timeout { get; private set; }

        public TimeoutError(TimeSpan timeout)
        {
            this.Timeout = timeout;

            this.ErrorMessage = "処理がタイムアウトしました Timeout = " + this.Timeout.TotalMilliseconds.ToString() + " [ms]";
        }
    }
}
