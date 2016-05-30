using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Miharu.Core.Tests.Monads
{
    public class TryTests
    {
        [Fact]
        public void LINQTest()
        {
            var result = from n1 in GetInt("1")
                         from n2 in GetInt("2")
                         select n1 + n2;

            Assert.True(result.IsSuccess);
            Assert.True(result.GetOrElse(0) == 3);
        }


        public static Try<int> GetInt(string str)
        {
            return Try<int>.Execute(() =>
            {
                return int.Parse(str);
            });
        }
    }
}
