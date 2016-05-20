using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;


namespace Miharu.Core.Tests.Monads
{
    public class Option2Tests
    {

        [Fact]
        public void IsDefined()
        {
            var none = Option2.Fail<int>();
            var some = Option2.Return(10);

            Assert.True(some.IsDefined);
            Assert.False(none.IsDefined);

            Assert.False(some.IsEmpty);
            Assert.True(none.IsEmpty);
        }
    }
}
