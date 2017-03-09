namespace Miharu.Errors.IO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IFileNotFoundError : IFileIOError { }


    public class FileNotFoundError : IFileNotFoundError
    {
        public FileNotFoundError(string target)
        {
            this.Target = target;
        }

        public string Target { get; private set; }

        public string ErrorMessage
        {
            get
            {
                return "ファイルが見つかりません Path = " + this.Target;
            }
        }
    }
}
