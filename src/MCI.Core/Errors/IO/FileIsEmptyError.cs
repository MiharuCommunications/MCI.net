namespace Miharu.Errors.IO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class FileIsEmptyError : FileIOError
    {
        public FileIsEmptyError(string target)
            : base(target)
        {
            this.ErrorMessage = "ファイルが空です Path = " + target;
        }
    }
}
