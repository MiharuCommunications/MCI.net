using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miharu.Monads
{
    public class IO<A>
    {
        public IO<B> Map<B>(Func<A, B> f)
        {
            throw new NotImplementedException();
        }
    }
}
