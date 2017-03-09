namespace Miharu.Errors.IO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface INotEnoughDiskSpaceError : IFileIOError { }

    public class NotEnoughDiskSpaceError : INotEnoughDiskSpaceError
    {
        public NotEnoughDiskSpaceError(string target)
        {
            this.Target = target;
        }


        public string Target { get; private set; }

        public string ErrorMessage
        {
            get
            {
                return "ディスクに十分な空き領域がありません";
            }
        }
    }
}
