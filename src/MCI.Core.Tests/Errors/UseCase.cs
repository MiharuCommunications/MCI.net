using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Miharu.Errors;

namespace Miharu.Core.Tests.Errors
{
    public class UseCase
    {
        public void Test()
        {
            var e = from x in GetInt("1")
                    from y in GetInt("2")
                    select x + y;

            if (e.IsLeft)
            {
                var error = e.Left.Get();

                if (error is IFileIOError)
                {

                }
            }
            else
            {

            }

        }

        public Either<IError, int> GetInt(string text)
        {
            try
            {
                var i = int.Parse(text);
                return new Right<IError, int>(i);
            }
            catch (Exception)
            {
                return new Left<IError, int>(null);
            }
        }
    }
}
