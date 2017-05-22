namespace Miharu.Errors.IO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DirectoryNotFoundError : FileIOError
    {
        public DirectoryNotFoundError(string target)
            : base(target)
        {
            this.ErrorMessage = "ディレクトリが見つかりません Path = " + target;
        }
    }
}
