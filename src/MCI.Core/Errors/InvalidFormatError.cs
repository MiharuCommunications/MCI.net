namespace Miharu.Errors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class InvalidFormatError : Error
    {
        public InvalidFormatError(string message)
        {
            this.ErrorMessage = message;
        }
    }
}
