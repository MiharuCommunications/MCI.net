namespace Miharu.Errors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class NoError : Error
    {
        public NoError()
        {
            this.ErrorMessage = "エラーはありません。";
        }
    }
}
