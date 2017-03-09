using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miharu.Errors
{
    public interface INoError : IError
    {
    }

    public class NoError : INoError
    {
        public NoError()
        {
        }

        public string ErrorMessage
        {
            get
            {
                return "エラーはありません";
            }
        }
    }
}
