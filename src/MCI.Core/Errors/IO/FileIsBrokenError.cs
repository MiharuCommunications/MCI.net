namespace Miharu.Errors.IO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IFileIsBrokenError : IFileIOError
    {
    }

    public class FileIsBrokenError : IFileIsBrokenError
    {
        public FileIsBrokenError(string target)
        {
            this.Target = target;
        }

        public string Target { get; private set; }

        public string ErrorMessage
        {
            get
            {
                return "ファイルが破損しています Path = " + this.Target;
            }
        }
    }
}
