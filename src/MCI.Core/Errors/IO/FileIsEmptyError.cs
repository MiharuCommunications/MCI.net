namespace Miharu.Errors.IO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IFileIsEmptyError : IFileIOError { }


    public class FileIsEmptyError : IFileIsEmptyError
    {
        public FileIsEmptyError(string target)
        {
            this.Target = target;
        }

        public string Target { get; private set; }

        public string ErrorMessage
        {
            get
            {
                return "ファイルが空です Path = " + this.Target;
            }
        }
    }
}
