namespace Miharu.Errors.IO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class FileIsBrokenError : FileIOError
    {
        public FileIsBrokenError(string target)
            :base(target)
        {
            this.ErrorMessage = "ファイルが破損しています Path = " + target;
        }
    }
}
