namespace Miharu.Errors.IO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IDirectoryNotFoundError : IFileIOError
    {
    }

    public class DirectoryNotFoundError : IDirectoryNotFoundError
    {
        public DirectoryNotFoundError(string target)
        {
            this.Target = target;
        }

        public string Target { get; private set; }

        public string ErrorMessage
        {
            get
            {
                return "ディレクトリが見つかりません Path = " + this.Target;
            }
        }
    }
}
