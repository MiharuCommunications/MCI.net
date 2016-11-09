namespace Miharu.Errors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    public interface INotEnoughDiskSpaceError : IFileIOError
    {
    }

    public interface IFileNotFoundError : IFileIOError
    {
    }

    public interface IDirectoryNotFoundError : IFileIOError
    {
    }
}
