namespace Miharu.Errors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class NotImplementedError : Error
    {
        public NotImplementedError()
        {
            this.ErrorMessage = "この機能はまだ実装されていません。";
        }
    }
}
