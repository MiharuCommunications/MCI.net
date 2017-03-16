namespace Miharu.Errors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface INotImplementedError : IError
    {

    }

    public class NotImplementedError : INotImplementedError
    {
        public NotImplementedError()
        {
        }

        public string ErrorMessage
        {
            get
            {
                return "この機能はまだ実装されていません。";
            }
        }
    }
}
