using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Miharu.Core.Tests.Monads
{
    public class EitherTests
    {
        [Fact]
        public void IsTest()
        {
            Either<string, int> r = new Right<string, int>(10);

            Assert.True(r.IsRight);
            Assert.False(r.IsLeft);

            Either<string, int> l = new Left<string, int>("a");

            Assert.True(l.IsLeft);
            Assert.False(l.IsRight);
        }

        [Fact]
        public void LINQTest()
        {
            var result = from e1 in GetInt("1").Right
                         from e2 in GetInt("2").Right
                         select e1 + e2;

            Assert.True(result.IsDefined);
            Assert.True(result.Get() == 3);
        }




        public static Either<string, int> GetInt(string str)
        {
            try
            {
                return new Right<string, int>(int.Parse(str));
            }
            catch
            {
                return new Left<string, int>("Fail");
            }
        }
    }
}
