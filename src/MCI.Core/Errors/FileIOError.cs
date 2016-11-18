namespace Miharu.Errors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    public interface IFileIOError : IError
    {
        string Target { get; }
    }

    public interface INotEnoughDiskSpaceError : IFileIOError
    {
    }

    public class NotEnoughDiskSpaceError : INotEnoughDiskSpaceError
    {
        public NotEnoughDiskSpaceError(string target)
        {
            this.Target = target;
        }


        public string Target { get; private set; }

        public string ErrorMessage
        {
            get { throw new NotImplementedException(); }
        }
    }

    public interface IFileNotFoundError : IFileIOError
    {
    }

    public class FileNotFoundError : IFileNotFoundError
    {
        public FileNotFoundError(string target)
        {
            this.Target = target;
        }

        public string ErrorMessage
        {
            get { throw new NotImplementedException(); }
        }

        public string Target { get; private set; }
    }


    public interface IDirectoryNotFoundError : IFileIOError
    {
    }

    public class DirectoryNotFoundError : IDirectoryNotFoundError
    {
        public DirectoryNotFoundError(string target)
        {
            this.Target = target;
        }

        public string ErrorMessage
        {
            get { throw new NotImplementedException(); }
        }

        public string Target { get; private set; }
    }


    public interface IFileIsEmptyError : IFileIOError
    {
    }

    public class FileIsEmptyError : IFileIsEmptyError
    {
        public FileIsEmptyError(string target)
        {
            this.Target = target;
        }

        public string Target { get; private set; }

        public string ErrorMessage
        {
            get { throw new NotImplementedException(); }
        }
    }

    public interface IFileIsBrokenError : IFileIOError
    {
    }

    public class FileIsBrokenError : IFileIsBrokenError
    {
        public FileIsBrokenError(string target)
        {
            this.Target = target;
        }

        public string Target { get; private set; }

        public string ErrorMessage
        {
            get { throw new NotImplementedException(); }
        }
    }
}
