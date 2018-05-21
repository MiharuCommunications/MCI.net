namespace Miharu.IO.Files
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class FileIOError : IFailedReason
    {
        public string Target { get; private set; }

        public string ErrorMessage { get; protected set; }

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
            this.ErrorMessage = "ディレクトリが見つかりません Path = " + target;
        }
    }

    public class FileNotFoundError : FileIOError
    {
        public FileNotFoundError(string target)
            : base(target)
        {
            this.ErrorMessage = "ファイルが見つかりません Path = " + target;
        }
    }

    public class NotEnoughDiskSpaceError : FileIOError
    {
        public NotEnoughDiskSpaceError(string target)
            : base(target)
        {
            this.ErrorMessage = "ディスクに十分な空き領域がありません。Path = " + target;
        }
    }

    public class FileIsBrokenError : FileIOError
    {
        public FileIsBrokenError(string target)
            : base(target)
        {
            this.ErrorMessage = "ファイルが破損しています Path = " + target;
        }
    }

    public class FileIsEmptyError : FileIOError
    {
        public FileIsEmptyError(string target)
            : base(target)
        {
            this.ErrorMessage = "ファイルが空です Path = " + target;
        }
    }
}
