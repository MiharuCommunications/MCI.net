namespace Miharu.IO.Files
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class FileIOError : IFailedReason
    {
        public string Target { get; private set; }

        protected FileIOError(string target)
        {
            this.Target = target;
        }
    }

    public class DirectoryNotFoundError : FileIOError
    {
        public DirectoryNotFoundError(string target)
            : base(target)
        {
        }
    }

    public class FileNotFoundError : FileIOError
    {
        public FileNotFoundError(string target)
            : base(target)
        {
        }
    }

    public class NotEnoughDiskSpaceError : FileIOError
    {
        public NotEnoughDiskSpaceError(string target)
            : base(target)
        {
        }
    }

    public class FileIsBrokenError : FileIOError
    {
        public FileIsBrokenError(string target)
            : base(target)
        {
        }
    }

    public class FileIsEmptyError : FileIOError
    {
        public FileIsEmptyError(string target)
            : base(target)
        {
        }
    }
}
