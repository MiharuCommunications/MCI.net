namespace Miharu.Errors.IO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class NotEnoughDiskSpaceError : FileIOError
    {
        public NotEnoughDiskSpaceError(string target)
            : base(target)
        {
            this.ErrorMessage = "ディスクに十分な空き領域がありません。Path = " + target;
        }
    }
}
