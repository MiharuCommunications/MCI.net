using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miharu.Errors
{
    public interface IUnkownError : IError
    {
        Exception InnerException { get; }
    }

    public class UnkownError : IUnkownError
    {
        public Exception InnerException { get; private set; }

        public UnkownError(Exception exception)
        {
            this.InnerException = exception;
            this.ErrorMessage = "原因不明のエラーです。";
        }

        public UnkownError(Exception exception, string message)
        {
            this.InnerException = exception;
            this.ErrorMessage = message;
        }

        public string ErrorMessage { get; private set; }
    }
}
