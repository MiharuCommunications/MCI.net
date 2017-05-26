namespace Miharu.Errors.Async
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TaskHasCanceledError : Error
    {
        public TaskHasCanceledError()
        {
            ErrorMessage = "タスクがキャンセルされました。";
        }
    }
}
