namespace Miharu.Errors.IO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class FileNotFoundError : FileIOError
    {
        public FileNotFoundError(string target)
            : base(target)
        {
            this.ErrorMessage = "ファイルが見つかりません Path = " + target;
        }
    }
}
