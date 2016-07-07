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
        public void RightTests()
        {
            Either<string, int> e = new Right<string, int>(0);

            Assert.True(e.IsRight);
            Assert.False(e.IsLeft);


            var s = e.Swap();

            Assert.False(s.IsRight);
            Assert.True(s.IsLeft);
        }


        [Fact]
        public void LeftTests()
        {
            Either<string, int> e = new Left<string, int>("0");

            Assert.False(e.IsRight);
            Assert.True(e.IsLeft);



            var s = e.Swap();

            Assert.True(s.IsRight);
            Assert.False(s.IsLeft);
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
