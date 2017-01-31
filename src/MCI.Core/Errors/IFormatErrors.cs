using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miharu.Errors
{
    public interface IInvalidFormatError : IError
    {
    }

    public class InvalidFormatError : IInvalidFormatError
    {
        public InvalidFormatError(string message)
        {
            this.ErrorMessage = message;
        }

        public string ErrorMessage { get; private set; }
    }
}
