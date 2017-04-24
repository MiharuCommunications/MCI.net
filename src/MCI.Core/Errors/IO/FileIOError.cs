namespace Miharu.Errors.IO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class FileIOError : Error
    {
        public string Target { get; private set; }

        protected FileIOError(string target)
        {
            this.Target = target;
        }
    }
}
