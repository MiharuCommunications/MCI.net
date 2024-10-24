namespace Miharu.Async
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class TaskHasCanceledError : IFailedReason
    {
        public string ErrorMessage { get; private set; }

        public TaskHasCanceledError()
        {
            ErrorMessage = "タスクがキャンセルされました。";
        }
    }
}
